using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsPaymentSystem.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }

        public decimal Balance { get; set; }

        [MinLength(3)]
        public string BankName { get; set; }

        [MinLength(3)]
        public string SWIFTCode { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}
