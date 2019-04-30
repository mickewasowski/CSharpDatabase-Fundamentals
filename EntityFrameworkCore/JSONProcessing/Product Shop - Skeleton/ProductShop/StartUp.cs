namespace ProductShop
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using ProductShop.Data;
    using ProductShop.Dtos.Export;
    using ProductShop.Models;
    using System;
    using System.IO;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();

            //context.Database.EnsureCreated();

            //1
            //var usersJson = File.ReadAllText(@"C:\MVSProjects\JSONProcessing\Product Shop - Skeleton\ProductShop\Datasets\users.json");

            //var importedCount = ImportUsers(context, usersJson);

            //Console.WriteLine(importedCount);

            //2
            //var productsJson = File.ReadAllText(@"C:\MVSProjects\JSONProcessing\Product Shop - Skeleton\ProductShop\Datasets\products.json");

            //var imported = ImportProducts(context, productsJson);

            //Console.WriteLine(imported);

            //3
            //var categoryJson = File.ReadAllText(@"C:\MVSProjects\JSONProcessing\Product Shop - Skeleton\ProductShop\Datasets\categories.json");

            //var imported = ImportCategories(context, categoryJson);

            //Console.WriteLine(imported);

            //4
            //var categoryProductJson = File.ReadAllText(@"C:\MVSProjects\JSONProcessing\Product Shop - Skeleton\ProductShop\Datasets\categories-products.json");

            //var imported = ImportCategoryProducts(context, categoryProductJson);

            //Console.WriteLine(imported);

            //5
            //var exported = GetProductsInRange(context);

            //Console.WriteLine(exported);

            //6
            //var exported = GetSoldProducts(context);

            //Console.WriteLine(exported);

            //7
            //var exported = GetCategoriesByProductsCount(context);

            //Console.WriteLine(exported);

            //8
            var exported = GetUsersWithProducts(context);

            Console.WriteLine(exported);
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            User[] users = JsonConvert.DeserializeObject<User[]>(inputJson)
                .Where(u => u.LastName != null && u.LastName.Length >= 3)
                .ToArray();

            context.Users.AddRange(users);
            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson)
                .Where(p => p.Name != null && p.Name.Length >= 3)
                .ToArray();

            context.Products.AddRange(products);
            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(c => c.Name != null && c.Name.Length >= 3 && c.Name.Length <= 15)
                .ToArray();

            context.Categories.AddRange(categories);
            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoriesProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoriesProducts);
            var count = context.SaveChanges();

            return $"Successfully imported {count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Seller = /*p.Seller.FirstName == null ? p.Seller.LastName : */$"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .OrderBy(x => x.Price)
                .ToList();

            var json = JsonConvert.SerializeObject(products,Formatting.Indented);

            return json;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts = context
                .Users
                .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => 
                new
                 {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                    .Where(ps => ps.Buyer != null)
                    .Select(ps => new
                    {
                        Name = ps.Name,
                        Price = ps.Price,
                        BuyerFirstName = ps.Buyer.FirstName,
                        BuyerLastName = ps.Buyer.LastName
                    })
                    .ToArray()
                 })
            .ToArray();

            DefaultContractResolver contractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var json = JsonConvert.SerializeObject(soldProducts, new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            return json;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categoriesByProductsCount = context
                .Categories
                .OrderByDescending(pc => pc.CategoryProducts.Count)
                .Select(c =>
                new
                {
                    Category = c.Name,
                    ProductsCount = c.CategoryProducts.Count,
                    AveragePrice = $"{c.CategoryProducts.Select(x => x.Product.Price).Average():f2}",
                    TotalRevenue = $"{c.CategoryProducts.Select(pc => pc.Product.Price).Sum():f2}"
                })
                    .ToArray();

            DefaultContractResolver contractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var json = JsonConvert.SerializeObject(categoriesByProductsCount,new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            return json;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersWithProducts = context
                .Users
                .Where(u => u.ProductsSold.Any(pc => pc.Buyer != null))
                .Select(u => new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold.Select(k => new
                        {
                            Name = k.Name,
                            Price = k.Price
                        }).ToArray()
                    }

                })
                .OrderByDescending(x => x.SoldProducts.Count)
                .ToArray();

            var jsonReady = new
            {
                usersCount = usersWithProducts.Length,
                users = usersWithProducts
            };

            DefaultContractResolver contractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var json = JsonConvert.SerializeObject(jsonReady, new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            return json;
        }
    }
}