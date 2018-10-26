using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodFinderIreland.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace FoodFinderIreland.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ImageURL,SuperDepartment,TPNB,ContentsMeasureType,Name,UnitOfSale,Description,AverageSellingUnitWeight,UnitQuantity,ContentsQuantity,Department,Price,UnitPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ImageURL,SuperDepartment,TPNB,ContentsMeasureType,Name,UnitOfSale,Description,AverageSellingUnitWeight,UnitQuantity,ContentsQuantity,Department,Price,UnitPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        

        public ActionResult NewSearch()
        {
            return View();
        }
        public async Task<ActionResult> MakeRequest(string q)// worked 
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "56ac439a92694577a2779f3d0ee0cd85");

            var uri = string.Format("https://dev.tescolabs.com/grocery/products/?query={0}&offset={1}&limit={2}", q, 0, 100);

            var response = await client.GetAsync(uri);            
            string body = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(body);
            
            IList<JToken> results = result["uk"]["ghs"]["products"]["results"].Children().ToList();

            //// serialize JSON results into .NET objects
            IList<Product> products = new List<Product>();
            foreach (JToken r in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
               Product product = r.ToObject<Product>();
               products.Add(product);
            }            
             return View(products);           
        }

    }
}



