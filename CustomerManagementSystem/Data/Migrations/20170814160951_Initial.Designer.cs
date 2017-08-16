using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CustomerManagementSystem.Data;
using CustomerManagementSystem.Domain;

namespace CustomerManagementSystem.Data.Migrations
{
    [DbContext(typeof(CustomerContext))]
    [Migration("20170814160951_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CustomerManagementSystem.Domain.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(1000);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<int>("OrderCount");

                    b.Property<int>("StateId");

                    b.Property<int>("Zip");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CustomerManagementSystem.Domain.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<decimal>("Price");

                    b.Property<string>("Product");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CustomerManagementSystem.Domain.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(2);

                    b.Property<string>("Name")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("CustomerManagementSystem.Domain.Customer", b =>
                {
                    b.HasOne("CustomerManagementSystem.Domain.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CustomerManagementSystem.Domain.Order", b =>
                {
                    b.HasOne("CustomerManagementSystem.Domain.Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");
                });
        }
    }
}
