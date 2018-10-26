using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FoodFinderIreland.Models
{
    public interface IProductsContext : IDisposable
    {
        DbSet<Item> Items { get; }
        DbSet<ShoppingList> ShoppingLists { get; }
        int SaveChanges();
        void MarkAsModified(Object item);

    }
}