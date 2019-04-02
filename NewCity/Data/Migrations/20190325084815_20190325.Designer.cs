﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewCity.Data;

namespace NewCity.Data.Migrations
{
    [DbContext(typeof(NewCityDbContext))]
    [Migration("20190325084815_20190325")]
    partial class _20190325
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("NewCity.Models.CharacterItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<Guid>("CharacterID");

                    b.Property<Guid>("ItemID");

                    b.HasKey("ID");

                    b.ToTable("CharacterItem");
                });

            modelBuilder.Entity("NewCity.Models.CharacterLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CharacterID");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Msg");

                    b.Property<Guid>("StoryID");

                    b.HasKey("ID");

                    b.ToTable("CharacterLog");
                });

            modelBuilder.Entity("NewCity.Models.CharacterSchedule", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CharacterID");

                    b.Property<bool>("IsMain");

                    b.Property<bool>("IsStory");

                    b.Property<Guid>("StoryCardID");

                    b.Property<Guid>("StorySeriesID");

                    b.Property<Guid?>("UserCharacterID");

                    b.HasKey("ID");

                    b.HasIndex("UserCharacterID");

                    b.ToTable("CharacterSchedule");
                });

            modelBuilder.Entity("NewCity.Models.Creator", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.ToTable("Creator");
                });

            modelBuilder.Entity("NewCity.Models.Item", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Coordinate");

                    b.Property<string>("Effect");

                    b.Property<string>("Introduction");

                    b.Property<string>("Name");

                    b.Property<float>("Price");

                    b.HasKey("ID");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("NewCity.Models.ItemLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CharacterID");

                    b.Property<Guid>("ItemID");

                    b.Property<string>("Msg");

                    b.HasKey("ID");

                    b.ToTable("ItemLog");
                });

            modelBuilder.Entity("NewCity.Models.Location", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Coordinate");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Img");

                    b.Property<string>("Introduction");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("NewCity.Models.StoryCard", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BackgroundIMG");

                    b.Property<string>("IMG");

                    b.Property<string>("StoryName");

                    b.Property<Guid>("StorySeriesID");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("StorySeriesID");

                    b.ToTable("StoryCard");
                });

            modelBuilder.Entity("NewCity.Models.StoryOption", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Condition");

                    b.Property<string>("Effect");

                    b.Property<Guid>("NextStoryCardID");

                    b.Property<Guid>("StoryCardID");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("StoryCardID");

                    b.ToTable("StoryOption");
                });

            modelBuilder.Entity("NewCity.Models.StorySeries", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("Author");

                    b.Property<DateTime>("Creationdate");

                    b.Property<string>("IMG");

                    b.Property<bool>("IsPlayed");

                    b.Property<bool>("IsTest");

                    b.Property<Guid>("LocationID");

                    b.Property<string>("SeriesName");

                    b.Property<string>("Text");

                    b.Property<string>("flag");

                    b.HasKey("ID");

                    b.HasIndex("LocationID");

                    b.ToTable("StorySeries");
                });

            modelBuilder.Entity("NewCity.Models.StoryStatus", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("StorySeries");

                    b.Property<string>("Type");

                    b.Property<string>("Value");

                    b.HasKey("ID");

                    b.ToTable("StoryStatus");
                });

            modelBuilder.Entity("NewCity.Models.UserCharacter", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("ActionPoints");

                    b.Property<string>("CharacterName");

                    b.Property<float>("Experience");

                    b.Property<float>("Intelligence");

                    b.Property<bool>("IsActivate");

                    b.Property<float>("Lucky");

                    b.Property<float>("Moral");

                    b.Property<float>("Speed");

                    b.Property<float>("Status");

                    b.Property<float>("Strength");

                    b.Property<Guid>("UserId");

                    b.HasKey("ID");

                    b.ToTable("UserCharacter");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NewCity.Models.CharacterSchedule", b =>
                {
                    b.HasOne("NewCity.Models.UserCharacter")
                        .WithMany("CharacterSchedules")
                        .HasForeignKey("UserCharacterID");
                });

            modelBuilder.Entity("NewCity.Models.StoryCard", b =>
                {
                    b.HasOne("NewCity.Models.StorySeries")
                        .WithMany("StoryCards")
                        .HasForeignKey("StorySeriesID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NewCity.Models.StoryOption", b =>
                {
                    b.HasOne("NewCity.Models.StoryCard")
                        .WithMany("StoryOptions")
                        .HasForeignKey("StoryCardID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NewCity.Models.StorySeries", b =>
                {
                    b.HasOne("NewCity.Models.Location")
                        .WithMany("StorySeries")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
