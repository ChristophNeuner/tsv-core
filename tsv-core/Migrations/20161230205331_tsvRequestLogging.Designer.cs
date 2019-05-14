using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using tsv_core.Models;

namespace tsvcore.Migrations
{
    [DbContext(typeof(LoggingDbContext))]
    [Migration("20161230205331_tsvRequestLogging")]
    partial class tsvRequestLogging
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("tsv_core.Models.Request", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IPAddressClient");

                    b.Property<string>("Path");

                    b.Property<string>("Time");

                    b.Property<string>("UserAgent");

                    b.HasKey("ID");

                    b.ToTable("Requests");
                });
        }
    }
}
