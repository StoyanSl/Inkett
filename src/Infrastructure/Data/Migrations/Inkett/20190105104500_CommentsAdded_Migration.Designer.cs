﻿// <auto-generated />
using System;
using Inkett.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inkett.Infrastructure.Migrations.Inkett
{
    [DbContext(typeof(InkettContext))]
    [Migration("20190105104500_CommentsAdded_Migration")]
    partial class CommentsAdded_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Inkett.ApplicationCore.Entitites.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlbumPictureUri");

                    b.Property<string>("Description");

                    b.Property<int>("ProfileId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Inkett.ApplicationCore.Entitites.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProfileId");

                    b.Property<int>("TattooId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("TattooId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Inkett.ApplicationCore.Entitites.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountId");

                    b.Property<string>("CoverPicture");

                    b.Property<string>("ProfileDescription");

                    b.Property<string>("ProfileName");

                    b.Property<string>("ProfilePicture");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Inkett.ApplicationCore.Entitites.Style", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Styles");
                });

            modelBuilder.Entity("Inkett.ApplicationCore.Entitites.Tattoo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumId");

                    b.Property<string>("Description");

                    b.Property<int>("ProfileId");

                    b.Property<string>("TattooPictureUri");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Tattoos");
                });

            modelBuilder.Entity("Inkett.ApplicationCore.Entitites.TattooStyle", b =>
                {
                    b.Property<int>("StyleId");

                    b.Property<int>("TattooId");

                    b.HasKey("StyleId", "TattooId");

                    b.HasIndex("TattooId");

                    b.ToTable("TattooStyles");
                });

            modelBuilder.Entity("Inkett.ApplicationCore.Entitites.Album", b =>
                {
                    b.HasOne("Inkett.ApplicationCore.Entitites.Profile")
                        .WithMany("Albums")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Inkett.ApplicationCore.Entitites.Comment", b =>
                {
                    b.HasOne("Inkett.ApplicationCore.Entitites.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Inkett.ApplicationCore.Entitites.Tattoo", "Tattoo")
                        .WithMany("Comments")
                        .HasForeignKey("TattooId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Inkett.ApplicationCore.Entitites.Tattoo", b =>
                {
                    b.HasOne("Inkett.ApplicationCore.Entitites.Album", "Album")
                        .WithMany("Tattoos")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Inkett.ApplicationCore.Entitites.Profile", "Profile")
                        .WithMany("Tattoos")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Inkett.ApplicationCore.Entitites.TattooStyle", b =>
                {
                    b.HasOne("Inkett.ApplicationCore.Entitites.Style", "Style")
                        .WithMany("TattooStyles")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Inkett.ApplicationCore.Entitites.Tattoo", "Tattoo")
                        .WithMany("TattooStyles")
                        .HasForeignKey("TattooId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
