using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MidWay.Models;
using Newtonsoft.Json;

namespace MidWay.Controllers
{
    public class HomeController : Controller
    {
        private Location midPoint;
        private string Address1;
        private string Address2;
        private List<Business> Businesses;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {

            var location1 = formCollection["location1"];
            var location2 = formCollection["location2"];
            var Description = formCollection["description"];

            if (!string.IsNullOrEmpty(location1) && !string.IsNullOrEmpty(location2))
            {
                var result = LocationRequest(location1, location2);
                ViewBag.MidLat = midPoint.lat;
                ViewBag.MidLng = midPoint.lng;

                var result2 = BusinessRequest(midPoint.lat, midPoint.lng, Description);
            }

            ViewBag.Address1 = location1;
            ViewBag.Address2 = location2;
            ViewBag.Description = Description;


            ViewBag.Businesses = JsonConvert.SerializeObject(Businesses);

            return View(Businesses);
        }

        async Task LocationRequest(string address1, string address2)
        {
            List<Result> result1 = await APIRequests.GetLocation(address1);
            List<Result> result2 = await APIRequests.GetLocation(address2);

            var calc = new Calculations();

            var midpoint = calc.Midpoint(result1.First().geometry.location, result2.First().geometry.location);
            midPoint = midpoint;
        }
        async Task BusinessRequest(double lat, double lng, string Description)
        {
            List<Business> result = await APIRequests.GetBusiness(lat,lng,Description);
            Businesses = result;
        }
    }
}