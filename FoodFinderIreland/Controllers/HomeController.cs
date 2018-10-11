using System.Net.Http;
using System.Web.Mvc;
using System;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Collections.Generic;
using FoodFinderIreland.Models;
using System.Threading.Tasks;


namespace FoodFinderIreland.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is a page about our Shop.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our contact page.";

            return View();
        }
        public async Task<ActionResult> MakeRequest(string query)
        {
            //System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "");

            var uri = string.Format("https://dev.tescolabs.com/grocery/products/?query={0}&offset={1}&limit={2}", query, 0, 10);

            var response = await client.GetAsync(uri);
            Console.WriteLine(response.ToString());
            string body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);

            return View("query");
        }
    }
}
        //[HttpPost]
        // public ActionResult MakeRequest(string q) Carol         
        //Carol part
        //q = q + " hello";
        //return View("q");
     


