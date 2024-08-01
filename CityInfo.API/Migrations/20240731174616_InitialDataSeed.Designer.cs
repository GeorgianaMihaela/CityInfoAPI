﻿// <auto-generated />
using CityInfo.API.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityInfo.API.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    [Migration("20240731174616_InitialDataSeed")]
    partial class InitialDataSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("CityInfo.API.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The one with Eiffel Tower",
                            Name = "Slobozia"
                        },
                        new
                        {
                            Id = 2,
                            Description = "The one with Trivale Forest",
                            Name = "Pitesti"
                        },
                        new
                        {
                            Id = 3,
                            Description = "The one with the nice center",
                            Name = "Brasov"
                        });
                });

            modelBuilder.Entity("CityInfo.API.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointsOfInterest");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Description = "Built by a baron",
                            Name = "Eiffel Tower"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 1,
                            Description = "Where you can walk",
                            Name = "City Center"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 2,
                            Description = "Walk in the forest",
                            Name = "Trivale Forest"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 2,
                            Description = "Walk around and bike",
                            Name = "Parcul Lunca Arges"
                        },
                        new
                        {
                            Id = 5,
                            CityId = 3,
                            Description = "The main attraction",
                            Name = "Piata Sfatului"
                        },
                        new
                        {
                            Id = 6,
                            CityId = 3,
                            Description = "You can hike",
                            Name = "Muntele Tampa"
                        });
                });

            modelBuilder.Entity("CityInfo.API.Entities.PointOfInterest", b =>
                {
                    b.HasOne("CityInfo.API.Entities.City", "City")
                        .WithMany("PointsOfInterest")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CityInfo.API.Entities.City", b =>
                {
                    b.Navigation("PointsOfInterest");
                });
#pragma warning restore 612, 618
        }
    }
}
