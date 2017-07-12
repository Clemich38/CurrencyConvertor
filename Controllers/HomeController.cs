using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;

using CurrencyConvertor.ViewModels;

namespace CurrencyConvertor.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;

        public HomeController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            string countryCode1 = "USD";
            string countryCode2 = "EUR";

            return View(await GetExRate(1, countryCode1, countryCode2));
        }

        [HttpPost]
        public async Task<IActionResult> GetRate(decimal value1, string countryCode1, string countryCode2)
        {
            if(countryCode1 == null)
                countryCode1 = "USD";
            
            if(countryCode2 == null)
                countryCode2 = "EUR";

            return View("Index", await GetExRate(value1, countryCode1, countryCode2));
        }

        
        public async Task<CurrencyModel> GetExRate(decimal value1, string countryCode1, string countryCode2)
        {
            // Create the Http client
            HttpClient client = new HttpClient();

            // Deserialize the string as an GeoIPRespContent object
            string filesPath = Path.Combine(_environment.WebRootPath, "res/Currency.json");
            string str = System.IO.File.ReadAllText(filesPath);

            CurrencyModel curModel = new CurrencyModel();

            curModel.currencyDic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Currency>>(str);

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
                curModel.countryCode1 = countryCode1;
                curModel.countryCode2 = countryCode2;
                curModel.exchangeRate = exchangeRate;
                curModel.value1 = value1;
                curModel.value2 = curModel.value1 * exchangeRate;
            }


            return curModel;
        }


        public IActionResult Error()
        {
            return View();
        }
    }


}
