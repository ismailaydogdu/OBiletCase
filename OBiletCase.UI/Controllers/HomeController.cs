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
            //TODO: Buraya gerçekten bir ip al !!!!!!!!!!!!!!!
            var clientIp = "159.146.41.195";

            CancellationToken cancellationToken = CancellationToken.None;
            if (string.IsNullOrEmpty(_sessionId))
            {
                var sessionResult = await _oBiletClient.GetSession(clientIp!, cancellationToken);
                if (sessionResult != null && sessionResult.Status == "Success")
                {
                    _sessionId = sessionResult!.Data!.SessionId!;
                    _deviceId = sessionResult!.Data!.DeviceId!;
                }
            }
            var result = await _oBiletClient.GetBusLocations(_sessionId, _deviceId, cancellationToken);
            return View(result);
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