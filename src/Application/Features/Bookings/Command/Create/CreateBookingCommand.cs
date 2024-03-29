// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using CleanArchitecture.Blazor.Application.Features.Bookings.DTOs;
using CleanArchitecture.Blazor.Application.Features.Bookings.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Bookings.Commands.Create;

public class CreateBookingCommand: ICacheInvalidatorRequest<Result<int>>
{
    
     public int Id {get; set;}
    public Guest? Guest {get; set;}
    public BookingType BookingType {get; set;} = default;
    public List<Room>? Rooms {get; set;}
    [Description("Check Out Date")] public DateTime CheckOutDate {get; set;}
    [Description("Check In Date")] public DateTime CheckInDate {get; set;}
    [Description("Total")] public decimal TotalPrice {get; set;}

    public string CacheKey => BookingCachingKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => BookingCachingKey.SharedExpiryTokenSource();
    private class Mapping : Profile
    {
        public Mapping()
        {
             CreateMap<BookingDto,CreateBookingCommand>(MemberList.None);
             CreateMap<CreateBookingCommand,Customer>(MemberList.None);
        }
    }
}
    
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateBookingCommand> _localizer;
        public CreateBookingCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateBookingCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
           if (request.Id > 0)
        {
            var item = await _context.Bookings.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
                       throw new NotFoundException($"Booking with id: {request.Id} not found.");
            item = _mapper.Map(request, item);
            item.AddDomainEvent(new UpdatedEvent<Booking>(item));
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            var item = _mapper.Map<Booking>(request);
            item.AddDomainEvent(new CreatedEvent<Booking>(item));
            _context.Bookings.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        }
    }

