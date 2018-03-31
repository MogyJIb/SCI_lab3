using Microsoft.AspNetCore.Mvc;
using DbDataLibrary.Models;
using lab3.ViewModels;
using DbDataLibrary.Interfaces;
using DbDataLibrary.Data;
using System.Linq;
using lab3.Utils;
using lab3.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace lab3.Controllers
{
    public class MainController : Controller
    {
        private IMemoryCache _cache;
        private ToursSqliteDbContext _db;

        public MainController(ToursSqliteDbContext dbContext, IMemoryCache memoryCache)
        {
            _db = dbContext;
            _cache = memoryCache;
        }

        [ResponseCache(CacheProfileName = Constants.CachedProfile)]
        public IActionResult Index()
        {
            IndexViewModel cachedViewModel = _cache.Get<IndexViewModel>(Constants.DbTablesCache);

            return View(cachedViewModel);
        }

        [HttpGet]
        public IActionResult Clients()
        {
            var clients = _db.Clients.ToList();
            
            Client client =  HttpContext.Session.Get<Client>(Constants.Client);

            return View(new ClientViewModel { Clients = clients, Client = client });
        }

        [HttpPost]
        public IActionResult Clients(Client client)
        {
            HttpContext.Session.Set(Constants.Client, client);
            
            _db.Clients.Add(client);
            _db.SaveChanges();

            return RedirectToAction("Clients");
        }


        [HttpGet]
        public IActionResult TourKinds()
        {
            var tourKinds = _db.TourKinds.ToList();
           
            TourKind tourKind = HttpContext.Request.Cookies.Get<TourKind>(Constants.TourKind);

            return View(new TourKindViewModel { TourKinds = tourKinds, TourKind = tourKind });
        }

        [HttpPost]
        public IActionResult TourKinds(TourKind tourKind)
        {
            HttpContext.Response.Cookies.Set(Constants.TourKind,tourKind);

            _db.TourKinds.Add(tourKind);
            _db.SaveChanges();

            return RedirectToAction("TourKinds");
        }

        [HttpGet]
        public IActionResult Tours()
        {
            var tours = _db.Tours.ToList();

            return View(new TourViewModel { Tours = tours});
        }

        [HttpPost]
        public IActionResult Tours(Tour tour)
        {
            _db.Tours.Add(tour);
            _db.SaveChanges();

            return RedirectToAction("Tours");
        }
    }
}