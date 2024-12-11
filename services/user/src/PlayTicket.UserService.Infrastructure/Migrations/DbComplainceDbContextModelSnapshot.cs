﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlayTicket.UserService.EntityFrameworkCore.DbCompliance;
using Volo.Abp.EntityFrameworkCore;

#nullable disable

namespace PlayTicket.UserService.Migrations
{
    [DbContext(typeof(DbComplainceDbContext))]
    partial class DbComplainceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.MySql)
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PlayTicket.UserService.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_dttm");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<Guid>("ReferenceId")
                        .HasColumnType("char(36)")
                        .HasColumnName("reference_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(1)")
                        .HasColumnName("status");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext")
                        .HasColumnName("user_uid");

                    b.HasKey("Id");

                    b.ToTable("t_user", (string)null);
                });

            modelBuilder.Entity("PlayTicket.UserService.Users.UserGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("char(36)")
                        .HasColumnName("application_uid");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("char(36)")
                        .HasColumnName("group_uid");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("char(36)")
                        .HasColumnName("partner_uid");

                    b.HasKey("Id");

                    b.ToTable("t_user_group", (string)null);
                });

            modelBuilder.Entity("PlayTicket.UserService.Users.UserGroupAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Assets")
                        .HasColumnType("longtext")
                        .HasColumnName("asset");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("char(36)")
                        .HasColumnName("group_uid");

                    b.HasKey("Id");

                    b.ToTable("t_user_group_access", (string)null);
                });

            modelBuilder.Entity("PlayTicket.UserService.Users.UserUserGroupMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("char(36)")
                        .HasColumnName("group_uid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("user_uid");

                    b.HasKey("Id");

                    b.ToTable("t_user_user_group_mapping", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
