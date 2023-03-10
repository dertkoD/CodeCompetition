// <auto-generated />
using System;
using BlazorApp1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorApp1.Migrations
{
    [DbContext(typeof(DB))]
    [Migration("20230202164738_InitDb")]
    partial class InitDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlazorApp1.Models.CategoryTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExtraPoints")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("ExtraTime")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryTask");
                });

            modelBuilder.Entity("BlazorApp1.Models.Competition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfSimultaneousTasks")
                        .HasColumnType("int");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Competition");
                });

            modelBuilder.Entity("BlazorApp1.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<Guid?>("CompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TaskCompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("TaskCompetitionId");

                    b.HasIndex("TeamId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BlazorApp1.Models.CustomerRole", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CustomerId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("CustomerRole");
                });

            modelBuilder.Entity("BlazorApp1.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("BlazorApp1.Models.StatusCompetition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StatusCompetition");
                });

            modelBuilder.Entity("BlazorApp1.Models.TaskCompetition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("CompletionTime")
                        .HasColumnType("time");

                    b.Property<string>("Decision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Point")
                        .HasColumnType("int");

                    b.Property<Guid?>("TeamBelongsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("TimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("TeamBelongsId");

                    b.ToTable("TaskCompetition");
                });

            modelBuilder.Entity("BlazorApp1.Models.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PhotoTeam")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Scores")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("BlazorApp1.Models.Competition", b =>
                {
                    b.HasOne("BlazorApp1.Models.StatusCompetition", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("BlazorApp1.Models.Customer", b =>
                {
                    b.HasOne("BlazorApp1.Models.Competition", null)
                        .WithMany("Admins")
                        .HasForeignKey("CompetitionId");

                    b.HasOne("BlazorApp1.Models.TaskCompetition", null)
                        .WithMany("CustomersPerformeTask")
                        .HasForeignKey("TaskCompetitionId");

                    b.HasOne("BlazorApp1.Models.Team", null)
                        .WithMany("Customers")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("BlazorApp1.Models.CustomerRole", b =>
                {
                    b.HasOne("BlazorApp1.Models.Customer", "CustomerUser")
                        .WithMany("Roles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorApp1.Models.Role", "Role")
                        .WithMany("Customers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerUser");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BlazorApp1.Models.TaskCompetition", b =>
                {
                    b.HasOne("BlazorApp1.Models.CategoryTask", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorApp1.Models.Competition", null)
                        .WithMany("Tasks")
                        .HasForeignKey("CompetitionId");

                    b.HasOne("BlazorApp1.Models.Team", "TeamBelongs")
                        .WithMany()
                        .HasForeignKey("TeamBelongsId");

                    b.Navigation("Category");

                    b.Navigation("TeamBelongs");
                });

            modelBuilder.Entity("BlazorApp1.Models.Team", b =>
                {
                    b.HasOne("BlazorApp1.Models.Competition", null)
                        .WithMany("Teams")
                        .HasForeignKey("CompetitionId");
                });

            modelBuilder.Entity("BlazorApp1.Models.Competition", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Tasks");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("BlazorApp1.Models.Customer", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("BlazorApp1.Models.Role", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("BlazorApp1.Models.TaskCompetition", b =>
                {
                    b.Navigation("CustomersPerformeTask");
                });

            modelBuilder.Entity("BlazorApp1.Models.Team", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
