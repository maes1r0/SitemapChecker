using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SitemapChecker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SitemapChecker.Data.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using SitemapChecker.Data.Models;

namespace SitemapChecker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ResultWebsitePerfomace result = new ResultWebsitePerfomace();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(UrlModel uri)
        {
            uri.GetSitemaps();
            result.WebsiteResponceTime = uri.GetResponceTime();
            return View("~/Views/Home/Result.cshtml", result);
        }
        public IActionResult Result(UrlModel uri)
        {
            return View(uri);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
