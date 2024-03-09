// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Common.Mappings;
using CleanArchitecture.Blazor.Application.Features.Rooms.Caching;
using CleanArchitecture.Blazor.Application.Features.Rooms.DTOs;
using CleanArchitecture.Blazor.Application.Features.Rooms.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Rooms.Queries.Paginations;

public class RoomsWithPaginationQuery : RoomAdvancedFilter, ICacheableRequest<PaginatedData<RoomDto>>
{
    public RoomAdvancedSpecification Specification => new(this);


    public string CacheKey => RoomCacheKey.GetPaginationCacheKey($"{this}");

    public MemoryCacheEntryOptions? Options => RoomCacheKey.MemoryCacheEntryOptions;

    // the currently logged in user
    public override string ToString()
    {
        return
            $"CurrentUser:{CurrentUser?.UserId},ListView:{ListView},Search:{Keyword},Name:{Name},SortDirection:{SortDirection},OrderBy:{OrderBy},{PageNumber},{PageSize}";
    }
}

public class RoomsWithPaginationQueryHandler :
    IRequestHandler<RoomsWithPaginationQuery, PaginatedData<RoomDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<RoomsWithPaginationQueryHandler> _localizer;
    private readonly IMapper _mapper;

    public RoomsWithPaginationQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<RoomsWithPaginationQueryHandler> localizer
    )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<PaginatedData<RoomDto>> Handle(RoomsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var item = await _context.Rooms.OrderBy($"{request.OrderBy} {request.SortDirection}")
            .ProjectToPaginatedDataAsync<Room, RoomDto>(request.Specification, request.PageNumber,
                request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
        return item;
    }
}