using Microsoft.AspNetCore.Mvc;
using OBiletCase.Service.Interfaces;
using OBiletCase.UI.Helpers;
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


            HomePageViewModel viewModel = new HomePageViewModel();
            viewModel.LastDestination = CookieHelper.GetObject<SelectBoxModel>(HttpContext, "lastDestination");
            viewModel.LastOrigin = CookieHelper.GetObject<SelectBoxModel>(HttpContext, "lastOrigin");
            return View(viewModel);
        }

        [Route("kesifler")]
        [HttpGet]
        public async Task<IActionResult> Expeditions(int originId, int destinationId, string originName, string destinationName, DateTime departureDate)
        {
            CookieHelper.SetCookie(HttpContext, "lastOrigin", new SelectBoxModel() { id = originId.ToString(), text = originName });
            CookieHelper.SetCookie(HttpContext, "lastDestination", new SelectBoxModel() { id = originId.ToString(), text = originName });
            ExpeditionsViewModel viewModel = new ExpeditionsViewModel();
            CancellationToken cancellationToken = CancellationToken.None;
            var getJourneysResult = await _oBiletClient.GetJourneys(originId, destinationId, departureDate, cancellationToken);
            if (getJourneysResult != null && getJourneysResult.Status.ToLower() == "success")
            {
                viewModel.IsSuccess = true;
                getJourneysResult.Data = getJourneysResult.Data;
                return View(viewModel);
            }
            else if (getJourneysResult != null)
            {
                viewModel.IsSuccess = false;
                viewModel.ErrorMessage = "Servis hatası => " + getJourneysResult.Message;
                return View(viewModel);
            }
            viewModel.IsSuccess = false;
            viewModel.ErrorMessage = "Bilinmeyen bir hata meydana geldi lütfen bizimle iletişime geçiniz";
            return View(viewModel);
        }

        [Route("get-bus-locations-list")]
        public async Task<IActionResult> BusLocationsList(string term)
        {
            CancellationToken cancellationToken = CancellationToken.None;
            var result = await _oBiletClient.GetBusLocations(term, cancellationToken);
            if (result != null)
            {
                var returnData = result.Data.Select(s => new SelectBoxModel { id = s.Id.ToString(), text = s.Name });
                return Json(returnData);
            }
            else { return Json(new List<SelectBoxModel>() { new SelectBoxModel() { id = "", text = "Api Erişim hatası" } }); }
        }


    }
}