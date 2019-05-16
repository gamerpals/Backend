﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using GamerPalsBackend.DataObjects.Models;

namespace GamerPalsBackend.Migrations
{
    [DbContext(typeof(PalsContext))]
    [Migration("20190225223033_ParameterFix")]
    partial class ParameterFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GamerPalsBackend.Models.ActiveSearch", b =>
                {
                    b.Property<int>("ActiveSearchID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("MaxPlayers");

                    b.Property<int?>("SearchTypeID");

                    b.Property<int?>("ServerGameServerID");

                    b.HasKey("ActiveSearchID");

                    b.HasIndex("SearchTypeID");

                    b.HasIndex("ServerGameServerID");

                    b.ToTable("ActiveSearches");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.ActiveSearchUser", b =>
                {
                    b.Property<int>("ActiveSearchID");

                    b.Property<int>("UserID");

                    b.HasKey("ActiveSearchID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("ActiveSearchUsers");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.Game", b =>
                {
                    b.Property<int>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentSearch");

                    b.Property<string>("GameName");

                    b.Property<int>("PlayersOnline");

                    b.HasKey("GameID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.GameServer", b =>
                {
                    b.Property<int>("GameServerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ServerGameGameID");

                    b.Property<string>("ServerName");

                    b.HasKey("GameServerID");

                    b.HasIndex("ServerGameGameID");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.Group", b =>
                {
                    b.Property<int>("GroupID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ActiveSearchID");

                    b.Property<int?>("LeaderUserID");

                    b.HasKey("GroupID");

                    b.HasIndex("ActiveSearchID");

                    b.HasIndex("LeaderUserID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.Language", b =>
                {
                    b.Property<int>("LanguageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LangLong");

                    b.Property<string>("LangShort");

                    b.HasKey("LanguageID");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.Parameter", b =>
                {
                    b.Property<int>("ParameterID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GameID");

                    b.Property<string>("ParameterName");

                    b.Property<string>("ParameterType");

                    b.HasKey("ParameterID");

                    b.HasIndex("GameID");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.SearchParameter", b =>
                {
                    b.Property<int>("ActiveSearchID");

                    b.Property<int>("ParameterID");

                    b.Property<string>("ParameterValue");

                    b.HasKey("ActiveSearchID", "ParameterID");

                    b.HasIndex("ParameterID");

                    b.ToTable("SearchParameters");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.SearchSettings", b =>
                {
                    b.Property<int>("SearchSettingsID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParameterID");

                    b.HasKey("SearchSettingsID");

                    b.HasIndex("ParameterID");

                    b.ToTable("SearchSettings");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.SearchType", b =>
                {
                    b.Property<int>("SearchTypeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SearchTypeName");

                    b.HasKey("SearchTypeID");

                    b.ToTable("SearchTypes");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.SystemSettings", b =>
                {
                    b.Property<int>("SystemSettingsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Setting");

                    b.Property<string>("SettingsName");

                    b.HasKey("SystemSettingsId");

                    b.ToTable("SystemSettings");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Age");

                    b.Property<string>("Country");

                    b.Property<int>("FacebookID");

                    b.Property<byte>("Gender");

                    b.Property<int>("GoogleID");

                    b.Property<int>("Karma");

                    b.Property<string>("Username");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.UserGame", b =>
                {
                    b.Property<int>("GameID");

                    b.Property<int>("UserID");

                    b.HasKey("GameID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("UserGame");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.UserLanguage", b =>
                {
                    b.Property<int>("LanguageID");

                    b.Property<int>("UserID");

                    b.HasKey("LanguageID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("UserLanguages");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.UserOptionRoles", b =>
                {
                    b.Property<int>("UserOptionId");

                    b.Property<int>("RoleId");

                    b.Property<int?>("UserOptionsID");

                    b.HasKey("UserOptionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserOptionsID");

                    b.ToTable("UserOptionRoles");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.UserOptions", b =>
                {
                    b.Property<int>("UserOptionsID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId");

                    b.HasKey("UserOptionsID");

                    b.ToTable("UserOptions");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.ActiveSearch", b =>
                {
                    b.HasOne("GamerPalsBackend.Models.SearchType", "SearchType")
                        .WithMany()
                        .HasForeignKey("SearchTypeID");

                    b.HasOne("GamerPalsBackend.Models.GameServer", "Server")
                        .WithMany()
                        .HasForeignKey("ServerGameServerID");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.ActiveSearchUser", b =>
                {
                    b.HasOne("GamerPalsBackend.Models.ActiveSearch", "ActiveSearch")
                        .WithMany("JoinedUsers")
                        .HasForeignKey("ActiveSearchID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GamerPalsBackend.Models.User", "User")
                        .WithMany("ActiveSearches")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GamerPalsBackend.Models.GameServer", b =>
                {
                    b.HasOne("GamerPalsBackend.Models.Game", "ServerGame")
                        .WithMany("Servers")
                        .HasForeignKey("ServerGameGameID");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.Group", b =>
                {
                    b.HasOne("GamerPalsBackend.Models.ActiveSearch", "ActiveSearch")
                        .WithMany()
                        .HasForeignKey("ActiveSearchID");

                    b.HasOne("GamerPalsBackend.Models.User", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderUserID");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.Parameter", b =>
                {
                    b.HasOne("GamerPalsBackend.Models.Game", "Game")
                        .WithMany("Parameters")
                        .HasForeignKey("GameID");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.SearchParameter", b =>
                {
                    b.HasOne("GamerPalsBackend.Models.ActiveSearch", "ActiveSearch")
                        .WithMany("Parameters")
                        .HasForeignKey("ActiveSearchID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GamerPalsBackend.Models.Parameter", "Parameter")
                        .WithMany()
                        .HasForeignKey("ParameterID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GamerPalsBackend.Models.SearchSettings", b =>
                {
                    b.HasOne("GamerPalsBackend.Models.Parameter", "Parameter")
                        .WithMany()
                        .HasForeignKey("ParameterID");
                });

            modelBuilder.Entity("GamerPalsBackend.Models.UserGame", b =>
                {
                    b.HasOne("GamerPalsBackend.Models.Game", "Game")
                        .WithMany("GameUsers")
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GamerPalsBackend.Models.User", "User")
                        .WithMany("UserGames")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GamerPalsBackend.Models.UserLanguage", b =>
                {
                    b.HasOne("GamerPalsBackend.Models.Language", "Language")
                        .WithMany("Users")
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GamerPalsBackend.Models.User", "User")
                        .WithMany("Languages")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GamerPalsBackend.Models.UserOptionRoles", b =>
                {
                    b.HasOne("GamerPalsBackend.Models.Role", "Role")
                        .WithMany("UserOptionRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GamerPalsBackend.Models.UserOptions", "UserOptions")
                        .WithMany("UserOptionRoles")
                        .HasForeignKey("UserOptionsID");
                });
#pragma warning restore 612, 618
        }
    }
}
