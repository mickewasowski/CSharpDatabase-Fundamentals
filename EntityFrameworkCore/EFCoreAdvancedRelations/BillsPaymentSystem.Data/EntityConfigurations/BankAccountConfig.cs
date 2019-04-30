using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BillsPaymentSystem.Data.EntityConfigurations
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(b => b.BankAccountId);

            builder
                .Property(b => b.BankName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder
                .Property(b => b.SWIFTCode).HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}
