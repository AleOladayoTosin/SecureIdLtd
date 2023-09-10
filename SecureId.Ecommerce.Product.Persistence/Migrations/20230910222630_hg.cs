using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SecureId.Ecommerce.Product.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class hg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CouponCode = table.Column<string>(type: "TEXT", nullable: true),
                    DiscountAmount = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount" },
                values: new object[,]
                {
                    { new Guid("c1bd2aa1-2363-4d79-b0d8-223f91ba8afc"), "200FF", 20.0 },
                    { new Guid("f0687a8f-c5fc-48a5-82d6-8988ffda42a8"), "100FF", 10.0 },
                    { new Guid("f81babe2-101a-443e-bef4-a8d37f9c94be"), "200FF", 30.0 }
                });

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3b204c5e-9f75-4960-9371-9a70d5397a07"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7c39c8ac-b7d0-4ecd-997c-27778caaad22"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a025fa45-9f50-48cf-91e2-a5906d4c23ce"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f7cb7038-5321-47b4-891c-22f92375fe30"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("3932fee2-c9bc-4d69-9ff3-23d9bdc6c1c3"), "Appetizer", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://dotnetmastery.blob.core.windows.net/mango/12.jpg", "Paneer Tikka", 13.99 },
                    { new Guid("575f6a2f-89d6-4592-9e56-3cef585289d9"), "Appetizer", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://dotnetmastery.blob.core.windows.net/mango/14.jpg", "Samosa", 15.0 },
                    { new Guid("850c348a-7c7d-4802-af98-eef8272cca60"), "Entree", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://dotnetmastery.blob.core.windows.net/mango/13.jpg", "Pav Bhaji", 15.0 },
                    { new Guid("e074e761-0f2b-4c36-87e4-2baa8bd1597a"), "Dessert", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://dotnetmastery.blob.core.windows.net/mango/11.jpg", "Sweet Pie", 10.99 }
                });
        }
    }
}
