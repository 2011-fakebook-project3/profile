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
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Fakebook.Profile.DataAccess.EntityModel.EntityProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.ToTable("Profile", "Fakebook");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(1994, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "john.werner@revature.net",
                            FirstName = "John",
                            LastName = "Werner",
                            ProfilePictureUrl = "https://images.unsplash.com/photo-1489533119213-66a5cd877091",
                            Status = "deployed my app feeling good about today's presentation"
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(1994, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "testaccount@gmail.com",
                            FirstName = "Jay",
                            LastName = "Shin",
                            ProfilePictureUrl = "https://cdn.download.ams.birds.cornell.edu/api/v1/asset/252252921/1800"
                        });
                });

            modelBuilder.Entity("Fakebook.Profile.DataAccess.EntityModel.Follow", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("FollowingId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "FollowingId");

                    b.HasIndex("FollowingId");

                    b.ToTable("Follows");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            FollowingId = 2
                        },
                        new
                        {
                            UserId = 2,
                            FollowingId = 1
                        });
                });

            modelBuilder.Entity("Fakebook.Profile.DataAccess.EntityModel.Follow", b =>
                {
                    b.HasOne("Fakebook.Profile.DataAccess.EntityModel.EntityProfile", "Following")
                        .WithMany("Followers")
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fakebook.Profile.DataAccess.EntityModel.EntityProfile", "User")
                        .WithMany("Following")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Following");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fakebook.Profile.DataAccess.EntityModel.EntityProfile", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Following");
                });
#pragma warning restore 612, 618
        }
    }
}
