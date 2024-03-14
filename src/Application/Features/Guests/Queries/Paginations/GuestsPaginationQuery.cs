// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Common.Mappings;
using CleanArchitecture.Blazor.Application.Features.Guests.Caching;
using CleanArchitecture.Blazor.Application.Features.Guests.DTOs;
using CleanArchitecture.Blazor.Application.Features.Guests.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Guests.Queries.Paginations;

public class GuestsWithPaginationQuery : GuestAdvancedFilter, ICacheableRequest<PaginatedData<GuestDto>>
{
    public GuestAdvancedSpecification Specification => new(this);


    public string CacheKey => GuestCacheKey.GetPaginationCacheKey($"{this}");

    public MemoryCacheEntryOptions? Options => GuestCacheKey.MemoryCacheEntryOptions;

    // the currently logged in user
    public override string ToString()
    {
        return
            $"CurrentUser:{CurrentUser?.UserId},ListView:{ListView},Search:{Keyword},Name:{Name},SortDirection:{SortDirection},OrderBy:{OrderBy},{PageNumber},{PageSize}";
    }
}

public class GuestsWithPaginationQueryHandler :
    IRequestHandler<GuestsWithPaginationQuery, PaginatedData<GuestDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<GuestsWithPaginationQueryHandler> _localizer;
    private readonly IMapper _mapper;

    public GuestsWithPaginationQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GuestsWithPaginationQueryHandler> localizer
    )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<PaginatedData<GuestDto>> Handle(GuestsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var item = await _context.Guests.OrderBy($"{request.OrderBy} {request.SortDirection}")
            .ProjectToPaginatedDataAsync<Guest, GuestDto>(request.Specification, request.PageNumber,
                request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
        return item;
    }
}