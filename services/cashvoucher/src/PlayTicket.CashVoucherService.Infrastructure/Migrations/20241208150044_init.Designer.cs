﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlayTicket.CashVoucherService.EntityFrameworkCore.DbOffice;
using Volo.Abp.EntityFrameworkCore;

#nullable disable

namespace PlayTicket.CashVoucherService.Migrations
{
    [DbContext(typeof(DbOfficeDbContext))]
    [Migration("20241208150044_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.MySql)
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);
#pragma warning restore 612, 618
        }
    }
}