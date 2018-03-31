using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using DbDataLibrary.Data;
using System.Threading.Tasks;
using lab3.ViewModels;
using lab3.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace lab3.Middleware
{
    public class DbCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;
        private string _cacheKey;

        public DbCacheMiddleware(RequestDelegate next, IMemoryCache memoryCache, string cacheKey)
        {
            _next = next;
            _memoryCache = memoryCache;
            _cacheKey = cacheKey;
        }

        public Task Invoke(HttpContext httpContext, ToursSqliteDbContext _db)
        {
            IndexViewModel indexViewModel;
            if (!_memoryCache.TryGetValue(_cacheKey, out indexViewModel))
            {
                var clients = _db.Clients
                    .OrderBy(t=>t.Name)
                    .Take(10)
                    .ToList();
                var tourKinds = _db.TourKinds
                    .OrderBy(t => t.Name)
                    .Take(10)
                    .ToList();
                var tours = _db.Tours
                    .OrderBy(t => t.Name)
                    .Include(t => t.Client)
                    .Include(t => t.TourKind)
                    .Take(10)
                    .ToList();

                indexViewModel = new IndexViewModel { Clients = clients, TourKinds = tourKinds, Tours = tours };
                _memoryCache.Set(_cacheKey, indexViewModel,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(Constants.Seconds)));
            }

            return _next(httpContext);
        }
    }


    public static class DbCacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseOperatinCache(this IApplicationBuilder builder, string cacheKey)
        {
            return builder.UseMiddleware<DbCacheMiddleware>(cacheKey);
        }
    }
}
