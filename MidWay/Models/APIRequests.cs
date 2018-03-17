using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace MidWay.Models
{
    public class APIRequests
    {
        public static async Task<List<Result>> GetLocation(string address)
        {
            address = address.Replace(" ", "+");
            var locationURL =
                "https://maps.googleapis.com/maps/api/geocode/json?address=" + address +
                "&key=AIzaSyABDqSOqOtqZIz7iaouakzvKNoymCthKv4";
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(locationURL).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var rootResult = JsonConvert.DeserializeObject<RootObject>(result);
                    return rootResult.results;
                }
                else
                {
                    return null;
                }
            }
        }
        public static async Task<List<Business>> GetBusiness(double lat, double lng, string Description)
        {
            var latitude = Math.Round(lat, 6);
            var longitude = Math.Round(lng, 6);
            var businessURL = "https://api.yelp.com/v3/businesses/search?term=" + Description + "&latitude=" + latitude + "&longitude=" + longitude + "&radius=32187";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "EDqXyZRFBzhS4jW1UYel1cUpQ1Nw8OalqL9YEeLpUAVK5noxoO2WjPZCo_NhwkguhH0v79o-cYqFyDfpSRUOtGRfwlXZvEWgFZVmk4kb034Swl2gXQ0oJOHpF0isWnYx");
                var response = client.GetAsync(businessURL).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var rootResult = JsonConvert.DeserializeObject<BusinessRootObject>(result);
                    return rootResult.businesses;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}