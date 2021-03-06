﻿// <auto-generated />
using HedgehogRun.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HedgehogRun.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180117030852_didReconnect")]
    partial class didReconnect
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HedgehogRun.Entities.HogLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("DidReconnect");

                    b.Property<double>("Humidity");

                    b.Property<DateTime>("PostTime");

                    b.Property<double>("TemperatureF");

                    b.Property<int>("Ticks");

                    b.HasKey("Id");

                    b.ToTable("HogLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
