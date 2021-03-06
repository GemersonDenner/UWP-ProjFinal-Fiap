﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Uwp.ProjFinal;

namespace Uwp.ProjFinal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3");

            modelBuilder.Entity("Uwp.ProjFinal.Models.AgendaItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("RemindMe");

                    b.Property<DateTime>("Time");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("AgendaItens");
                });
        }
    }
}
