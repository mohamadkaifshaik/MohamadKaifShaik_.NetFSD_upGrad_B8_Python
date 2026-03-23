using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Mrp { get; set; }
}

class Program
{
    static void Main()
    {
        List<Product> products = new List<Product>()
        {
            new Product{Id=101,Name="Soap",Category="FMCG",Mrp=25},
            new Product{Id=102,Name="Rice",Category="Grain",Mrp=60},
            new Product{Id=103,Name="Shampoo",Category="FMCG",Mrp=120},
            new Product{Id=104,Name="Wheat",Category="Grain",Mrp=45},
            new Product{Id=105,Name="Oil",Category="FMCG",Mrp=150}
        };
        // category FMCG
        Console.WriteLine("1. FMCG Products");
        foreach (var p in products.Where(x => x.Category == "FMCG"))
            Console.WriteLine($"{p.Id} {p.Name} {p.Category} {p.Mrp}");

        // category Grain
        Console.WriteLine("\n2. Grain Products");
        foreach (var p in products.Where(x => x.Category == "Grain"))
            Console.WriteLine($"{p.Id} {p.Name} {p.Category} {p.Mrp}");

        // Id in Ascending
        Console.WriteLine("\n3. Sort by Id");
        foreach (var p in products.OrderBy(x => x.Id))
            Console.WriteLine($"{p.Id} {p.Name} {p.Category} {p.Mrp}");

        // Category in Ascending
        Console.WriteLine("\n4. Sort by Category");
        foreach (var p in products.OrderBy(x => x.Category))
            Console.WriteLine($"{p.Id} {p.Name} {p.Category} {p.Mrp}");


        // Mrp in Ascending
        Console.WriteLine("\n5. Sort by MRP Ascending");
        foreach (var p in products.OrderBy(x => x.Mrp))
            Console.WriteLine($"{p.Id} {p.Name} {p.Category} {p.Mrp}");

        // Mrp in Descending
        Console.WriteLine("\n6. Sort by MRP Descending");
        foreach (var p in products.OrderByDescending(x => x.Mrp))
            Console.WriteLine($"{p.Id} {p.Name} {p.Category} {p.Mrp}");

        // Group by Category
        Console.WriteLine("\n7. Group by Category");
        var g1 = products.GroupBy(x => x.Category);
        foreach (var g in g1)
        {
            Console.WriteLine("Category: " + g.Key);
            foreach (var p in g)
                Console.WriteLine($"{p.Name} {p.Mrp}");
        }

        // Group by Mrp
        Console.WriteLine("\n8. Group by MRP");
        var g2 = products.GroupBy(x => x.Mrp);
        foreach (var g in g2)
        {
            Console.WriteLine("MRP: " + g.Key);
            foreach (var p in g)
                Console.WriteLine($"{p.Name} {p.Category}");
        }

        // Highest Mrp in FMCG
        Console.WriteLine("\n9. Highest price FMCG product");
        var highest = products.Where(x => x.Category == "FMCG")
                              .OrderByDescending(x => x.Mrp)
                              .First();
        Console.WriteLine($"{highest.Name} {highest.Mrp}");

        // Total Products
        Console.WriteLine("\n10. Total Products");
        Console.WriteLine(products.Count());

        // Total Products in FMCG
        Console.WriteLine("\n11. FMCG Product Count");
        Console.WriteLine(products.Count(x => x.Category == "FMCG"));

        // Max Mrp
        Console.WriteLine("\n12. Max Price");
        Console.WriteLine(products.Max(x => x.Mrp));

        // Min Mrp
        Console.WriteLine("\n13. Min Price");
        Console.WriteLine(products.Min(x => x.Mrp));

        // All Products with Mrp lessthan 30
        Console.WriteLine("\n14. All products below Rs.30?");
        Console.WriteLine(products.All(x => x.Mrp < 30));

        // Any Product with Mrp lessthan 30
        Console.WriteLine("\n15. Any product below Rs.30?");
        Console.WriteLine(products.Any(x => x.Mrp < 30));


    }
}
