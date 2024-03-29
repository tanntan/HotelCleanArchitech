// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Bookings.Caching;
using CleanArchitecture.Blazor.Application.Features.Bookings.DTOs;
using CleanArchitecture.Blazor.Application.Features.Rooms.Queries.GetAll;

namespace CleanArchitecture.Blazor.Application.Features.Bookings.Queries.GetAll;

public class GetAllBookingQuery : ICacheableRequest<IEnumerable<BookingDto>>
{
    public string CacheKey => BookingCachingKey.GetAllCacheKey;
    public MemoryCacheEntryOptions? Options => BookingCachingKey.MemoryCacheEntryOptions;
}

public class GetBookingQuery : ICacheableRequest<BookingDto>
{
    public required int Id { get; set; }

    public string CacheKey => BookingCachingKey.GetRoomByIdCacheKey(Id);
    public MemoryCacheEntryOptions? Options => BookingCachingKey.MemoryCacheEntryOptions;
}

public class GetAllBookingQueryHandler :
    IRequestHandler<GetAllBookingQuery, IEnumerable<BookingDto>>,
    IRequestHandler<GetBookingQuery, BookingDto>

{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<GetAllBookingQueryHandler> _localizer;
    private readonly IMapper _mapper;

    public GetAllBookingQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllBookingQueryHandler> localizer
    )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<BookingDto>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
    {
        //TODO:Implementing GetAllProductsQueryHandler method 
        var data = await _context.Bookings
            .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return data;
    }

    public async Task<BookingDto> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Bookings.Where(x => x.Id == request.Id)
                       .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync(cancellationToken) ??
                   throw new NotFoundException($"Booking with id: {request.Id} not found.");
        return data;
    }

    

}