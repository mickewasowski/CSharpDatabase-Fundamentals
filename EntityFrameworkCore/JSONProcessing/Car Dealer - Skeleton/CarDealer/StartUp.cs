namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using CarDealer.Data;
    using CarDealer.Models;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //1
            //var jsonFile = File.ReadAllText(@"C:\MVSProjects\JSONProcessing\Car Dealer - Skeleton\CarDealer\Datasets\suppliers.json");
            //var result = ImportSuppliers(context, jsonFile);

            //Console.WriteLine(result);

            //2
            //var jsonFile = File.ReadAllText(@"C:\MVSProjects\JSONProcessing\Car Dealer - Skeleton\CarDealer\Datasets\parts.json");

            //var result = ImportParts(context, jsonFile);

            //Console.WriteLine(result);

            //3
            //var jsonFile = File.ReadAllText(@"C:\MVSProjects\JSONProcessing\Car Dealer - Skeleton\CarDealer\Datasets\cars.json");

            //var result = ImportCars(context, jsonFile);

            //Console.WriteLine(result);

            //4
            //var jsonFile = File.ReadAllText(@"C:\MVSProjects\JSONProcessing\Car Dealer - Skeleton\CarDealer\Datasets\customers.json");

            //var result = ImportCustomers(context, jsonFile);

            //Console.WriteLine(result);

            //5
            //var jsonFile = File.ReadAllText(@"C:\MVSProjects\JSONProcessing\Car Dealer - Skeleton\CarDealer\Datasets\sales.json");

            //var result = ImportSales(context, jsonFile);

            //Console.WriteLine(result);

            //6
            //Console.WriteLine(GetOrderedCustomers(context));

            //7
            //Console.WriteLine(GetCarsFromMakeToyota(context));

            //8
            //Console.WriteLine(GetLocalSuppliers(context));

            //9
            //Console.WriteLine(GetCarsWithTheirListOfParts(context));

            //10
            //Console.WriteLine(GetTotalSalesByCustomer(context));

            //11
            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);
            var count = context.SaveChanges();

            return $"Successfully imported {count}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<Part[]>(inputJson)
                .Where(x => context.Suppliers.Any(s => s.Id == x.SupplierId))
                .ToArray();

            context.Parts.AddRange(parts);
            var count = context.SaveChanges();

            return $"Successfully imported {count}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);
            var count = context.SaveChanges();

            return $"Successfully imported {count}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<Car[]>(inputJson);

            context.AddRange(cars);
            var count = context.SaveChanges();

            return $"Successfully imported {count}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);
            var count = context.SaveChanges();

            return $"Successfully imported {count}.";
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(b => b.BirthDate).ThenByDescending(c => c.IsYoungDriver)
                .Select(x => new
                {
                    Name = x.Name,
                    BirthDate = $"{x.BirthDate.ToString("dd/MM/yyyy")}",
                    IsYoungDriver = x.IsYoungDriver
            }).ToArray();

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return json;
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars.Where(x => x.Make == "Toyota").OrderBy(x => x.Model).ThenByDescending(x => x.TravelledDistance).Select(x => new {
                Id = x.Id,
                Make = x.Make,
                Model = x.Model,
                TravellDistance = x.TravelledDistance
            }).ToArray();

            var json = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return json;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context
                .Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
            .ToArray();

            var json = JsonConvert.SerializeObject(suppliers,Formatting.Indented);

            return json;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsAndParts = context.Cars/*.Include(pc => pc.PartCars.Select(x => x.Part))*/.Select(c => new
            {
                car = new
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                },
                parts = c.PartCars.Select(pc => new
                {
                    Name = pc.Part.Name,
                    Price = $"{pc.Part.Price:f2}"
                }).ToArray()
            }).ToArray();

            var json = JsonConvert.SerializeObject(carsAndParts, Formatting.Indented);

            return json;
        }

        //public static string GetTotalSalesByCustomer(CarDealerContext context)
        //{
        //    var salesByCustomers = context.Customers.Where(c => c.Sales.Count >= 1).Select(c => new
        //    {
        //        FullName = c.Name,
        //        BoughtCars = c.Sales.Count,
        //        SpentMoney = $"{c.Sales.Select(x => x.Car.PartCars.Sum(y => y.Part.Price)):f2}"
        //    })
        //    .OrderByDescending(x => x.SpentMoney)
        //    .ThenByDescending(x => x.BoughtCars)
        //    .ToArray();

        //    DefaultContractResolver contractResolver = new DefaultContractResolver()
        //    {
        //        NamingStrategy = new CamelCaseNamingStrategy()
        //    };

        //    var json = JsonConvert.SerializeObject(salesByCustomers, new JsonSerializerSettings()
        //    {
        //        ContractResolver = contractResolver,
        //        Formatting = Formatting.Indented
        //    });

        //    return json;
        //}

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var carParts =
                context
                    .Customers
                    .Where(x => x.Sales.Count >= 1)
                    .Select(x => new
                    {
                        fullName = x.Name,
                        boughtCars = x.Sales.Count,
                        spentMoney = x.Sales.Sum(a => a.Car.PartCars.Sum(z => z.Part.Price))

                    })
                    .OrderByDescending(x => x.boughtCars)
                    //.ThenByDescending(x => x.boughtCars)
                    .ToArray();
            ;

            var json = JsonConvert.SerializeObject(carParts, Formatting.Indented);
            return json;
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            //doesn't work


            var salesWithDiscount = context
                .Sales
                .Take(10)
                .Where(x => x.Car != null && x.Customer != null)
                .Select(s => new
                {
                    car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = $"{s.Discount:f2}",
                    price = s.Car.PartCars.Select(x => new
                    {
                        price = $"{x.Part.Price * x.Part.Quantity:f2}"
                    }),
                    priceWithDiscount = s.Car.PartCars.Select(x => new
                    {
                        priceWithDiscount = (x.Part.Price * x.Part.Quantity) - (x.Part.Price * x.Part.Quantity) * s.Discount /100
                    })
                }).ToArray();

            var json = JsonConvert.SerializeObject(salesWithDiscount, Formatting.Indented);

            return json;

            //var getSales =
            //    context
            //        .Cars

            //        .Select(x => new
            //        {
            //            Make = x.Make,
            //            Model = x.Model,
            //            TravelledDistance = x.TravelledDistance,
            //            customerName = x.Sales.Select(s => new
            //            {
            //                customerName = s.Customer.Name,
            //                Discount = s.Discount,
            //                Price = s.Car.PartCars.Select(d => new
            //                {
            //                    price = d.Part.Quantity * d.Part.Price
            //                })
            //            })
            //        })
            //        .ToArray();
            //;

            //var json = JsonConvert.SerializeObject(getSales, Formatting.Indented);
            //return json;
        }
    }
}