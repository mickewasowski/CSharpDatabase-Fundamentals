using AutoMapper;
using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ProductShopProfile>();
            });

            var usersXml = File.ReadAllText("../../../Datasets/users.xml");
            var productsXml = File.ReadAllText("../../../Datasets/products.xml");
            var categoriesXml = File.ReadAllText("../../../Datasets/categories.xml");
            var categoriesProductsXml = File.ReadAllText("../../../Datasets/categories-products.xml");

            using (ProductShopContext context = new ProductShopContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                //1
                //var usersResult = ImportUsers(context, usersXml);
                //Console.WriteLine(usersResult);

                //2
                //var productsResult = ImportProducts(context, productsXml);
                //Console.WriteLine(productsResult);

                //3
                //var categoriesResult = ImportCategories(context, categoriesXml);
                //Console.WriteLine(categoriesResult);

                //4
                //var result = ImportCategoryProducts(context, categoriesProductsXml);
                //Console.WriteLine(result);

                //5
                //var result = GetProductsInRange(context);
                //Console.WriteLine(result);

                //6
                //var result = GetSoldProducts(context);
                //Console.WriteLine(result);

                //7
                //var result = GetCategoriesByProductsCount(context);
                //Console.WriteLine(result);

                //8
                var result = GetUsersWithProducts(context);
                Console.WriteLine(result);
            }
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportUserDto[]), new XmlRootAttribute("Users"));

            var usersDto = (ImportUserDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var users = new List<User>();

            foreach (var userDto in usersDto)
            {
                var user = Mapper.Map<User>(userDto);
                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}"; ;
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            //FOR THE FOREIGN KEY MAKE THE ID NULLABLE !!!!!

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportProductsDto[]), new XmlRootAttribute("Products"));

            var productsDto = (ImportProductsDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var products = new List<Product>();

            foreach (var productDto in productsDto)
            {
                var product = Mapper.Map<Product>(productDto);
                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCategoriesDto[]), new XmlRootAttribute("Categories"));

            var categoriesDto = (ImportCategoriesDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var categories = new List<Category>();

            foreach (var categorieDto in categoriesDto)
            {
                if (categorieDto.Name == null)
                {
                    continue;
                }
                var category = Mapper.Map<Category>(categorieDto);

                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCategoriesProductsDto[]), new XmlRootAttribute("CategoryProducts"));

            var categoriesProductsDto = (ImportCategoriesProductsDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var categoriesProducts = new List<CategoryProduct>();

            foreach (var categoryProductDto in categoriesProductsDto)
            {
                var category = context.Categories.Find(categoryProductDto.CategoryId);
                var product = context.Products.Find(categoryProductDto.ProductId);

                if (category == null || product == null)
                {
                    continue;
                }
                var import = new CategoryProduct()
                {
                    CategoryId = categoryProductDto.CategoryId,
                    ProductId = categoryProductDto.ProductId
                };

                categoriesProducts.Add(import);
            }

            context.CategoryProducts.AddRange(categoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new ExportProductInRangeDto
                {
                    Name = x.Name,
                    Price = x.Price,
                    Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName
                })
                .OrderBy(x => x.Price)
                .Take(10)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportProductInRangeDto[]), new XmlRootAttribute("Products"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[]
            {
                new XmlQualifiedName("","")
            });

            xmlSerializer.Serialize(new StringWriter(sb), products, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(x => x.ProductsSold.Any())
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new ExportSoldProductsDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.ProductsSold.Select(s => new ProductDto
                    {
                        ProductName = s.Name,
                        Price = s.Price
                    }).ToArray()
                })
                .Take(5)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportSoldProductsDto[]), new XmlRootAttribute("Users"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[]
            {
                XmlQualifiedName.Empty
            });

            xmlSerializer.Serialize(new StringWriter(sb), users, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .Select(x => new CategoriesByProductsCountDto
                {
                    Name = x.Name,
                    Count = x.CategoryProducts.Count,
                    AverigePrice = x.CategoryProducts.Select(c => c.Product.Price).Average(),
                    TotalRevenue = Math.Round(x.CategoryProducts.Select(c => c.Product.Price).Sum(), 2)
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.TotalRevenue)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CategoriesByProductsCountDto[]), new XmlRootAttribute("Categories"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[]
            {
                XmlQualifiedName.Empty
            });

            xmlSerializer.Serialize(new StringWriter(sb), categories, namespaces);

            return sb.ToString().TrimEnd();

        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(x => x.ProductsSold.Any())
                .OrderByDescending(x => x.ProductsSold.Count)
                .Select(x => new UsersAndProductsDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    SoldProducts = new UsersAndProductsSoldProductsDto
                    {
                        Count = x.ProductsSold.Count,
                        Products = x.ProductsSold.Select(p => new ProductDto
                        {
                            ProductName = p.Name,
                            Price = Math.Round(p.Price, 2)
                        })
                        .OrderByDescending(n => n.Price)
                        .ToArray()
                    }
                })
                .Take(10)
                .ToArray();

            var customExport = new CustomExportUsersAndProductsDto
            {
                Count = context.Users.Count(x => x.ProductsSold.Any()),
                UsersAndProducts = users
            };

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomExportUsersAndProductsDto), new XmlRootAttribute("Users"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[]
            {
                XmlQualifiedName.Empty
            });

            xmlSerializer.Serialize(new StringWriter(sb), customExport, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}