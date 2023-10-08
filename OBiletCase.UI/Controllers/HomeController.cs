using Microsoft.AspNetCore.Mvc;
using OBiletCase.Service.Interfaces;
using OBiletCase.UI.Models;
using System.Diagnostics;
using System.Net;

namespace OBiletCase.UI.Controllers
{
    public class HomeController : Controller
    {
        string _sessionId = String.Empty;
        string _deviceId = String.Empty;
        private readonly ILogger<HomeController> _logger;
        private readonly IJourneyOfferClient _oBiletClient;
        public HomeController(ILogger<HomeController> logger, IJourneyOfferClient oBiletClient)
        {
            _logger = logger;
            _oBiletClient = oBiletClient;
        }


        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Expeditions(int originId, int destinationId, DateTime departureDate)
        {
            CancellationToken cancellationToken = CancellationToken.None;
            var getJourneysResult = await _oBiletClient.GetJourneys(originId, destinationId, departureDate, cancellationToken);
            return View();
        }

        [Route("get-bus-locations-list")]
        public async Task<IActionResult> BusLocationsList(string term)
        {
            CancellationToken cancellationToken = CancellationToken.None;
            var result = await _oBiletClient.GetBusLocations(term,cancellationToken);
            var returnData = result.Data.Select(s => new { id = s.Id, text = s.Name });
            return Json(returnData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}