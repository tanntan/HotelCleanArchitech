// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Common.Mappings;
using CleanArchitecture.Blazor.Application.Features.RoomTypes.Caching;
using CleanArchitecture.Blazor.Application.Features.RoomTypes.DTOs;
using CleanArchitecture.Blazor.Application.Features.RoomTypes.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.RoomTypes.Queries.Paginations;

public class RoomTypesWithPaginationQuery : RoomTypeAdvancedFilter, ICacheableRequest<PaginatedData<RoomTypeDto>>
{
    public RoomTypeAdvancedSpecification Specification => new(this);


    public string CacheKey => RoomTypeCacheKey.GetPaginationCacheKey($"{this}");

    public MemoryCacheEntryOptions? Options => RoomTypeCacheKey.MemoryCacheEntryOptions;

    // the currently logged in user
    public override string ToString()
    {
        return
            $"CurrentUser:{CurrentUser?.UserId},ListView:{ListView},Search:{Keyword},Name:{Name}, PricePerNight: {PricePerNight},SortDirection:{SortDirection},OrderBy:{OrderBy},{PageNumber},{PageSize}";
    }
}

public class RoomTypesWithPaginationQueryHandler :
    IRequestHandler<RoomTypesWithPaginationQuery, PaginatedData<RoomTypeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<RoomTypesWithPaginationQueryHandler> _localizer;
    private readonly IMapper _mapper;

    public RoomTypesWithPaginationQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<RoomTypesWithPaginationQueryHandler> localizer
    )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<PaginatedData<RoomTypeDto>> Handle(RoomTypesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var item = await _context.RoomTypes.OrderBy($"{request.OrderBy} {request.SortDirection}")
            .ProjectToPaginatedDataAsync<RoomType, RoomTypeDto>(request.Specification, request.PageNumber,
                request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
        return item;
    }
}