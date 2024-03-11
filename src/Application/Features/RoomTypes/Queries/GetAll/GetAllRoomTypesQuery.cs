// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.RoomTypes.Caching;
using CleanArchitecture.Blazor.Application.Features.RoomTypes.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.RoomTypes.Queries.GetAll;

public class GetAllRoomTypesQuery : ICacheableRequest<IEnumerable<RoomTypeDto>>
{
    public string CacheKey => RoomTypeCacheKey.GetAllCacheKey;
    public MemoryCacheEntryOptions? Options => RoomTypeCacheKey.MemoryCacheEntryOptions;
}

public class GetRoomTypeQuery : ICacheableRequest<RoomTypeDto>
{
    public required int Id { get; set; }

    public string CacheKey => RoomTypeCacheKey.GetRoomTypeByIdCacheKey(Id);
    public MemoryCacheEntryOptions? Options => RoomTypeCacheKey.MemoryCacheEntryOptions;
}

public class GetAllRoomTypesQueryHandler :
    IRequestHandler<GetAllRoomTypesQuery, IEnumerable<RoomTypeDto>>,
    IRequestHandler<GetRoomTypeQuery, RoomTypeDto>

{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<GetAllRoomTypesQueryHandler> _localizer;
    private readonly IMapper _mapper;

    public GetAllRoomTypesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllRoomTypesQueryHandler> localizer
    )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<RoomTypeDto>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
    {
        //TODO:Implementing GetAllProductsQueryHandler method 
        var data = await _context.RoomTypes
            .ProjectTo<RoomTypeDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return data;
    }

    public async Task<RoomTypeDto> Handle(GetRoomTypeQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.RoomTypes.Where(x => x.Id == request.Id)
                       .ProjectTo<RoomTypeDto>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync(cancellationToken) ??
                   throw new NotFoundException($"RoomType with id: {request.Id} not found.");
        return data;
    }
}