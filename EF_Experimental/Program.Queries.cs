using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using EF_Experimental;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


partial class Program
{
    static void QueryingCategories()
    {
        using (Northwind db = new())
        {
            SectionTitle("Categories and how many products they have:");
            // a query to get all categories and their related products
            IQueryable<Category>? categories = db.Categories?.Include(c => c.Products);
            // If the condition was written as if (categories is null), it would only check if the categories variable is null, but it
            // would not check if it is empty.This could lead to an exception being thrown if the query is executed on an empty collection.
            if ((categories is null) || (!categories.Any()))
            {
                Fail("No categories found.");
                return;
            }
            // execute query and enumerate results
            foreach (Category c in categories)
            {
                Console.WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
            }
        }
    }

    static void FilteredIncludes()
    {
        using (Northwind db = new())
        {
            SectionTitle("Products with a minimum number of units in stock.");

            string? input;
            int stock;
            do
            {
                Console.Write("Enter a minimum for units in stock: ");
                input = Console.ReadLine();
            }
            while (!int.TryParse(input, out stock));
            IQueryable<Category>? categories = db.Categories?.Include(c => c.Products.Where(p => p.Stock >= stock));
            if ((categories is null) || (!categories.Any()))
            {
                Fail("No categories found.");
                return;
            }
            Info($"ToQueryString: {categories.ToQueryString()}");
            foreach (Category c in categories)
            {
                Console.WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum of {stock} units in stock.");
                foreach (Product p in c.Products)
                {
                    Console.WriteLine($"  {p.ProductName} has {p.Stock} units in stock.");
                }
            }
        }
    }

    static void QueryingProducts()
    {
        using (Northwind db = new())
        {
            SectionTitle("Products that cost more than a price, highest at top.");

            string? input;
            decimal price;
            do
            {
                Console.Write("Enter a product price: ");
                input = Console.ReadLine();
            } 
            while (!decimal.TryParse(input, out price));

            IQueryable<Product>? products = db.Products?
              .Where(product => product.Cost > price)
              .OrderByDescending(product => product.Cost);
            if ((products is null) || (!products.Any()))
            {
                Fail("No products found.");
                return;
            }
            Info($"ToQueryString: {products.ToQueryString()}");
            foreach (Product p in products)
            {
                Console.WriteLine("{0}: {1} costs {2:$#,##0.00} and has {3} in stock.", p.ProductId, p.ProductName, p.Cost, p.Stock);
            }
        }
    }
}