﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spike;

#nullable disable

namespace Spike.Migrations
{
    [DbContext(typeof(SpikeContext))]
    [Migration("20230912025842_inital2")]
    partial class inital2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AuthorHistoricV1DocumentHistoricV1", b =>
                {
                    b.Property<Guid>("AuthorsHistoricV1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DocumentsHistoricV1Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AuthorsHistoricV1Id", "DocumentsHistoricV1Id");

                    b.HasIndex("DocumentsHistoricV1Id");

                    b.ToTable("AuthorHistoricV1DocumentHistoricV1");
                });

            modelBuilder.Entity("AuthorV1DocumentV1", b =>
                {
                    b.Property<Guid>("AuthorsV1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DocumentsV1Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AuthorsV1Id", "DocumentsV1Id");

                    b.HasIndex("DocumentsV1Id");

                    b.ToTable("AuthorV1DocumentV1");
                });

            modelBuilder.Entity("DATA.Repository.Implementation.HistoricEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PeriodEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PeriodStart")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("VersionTag")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.ToView("HistoricEntityHistory", (string)null);
                });

            modelBuilder.Entity("Spike.Models.AuthorV1", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("VersionTag")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AuthorsV1");
                });

            modelBuilder.Entity("Spike.Models.DocumentV1", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("VersionTag")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("DocumentsV1");
                });

            modelBuilder.Entity("Spike.Models.HistoricAuthorBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PeriodEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PeriodStart")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("VersionTag")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.ToView("HistoricAuthorBaseHistory", (string)null);
                });

            modelBuilder.Entity("Spike.Models.HistoricDocumentBase", b =>
                {
                    b.HasBaseType("DATA.Repository.Implementation.HistoricEntity");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToView("HistoricDocumentBaseHistory", (string)null);
                });

            modelBuilder.Entity("Spike.Models.AuthorHistoricV1", b =>
                {
                    b.HasBaseType("Spike.Models.HistoricAuthorBase");

                    b.ToView("AuthorHistoricV1History", (string)null);
                });

            modelBuilder.Entity("Spike.Models.DocumentHistoricV1", b =>
                {
                    b.HasBaseType("Spike.Models.HistoricDocumentBase");

                    b.ToView("DocumentHistoricV1History", (string)null);
                });

            modelBuilder.Entity("AuthorHistoricV1DocumentHistoricV1", b =>
                {
                    b.HasOne("Spike.Models.AuthorHistoricV1", null)
                        .WithMany()
                        .HasForeignKey("AuthorsHistoricV1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Spike.Models.DocumentHistoricV1", null)
                        .WithMany()
                        .HasForeignKey("DocumentsHistoricV1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AuthorV1DocumentV1", b =>
                {
                    b.HasOne("Spike.Models.AuthorV1", null)
                        .WithMany()
                        .HasForeignKey("AuthorsV1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Spike.Models.DocumentV1", null)
                        .WithMany()
                        .HasForeignKey("DocumentsV1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
