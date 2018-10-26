using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodFinderIreland.Models;

namespace FoodFinderIreland.Controllers
{
    public class ShoppingListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingList
        public ActionResult Index()
        {
            return View(db.ShoppingLists.ToList());
        }

        // GET: ShoppingList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList shoppingList = db.ShoppingLists.Find(id);
            if (shoppingList == null)
            {
                return HttpNotFound();
            }
            return View(shoppingList);
        }

        // GET: ShoppingList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShoppingListId,ListName")] ShoppingList shoppingList)
        {
            if (ModelState.IsValid)
            {
                shoppingList.Added = DateTime.Today;              
                db.ShoppingLists.Add(shoppingList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoppingList);
        }

        // GET: ShoppingList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList shoppingList = db.ShoppingLists.Find(id);
            if (shoppingList == null)
            {
                return HttpNotFound();
            }
            return View(shoppingList);
        }

        // POST: ShoppingList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShoppingListId,ListName,Added")] ShoppingList shoppingList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoppingList);
        }

        // GET: ShoppingList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList shoppingList = db.ShoppingLists.Find(id);
            if (shoppingList == null)
            {
                return HttpNotFound();
            }
            return View(shoppingList);
        }

        // POST: ShoppingList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingList shoppingList = db.ShoppingLists.Find(id);
            db.ShoppingLists.Remove(shoppingList);
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

        // GET: ShoppingList/SelectProduct
        //public ActionResult SelectProduct(Product product)
        //{
        //    if (product == first)
        //    {
        //        return View("Create");
        //    }
        //    // transform the Product to an item and save the item to the database
        //    // add the item to the shop list
        //    // add pop up to check if it is a new shopping list or a closed shopping list
        //    return View();
        //}
    }
}
