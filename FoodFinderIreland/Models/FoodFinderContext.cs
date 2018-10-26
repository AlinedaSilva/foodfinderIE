using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FoodFinderIreland.Models
{
    public class FoodFinderContext: DbContext, IProductsContext
    {
        public FoodFinderContext() : base("FoodFinderContext")
        {
        }
       // public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public void MarkAsModified(Object item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}