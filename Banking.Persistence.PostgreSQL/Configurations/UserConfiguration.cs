using Banking.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Persistence.PostgreSQL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);
            
            builder
                .Property(u => u.PasswordHash)
                .IsRequired();

            builder
                .Property(u => u.PasswordSalt)
                .IsRequired();

            builder
                .Property(u => u.CreationDate)
                .HasDefaultValueSql("timezone('utc', now())");

            builder
                .Property(u=> u.UpdationDate);

            builder
                .Property(u => u.IsActive)
                .HasDefaultValue(1);

            builder
                .Property(u => u.IsDeleted)
                .HasDefaultValue(0);

            builder.HasOne(u => u.ContactDetails)
                .WithOne(c => c.User)
                .HasForeignKey<ContactDetails>(cd => cd.UserId);

            builder.HasMany(u => u.Accounts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder
                .ToTable("User");
        }
    }
}
