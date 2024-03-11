// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Application.Features.RoomTypes.Caching;

namespace CleanArchitecture.Blazor.Application.Features.RoomTypes.Commands.Delete;

public class DeleteRoomTypeCommand : ICacheInvalidatorRequest<Result<int>>
{
    public DeleteRoomTypeCommand(int[] id)
    {
        Id = id;
    }

    public int[] Id { get; }
    public string CacheKey => RoomTypeCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => RoomTypeCacheKey.SharedExpiryTokenSource();
}

public class DeleteRoomTypeCommandHandler :
    IRequestHandler<DeleteRoomTypeCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<DeleteRoomTypeCommandHandler> _localizer;
    private readonly IMapper _mapper;

    public DeleteRoomTypeCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<DeleteRoomTypeCommandHandler> localizer,
        IMapper mapper
    )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.RoomTypes.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
        foreach (var item in items)
        {
            item.AddDomainEvent(new DeletedEvent<RoomType>(item));
            _context.RoomTypes.Remove(item);
        }

        var result = await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(result);
    }
}