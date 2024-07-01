using Banking.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Persistence.PostgreSQL.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder.Property(cd => cd.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(a => a.AccountNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(a => a.Balance)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder
                .Property(a => a.CreationDate)
                .HasDefaultValueSql("timezone('utc', now())");

            builder
                .Property(a => a.UpdationDate);

            builder
                .Property(a => a.IsActive)
                .HasDefaultValue(1);

            builder
                .Property(a => a.IsDeleted)
                .HasDefaultValue(0);

            //builder.HasMany(a => a.Transactions)
            //    .WithOne(t => t.SourceAccount)
            //    .HasForeignKey(t => t.SourceAccountId);

            //builder.HasMany(a => a.Transactions)
            //    .WithOne(t => t.DestinationAccount)
            //    .HasForeignKey(t => t.DestinationAccountId);

            builder
                .ToTable("Account");
        }
    }
}
