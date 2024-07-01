using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking.Core.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Banking.Core.Model.Enum;
using System.Reflection.Emit;

namespace Banking.Persistence.PostgreSQL.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder
                .Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(t => t.Type)
                .HasConversion(new EnumToStringConverter<TransactionType>())
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(t => t.Date)
                .IsRequired();

            builder
                .Property(t => t.CreationDate)
                .HasDefaultValueSql("timezone('utc', now())");

            builder
                .Property(t => t.UpdationDate);

            builder
                .Property(t => t.IsActive)
                .HasDefaultValue(1);

            builder
                .Property(t => t.IsDeleted)
                .HasDefaultValue(0);

            // Configure the relationship between Account and Transactions (Source)
            builder
                .HasOne(t => t.SourceAccount)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.SourceAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between Account and Transactions (Destination)
            builder
                .HasOne(t => t.DestinationAccount)
                .WithMany()
                .HasForeignKey(t => t.DestinationAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .ToTable("Transaction");
        }
    }
}
