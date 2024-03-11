// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.RoomTypes.Caching;

public static class RoomTypeCacheKey
{
    public const string GetAllCacheKey = "all-Rooms";
    private static readonly TimeSpan RefreshInterval = TimeSpan.FromHours(1);
    private static CancellationTokenSource _tokenSource;
    private static readonly object _tokenLock = new();

    static RoomTypeCacheKey()
    {
        _tokenSource = new CancellationTokenSource(RefreshInterval);
    }

    public static MemoryCacheEntryOptions MemoryCacheEntryOptions =>
        new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(SharedExpiryTokenSource().Token));

    public static string GetRoomTypeByIdCacheKey(int id)
    {
        return $"GetRoomById,{id}";
    }

    public static string GetPaginationCacheKey(string parameters)
    {
        return $"RoomsWithPaginationQuery,{parameters}";
    }

    public static CancellationTokenSource SharedExpiryTokenSource()
    {
        lock (_tokenLock)
        {
            if (_tokenSource.IsCancellationRequested) _tokenSource = new CancellationTokenSource(RefreshInterval);

            return _tokenSource;
        }
    }

    public static void Refresh()
    {
        lock (_tokenLock)
        {
            if (!_tokenSource.IsCancellationRequested)
            {
                _tokenSource.Cancel();
                _tokenSource = new CancellationTokenSource(RefreshInterval);
            }
        }
    }
}