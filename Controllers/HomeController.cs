using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CurrencyConvertor.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // Create the Http client
            HttpClient client = new HttpClient();


            string countryCode1 = "EUR";
            string countryCode2 = "USD";

            // Build the Http request string
            const string urlPattern = "http://finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=l1";
            string url = String.Format(urlPattern, countryCode1, countryCode2);

            // Send the GET request
            var response = client.GetAsync(url).Result;

            if (response != null && ((response.ReasonPhrase != "Unauthorized") && (response.ReasonPhrase != "Not Found")))
            {
                // Get the response string
                string strResponse = await response.Content.ReadAsStringAsync();
                decimal exchangeRate = decimal.Parse(strResponse);

                // Store it in the dictionnary for disply
                ViewData["exchange_rate"] = exchangeRate;
            }

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }


}
