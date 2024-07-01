using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Banking.Persistence.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    UpdationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AccountNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    UpdationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    UpdationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactDetail_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SourceAccountId = table.Column<int>(type: "integer", nullable: false),
                    DestinationAccountId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    UpdationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Account_DestinationAccountId",
                        column: x => x.DestinationAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Account_SourceAccountId",
                        column: x => x.SourceAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "UpdationDate", "Username" },
                values: new object[,]
                {
                    { 1, new byte[] { 189, 199, 68, 88, 166, 29, 216, 98, 128, 28, 171, 24, 152, 172, 169, 89, 206, 191, 206, 68, 242, 198, 247, 242, 242, 231, 137, 46, 86, 108, 144, 253, 227, 101, 127, 173, 221, 156, 55, 217, 38, 122, 101, 150, 17, 83, 232, 117, 112, 12, 110, 226, 74, 62, 135, 250, 191, 199, 10, 162, 75, 151, 218, 226 }, new byte[] { 103, 115, 217, 81, 203, 225, 203, 192, 108, 71, 231, 77, 69, 200, 159, 218, 121, 129, 61, 175, 4, 113, 99, 243, 183, 76, 123, 204, 247, 15, 59, 168, 92, 64, 209, 67, 218, 31, 156, 18, 245, 4, 170, 164, 106, 250, 249, 149, 158, 168, 131, 248, 100, 235, 147, 203, 193, 70, 198, 204, 119, 10, 170, 29, 194, 140, 154, 233, 68, 221, 13, 147, 246, 149, 169, 192, 171, 174, 53, 126, 77, 2, 140, 61, 107, 100, 165, 17, 192, 22, 13, 44, 26, 185, 124, 220, 227, 125, 118, 121, 111, 39, 196, 121, 19, 162, 99, 39, 1, 225, 252, 204, 218, 254, 194, 148, 8, 131, 168, 72, 218, 152, 127, 12, 39, 81, 243, 11 }, null, "JohnSmith236" },
                    { 2, new byte[] { 189, 199, 68, 88, 166, 29, 216, 98, 128, 28, 171, 24, 152, 172, 169, 89, 206, 191, 206, 68, 242, 198, 247, 242, 242, 231, 137, 46, 86, 108, 144, 253, 227, 101, 127, 173, 221, 156, 55, 217, 38, 122, 101, 150, 17, 83, 232, 117, 112, 12, 110, 226, 74, 62, 135, 250, 191, 199, 10, 162, 75, 151, 218, 226 }, new byte[] { 103, 115, 217, 81, 203, 225, 203, 192, 108, 71, 231, 77, 69, 200, 159, 218, 121, 129, 61, 175, 4, 113, 99, 243, 183, 76, 123, 204, 247, 15, 59, 168, 92, 64, 209, 67, 218, 31, 156, 18, 245, 4, 170, 164, 106, 250, 249, 149, 158, 168, 131, 248, 100, 235, 147, 203, 193, 70, 198, 204, 119, 10, 170, 29, 194, 140, 154, 233, 68, 221, 13, 147, 246, 149, 169, 192, 171, 174, 53, 126, 77, 2, 140, 61, 107, 100, 165, 17, 192, 22, 13, 44, 26, 185, 124, 220, 227, 125, 118, 121, 111, 39, 196, 121, 19, 162, 99, 39, 1, 225, 252, 204, 218, 254, 194, 148, 8, 131, 168, 72, 218, 152, 127, 12, 39, 81, 243, 11 }, null, "Ben237" }
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "AccountNumber", "Balance", "UpdationDate", "UserId" },
                values: new object[,]
                {
                    { 1, "D5625316", 1000m, null, 1 },
                    { 2, "D5633319", 2000m, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "ContactDetail",
                columns: new[] { "Id", "Email", "PhoneNumber", "UpdationDate", "UserId" },
                values: new object[,]
                {
                    { 1, "JohnSmith@gmail.com", "123456", null, 1 },
                    { 2, "Ben237@gmail.com", "123456888", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "Id", "Amount", "Date", "Description", "DestinationAccountId", "SourceAccountId", "Type", "UpdationDate" },
                values: new object[,]
                {
                    { 1, 50m, new DateTime(2024, 6, 28, 5, 57, 55, 426, DateTimeKind.Utc).AddTicks(4774), "Test", 2, 1, "Debit", null },
                    { 2, 60m, new DateTime(2024, 6, 28, 5, 57, 55, 426, DateTimeKind.Utc).AddTicks(4807), "Test", 1, 2, "Credit", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetail_UserId",
                table: "ContactDetail",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_DestinationAccountId",
                table: "Transaction",
                column: "DestinationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_SourceAccountId",
                table: "Transaction",
                column: "SourceAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactDetail");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
