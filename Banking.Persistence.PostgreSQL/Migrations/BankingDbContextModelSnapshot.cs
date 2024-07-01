﻿// <auto-generated />
using System;
using Banking.Persistence.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Banking.Persistence.PostgreSQL.Migrations
{
    [DbContext(typeof(BankingDbContext))]
    partial class BankingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Banking.Core.Model.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("timezone('utc', now())");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("UpdationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Account", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountNumber = "D5625316",
                            Balance = 1000m,
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = false,
                            IsDeleted = false,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            AccountNumber = "D5633319",
                            Balance = 2000m,
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = false,
                            IsDeleted = false,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Banking.Core.Model.ContactDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("timezone('utc', now())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime?>("UpdationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ContactDetail", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "JohnSmith@gmail.com",
                            IsActive = false,
                            IsDeleted = false,
                            PhoneNumber = "123456",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "Ben237@gmail.com",
                            IsActive = false,
                            IsDeleted = false,
                            PhoneNumber = "123456888",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Banking.Core.Model.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("timezone('utc', now())");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("DestinationAccountId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<int>("SourceAccountId")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("UpdationDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAccountId");

                    b.HasIndex("SourceAccountId");

                    b.ToTable("Transaction", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 50m,
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Date = new DateTime(2024, 6, 28, 5, 57, 55, 426, DateTimeKind.Utc).AddTicks(4774),
                            Description = "Test",
                            DestinationAccountId = 2,
                            IsActive = false,
                            IsDeleted = false,
                            SourceAccountId = 1,
                            Type = "Debit"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 60m,
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Date = new DateTime(2024, 6, 28, 5, 57, 55, 426, DateTimeKind.Utc).AddTicks(4807),
                            Description = "Test",
                            DestinationAccountId = 1,
                            IsActive = false,
                            IsDeleted = false,
                            SourceAccountId = 2,
                            Type = "Credit"
                        });
                });

            modelBuilder.Entity("Banking.Core.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("timezone('utc', now())");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<DateTime?>("UpdationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = false,
                            IsDeleted = false,
                            PasswordHash = new byte[] { 189, 199, 68, 88, 166, 29, 216, 98, 128, 28, 171, 24, 152, 172, 169, 89, 206, 191, 206, 68, 242, 198, 247, 242, 242, 231, 137, 46, 86, 108, 144, 253, 227, 101, 127, 173, 221, 156, 55, 217, 38, 122, 101, 150, 17, 83, 232, 117, 112, 12, 110, 226, 74, 62, 135, 250, 191, 199, 10, 162, 75, 151, 218, 226 },
                            PasswordSalt = new byte[] { 103, 115, 217, 81, 203, 225, 203, 192, 108, 71, 231, 77, 69, 200, 159, 218, 121, 129, 61, 175, 4, 113, 99, 243, 183, 76, 123, 204, 247, 15, 59, 168, 92, 64, 209, 67, 218, 31, 156, 18, 245, 4, 170, 164, 106, 250, 249, 149, 158, 168, 131, 248, 100, 235, 147, 203, 193, 70, 198, 204, 119, 10, 170, 29, 194, 140, 154, 233, 68, 221, 13, 147, 246, 149, 169, 192, 171, 174, 53, 126, 77, 2, 140, 61, 107, 100, 165, 17, 192, 22, 13, 44, 26, 185, 124, 220, 227, 125, 118, 121, 111, 39, 196, 121, 19, 162, 99, 39, 1, 225, 252, 204, 218, 254, 194, 148, 8, 131, 168, 72, 218, 152, 127, 12, 39, 81, 243, 11 },
                            Username = "JohnSmith236"
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = false,
                            IsDeleted = false,
                            PasswordHash = new byte[] { 189, 199, 68, 88, 166, 29, 216, 98, 128, 28, 171, 24, 152, 172, 169, 89, 206, 191, 206, 68, 242, 198, 247, 242, 242, 231, 137, 46, 86, 108, 144, 253, 227, 101, 127, 173, 221, 156, 55, 217, 38, 122, 101, 150, 17, 83, 232, 117, 112, 12, 110, 226, 74, 62, 135, 250, 191, 199, 10, 162, 75, 151, 218, 226 },
                            PasswordSalt = new byte[] { 103, 115, 217, 81, 203, 225, 203, 192, 108, 71, 231, 77, 69, 200, 159, 218, 121, 129, 61, 175, 4, 113, 99, 243, 183, 76, 123, 204, 247, 15, 59, 168, 92, 64, 209, 67, 218, 31, 156, 18, 245, 4, 170, 164, 106, 250, 249, 149, 158, 168, 131, 248, 100, 235, 147, 203, 193, 70, 198, 204, 119, 10, 170, 29, 194, 140, 154, 233, 68, 221, 13, 147, 246, 149, 169, 192, 171, 174, 53, 126, 77, 2, 140, 61, 107, 100, 165, 17, 192, 22, 13, 44, 26, 185, 124, 220, 227, 125, 118, 121, 111, 39, 196, 121, 19, 162, 99, 39, 1, 225, 252, 204, 218, 254, 194, 148, 8, 131, 168, 72, 218, 152, 127, 12, 39, 81, 243, 11 },
                            Username = "Ben237"
                        });
                });

            modelBuilder.Entity("Banking.Core.Model.Account", b =>
                {
                    b.HasOne("Banking.Core.Model.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Banking.Core.Model.ContactDetails", b =>
                {
                    b.HasOne("Banking.Core.Model.User", "User")
                        .WithOne("ContactDetails")
                        .HasForeignKey("Banking.Core.Model.ContactDetails", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Banking.Core.Model.Transaction", b =>
                {
                    b.HasOne("Banking.Core.Model.Account", "DestinationAccount")
                        .WithMany()
                        .HasForeignKey("DestinationAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Banking.Core.Model.Account", "SourceAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("SourceAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DestinationAccount");

                    b.Navigation("SourceAccount");
                });

            modelBuilder.Entity("Banking.Core.Model.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Banking.Core.Model.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("ContactDetails")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
