// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Guests.Caching;
using CleanArchitecture.Blazor.Application.Features.Guests.DTOs;
using CleanArchitecture.Blazor.Application.Features.Rooms.Queries.GetAll;

namespace CleanArchitecture.Blazor.Application.Features.Guests.Queries.GetAll;

public class GetAllGuestQuery : ICacheableRequest<IEnumerable<GuestDto>>
{
    public string CacheKey => GuestCacheKey.GetAllCacheKey;
    public MemoryCacheEntryOptions? Options => GuestCacheKey.MemoryCacheEntryOptions;
}

public class GetGuestQuery : ICacheableRequest<GuestDto>
{
    public required int Id { get; set; }

    public string CacheKey => GuestCacheKey.GetRoomByIdCacheKey(Id);
    public MemoryCacheEntryOptions? Options => GuestCacheKey.MemoryCacheEntryOptions;
}

public class GetAllGuestQueryHandler :
    IRequestHandler<GetAllGuestQuery, IEnumerable<GuestDto>>,
    IRequestHandler<GetGuestQuery, GuestDto>

{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<GetAllGuestQueryHandler> _localizer;
    private readonly IMapper _mapper;

    public GetAllGuestQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllGuestQueryHandler> localizer
    )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<GuestDto>> Handle(GetAllGuestQuery request, CancellationToken cancellationToken)
    {
        //TODO:Implementing GetAllProductsQueryHandler method 
        var data = await _context.Guests
            .ProjectTo<GuestDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return data;
    }

    public async Task<GuestDto> Handle(GetGuestQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Guests.Where(x => x.Id == request.Id)
                       .ProjectTo<GuestDto>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync(cancellationToken) ??
                   throw new NotFoundException($"Guest with id: {request.Id} not found.");
        return data;
    }

    

}