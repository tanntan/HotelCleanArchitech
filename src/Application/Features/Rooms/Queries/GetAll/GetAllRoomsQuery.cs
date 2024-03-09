// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Rooms.Caching;
using CleanArchitecture.Blazor.Application.Features.Rooms.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Rooms.Queries.GetAll;

public class GetAllRoomsQuery : ICacheableRequest<IEnumerable<RoomDto>>
{
    public string CacheKey => RoomCacheKey.GetAllCacheKey;
    public MemoryCacheEntryOptions? Options => RoomCacheKey.MemoryCacheEntryOptions;
}

public class GetRoomQuery : ICacheableRequest<RoomDto>
{
    public required int Id { get; set; }

    public string CacheKey => RoomCacheKey.GetRoomByIdCacheKey(Id);
    public MemoryCacheEntryOptions? Options => RoomCacheKey.MemoryCacheEntryOptions;
}

public class GetAllRoomsQueryHandler :
    IRequestHandler<GetAllRoomsQuery, IEnumerable<RoomDto>>,
    IRequestHandler<GetRoomQuery, RoomDto>

{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<GetAllRoomsQueryHandler> _localizer;
    private readonly IMapper _mapper;

    public GetAllRoomsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllRoomsQueryHandler> localizer
    )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        //TODO:Implementing GetAllProductsQueryHandler method 
        var data = await _context.Rooms
            .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return data;
    }

    public async Task<RoomDto> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Rooms.Where(x => x.Id == request.Id)
                       .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync(cancellationToken) ??
                   throw new NotFoundException($"Room with id: {request.Id} not found.");
        return data;
    }
}