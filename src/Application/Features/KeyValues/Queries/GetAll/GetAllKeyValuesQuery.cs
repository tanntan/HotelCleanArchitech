// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.KeyValues.Caching;
using CleanArchitecture.Blazor.Application.Features.KeyValues.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.Blazor.Application.Features.KeyValues.Queries.ByName;

public class GetAllKeyValuesQuery : IRequest<IList<KeyValueDto>>, ICacheable
{

    public string CacheKey => KeyValueCacheKey.GetAllCacheKey;

    public MemoryCacheEntryOptions? Options => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(KeyValueCacheKey.SharedExpiryTokenSource.Token));
}
public class GetAllKeyValuesQueryHandler : IRequestHandler<GetAllKeyValuesQuery, IList<KeyValueDto>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllKeyValuesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IList<KeyValueDto>> Handle(GetAllKeyValuesQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.KeyValues
           .ProjectTo<KeyValueDto>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);
        return data;
    }
}