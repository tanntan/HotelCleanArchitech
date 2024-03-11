// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Features.RoomTypes.Caching;
using CleanArchitecture.Blazor.Application.Features.RoomTypes.DTOs;
using Microsoft.AspNetCore.Components.Forms;

namespace CleanArchitecture.Blazor.Application.Features.RoomTypes.Commands.AddEdit;

public class AddEditRoomTypeCommand : ICacheInvalidatorRequest<Result<int>>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Status {get; set;}
    public decimal PricePerNight {get; set;}
    public int Capacity {get; set;}
    public string CacheKey => RoomTypeCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => RoomTypeCacheKey.SharedExpiryTokenSource();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RoomTypeDto, AddEditRoomTypeCommand>(MemberList.None);
            CreateMap<AddEditRoomTypeCommand, RoomType>(MemberList.None);
        }
    }
}

public class AddEditRoomTypeCommandHandler : IRequestHandler<AddEditRoomTypeCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddEditRoomTypeCommandHandler(
        IApplicationDbContext context,
        IMapper mapper
    )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(AddEditRoomTypeCommand request, CancellationToken cancellationToken)
    {
        if (request.Id > 0)
        {
            var item = await _context.RoomTypes.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
                       throw new NotFoundException($"Room Type with id: {request.Id} not found.");
            item = _mapper.Map(request, item);
            item.AddDomainEvent(new UpdatedEvent<RoomType>(item));
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            var item = _mapper.Map<RoomType>(request);
            item.AddDomainEvent(new CreatedEvent<RoomType>(item));
            _context.RoomTypes.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
    }
}