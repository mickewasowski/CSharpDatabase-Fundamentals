using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BillsPaymentSystem.Models
{
    public class User
    {
        public User()
        {
            this.PaymentMethods = new List<PaymentMethod>();
        }

        public int UserId { get; set; }

        [MinLength(3)]
        public string FirstName { get; set; }

        [MinLength(3)]
        public string LastName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
