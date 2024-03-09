// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Features.Rooms.Caching;
using CleanArchitecture.Blazor.Application.Features.Rooms.DTOs;
using Microsoft.AspNetCore.Components.Forms;

namespace CleanArchitecture.Blazor.Application.Features.Rooms.Commands.AddEdit;

public class AddEditRoomCommand : ICacheInvalidatorRequest<Result<int>>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public RoomStatus? RoomStatus { get; set; }
    public List<RoomImage>? RoomImages { get; set; }
    public IReadOnlyList<IBrowserFile>? UploadPictures { get; set; }
    public string CacheKey => RoomCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => RoomCacheKey.SharedExpiryTokenSource();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RoomDto, AddEditRoomCommand>(MemberList.None);
            CreateMap<AddEditRoomCommand, Room>(MemberList.None);
        }
    }
}

public class AddEditRoomCommandHandler : IRequestHandler<AddEditRoomCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddEditRoomCommandHandler(
        IApplicationDbContext context,
        IMapper mapper
    )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(AddEditRoomCommand request, CancellationToken cancellationToken)
    {
        if (request.Id > 0)
        {
            var item = await _context.Rooms.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
                       throw new NotFoundException($"Room with id: {request.Id} not found.");
            item = _mapper.Map(request, item);
            item.AddDomainEvent(new UpdatedEvent<Room>(item));
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            var item = _mapper.Map<Room>(request);
            item.AddDomainEvent(new CreatedEvent<Room>(item));
            _context.Rooms.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
    }
}