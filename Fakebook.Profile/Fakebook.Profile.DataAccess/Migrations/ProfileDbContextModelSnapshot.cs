﻿// <auto-generated />
using System;
using Fakebook.Profile.DataAccess.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fakebook.Profile.DataAccess.Migrations
{
    [DbContext(typeof(ProfileDbContext))]
    partial class ProfileDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Fakebook.Profile.DataAccess.EntityModel.EntityProfile", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("Email");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Email");

                    b.ToTable("Profile", "Fakebook");

                    b.HasData(
                        new
                        {
                            Email = "david.barnes@revature.net",
                            BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "David",
                            LastName = "Barnes",
                            ProfilePictureUrl = "https://images.unsplash.com/photo-1489533119213-66a5cd877091",
                            Status = "deployed my app feeling good about today's presentation"
                        },
                        new
                        {
                            Email = "testaccount@gmail.com",
                            BirthDate = new DateTime(1994, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Jay",
                            LastName = "Shin",
                            ProfilePictureUrl = "https://cdn.download.ams.birds.cornell.edu/api/v1/asset/252252921/1800"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
