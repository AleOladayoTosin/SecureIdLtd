﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SecureId.Ecommerce.ShoppingCart.Persistence;

#nullable disable

namespace SecureId.Ecommerce.ShoppingCart.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230910223559_changedatatype")]
    partial class changedatatype
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("SecureId.Ecommerce.ShoppingCart.Domain.CartDetails", b =>
                {
                    b.Property<Guid>("CartDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CartHeaderId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.HasKey("CartDetailsId");

                    b.HasIndex("CartHeaderId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartDetails");
                });

            modelBuilder.Entity("SecureId.Ecommerce.ShoppingCart.Domain.CartHeader", b =>
                {
                    b.Property<Guid>("CartHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CouponCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("CartHeaderId");

                    b.ToTable("CartHeaders");
                });

            modelBuilder.Entity("SecureId.Ecommerce.ShoppingCart.Domain.OrderDetails", b =>
                {
                    b.Property<Guid>("OrderDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("OrderHeaderId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.HasKey("OrderDetailsId");

                    b.HasIndex("OrderHeaderId");

                    b.ToTable("orderDetails");
                });

            modelBuilder.Entity("SecureId.Ecommerce.ShoppingCart.Domain.OrderHeader", b =>
                {
                    b.Property<Guid>("OrderHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CVV")
                        .HasColumnType("TEXT");

                    b.Property<string>("CardNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("CartTotalItem")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CouponCode")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<double>("DiscountTotal")
                        .HasColumnType("REAL");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExpiryMonthYear")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<double>("OrderTotal")
                        .HasColumnType("REAL");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PickUpDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Retries")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("OrderHeaderId");

                    b.ToTable("orderHeaders");
                });

            modelBuilder.Entity("SecureId.Ecommerce.ShoppingCart.Domain.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoryName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SecureId.Ecommerce.ShoppingCart.Domain.CartDetails", b =>
                {
                    b.HasOne("SecureId.Ecommerce.ShoppingCart.Domain.CartHeader", "CartHeader")
                        .WithMany()
                        .HasForeignKey("CartHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SecureId.Ecommerce.ShoppingCart.Domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartHeader");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SecureId.Ecommerce.ShoppingCart.Domain.OrderDetails", b =>
                {
                    b.HasOne("SecureId.Ecommerce.ShoppingCart.Domain.OrderHeader", "OrderHeader")
                        .WithMany("orderDetails")
                        .HasForeignKey("OrderHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderHeader");
                });

            modelBuilder.Entity("SecureId.Ecommerce.ShoppingCart.Domain.OrderHeader", b =>
                {
                    b.Navigation("orderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
