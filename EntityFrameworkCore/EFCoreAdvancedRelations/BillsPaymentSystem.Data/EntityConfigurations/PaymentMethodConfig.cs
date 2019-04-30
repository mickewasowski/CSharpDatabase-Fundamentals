using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.Data.EntityConfigurations
{
    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder
                .HasOne(pm => pm.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(pm => pm.UserId);

            builder
                .HasOne(pm => pm.BankAccount)
                .WithOne(b => b.PaymentMethod);

            builder
                .HasOne(pm => pm.CreditCard)
                .WithOne(c => c.PaymentMethod);
        }
    }
}
