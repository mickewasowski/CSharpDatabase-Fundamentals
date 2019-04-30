using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models;
using BillsPaymentSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BillsPaymentSystem.App
{
    public class DbInitializer
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            SeedUsers(context);
            SeedCreditCards(context);
            SeedBankAccounts(context);
            SeedPaymentMethods(context);
        }

        private static void SeedPaymentMethods(BillsPaymentSystemContext context)
        {
            int[] userIds = {1,2,7,0,3 };
            int[] bankIds = {0,1,0,3,4 };
            int[] creditCardIds = { 1, 0, 5, 0, 3 };

            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

            for (int i = 0; i < userIds.Length; i++)
            {
                var paymentMethod = new PaymentMethod
                {
                    Type =(PaymentMethodType) new Random().Next(0,2),
                    UserId = userIds[i],
                    BankAccountId = bankIds[i],
                    CreditCardId = creditCardIds[i]
                };

                if (!IsValid(paymentMethod))
                {
                    continue;
                }

                paymentMethods.Add(paymentMethod);
            }

            context.PaymentMethods.AddRange(paymentMethods);
            context.SaveChanges();
        }

        private static void SeedBankAccounts(BillsPaymentSystemContext context)
        {
            decimal[] balances = {-50.00m,324m,7657m,-52.10m,325m };
            string[] bankNames =
            {
                "Dsk","Unicredit","Chushki",null,""
            };
            string[] swiftCodes = {"","41vgd","352fd",null,"chushki"};

            var bankAcounts = new List<BankAccount>();

            for (int i = 0; i < 5; i++)
            {
                var bankAccount = new BankAccount
                {
                    Balance = balances[i],
                    BankName = bankNames[i],
                    SWIFTCode = swiftCodes[i]
                };

                if (!IsValid(bankAccount))
                {
                    continue;
                }

                bankAcounts.Add(bankAccount);
            }

            context.BankAccounts.AddRange(bankAcounts);
            context.SaveChanges();
        }

        private static void SeedCreditCards(BillsPaymentSystemContext context)
        {
            decimal[] limits = {-12.02m,5.78m,0m,-58.23m,213.00m };
            decimal[] moneyOwned = { 25.20m, -85.21m, 152m, 556m, -85m };
            DateTime[] expirationDates =
            {
                new DateTime(2010,10,2),
                new DateTime(2020,3,2),
                new DateTime(2056,5,12),
                new DateTime(2015,3,13),
                new DateTime(1998,5,9)
            };

            List<CreditCard> creditCards = new List<CreditCard>();

            for (int i = 0; i < limits.Length; i++)
            {
                var creditCard = new CreditCard
                {
                    Limit = limits[i],
                    MoneyOwned = moneyOwned[i],
                    ExpirationDate = expirationDates[i]
                };

                if (!IsValid(creditCard))
                {
                    continue;
                }
                creditCards.Add(creditCard);
            }

            context.CreditCards.AddRange(creditCards);
            context.SaveChanges();
        }

        private static void SeedUsers(BillsPaymentSystemContext context)
        {
            string[] firstNames = {"Gosho","Pesho","Mimi",null,"" };
            string[] lastNames = {"Kirov","Goshev","Petrova","Chushki",null };
            string[] emails = {"gosho@kirov.com","pesho@gmail.com","mimi0201@abv.bg","ChuskiSDryjki",null };
            string[] passwords = {"gosgdfhg","piurch23657com","v45fdabvbg","Cc4538yjki",null };

            List<User> users = new List<User>();

            for (int i = 0; i < firstNames.Length; i++)
            {
                var user = new User
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    Email = emails[i],
                    Password = passwords[i]
                };

                if (!IsValid(user))
                {
                    continue;
                }

                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            return isValid;
        }
    }
}
