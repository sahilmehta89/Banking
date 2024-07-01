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
    public class ContactDetailsConfiguration : IEntityTypeConfiguration<ContactDetails>
    {
        public void Configure(EntityTypeBuilder<ContactDetails> builder)
        {
            builder
                .HasKey(cd => cd.Id);

            builder.Property(cd => cd.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(cd => cd.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(cd => cd.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(cd => cd.CreationDate)
                .HasDefaultValueSql("timezone('utc', now())");

            builder
                .Property(cd => cd.UpdationDate);

            builder
                .Property(cd => cd.IsActive)
                .HasDefaultValue(1);

            builder
                .Property(cd => cd.IsDeleted)
                .HasDefaultValue(0);

            builder
                .ToTable("ContactDetail");
        }
    }
}
