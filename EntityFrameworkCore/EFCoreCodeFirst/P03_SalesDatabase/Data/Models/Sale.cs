using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    public class Sale
    {
        //public Sale()
        //{
        //    this.Products = new HashSet<Product>();
        //    this.Stores = new HashSet<Store>();
        //    this.Customers = new HashSet<Customer>();
        //}

        public int SaleId { get; set; }

        public DateTime Date { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        //public ICollection<Product> Products { get; set; }

        //public ICollection<Store> Stores { get; set; }

        //public ICollection<Customer> Customers { get; set; }
    }
}
