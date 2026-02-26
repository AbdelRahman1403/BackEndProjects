using System.Diagnostics;
using BLL.Interfaces;
using GymManagementPL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnalyticsService analyticsService;

        public HomeController(ILogger<HomeController> logger , IAnalyticsService analyticsService)
        {
            _logger = logger;
            this.analyticsService = analyticsService;
        }

        public IActionResult Index()
        {
            var data = analyticsService.GetAnalyticsData();
            return View(data);
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
