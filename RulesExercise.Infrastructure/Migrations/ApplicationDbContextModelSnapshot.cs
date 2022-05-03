﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RulesExercise.Infrastructure.Persistence;

#nullable disable

namespace RulesExercise.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("RulesExercise.Domain.Entities.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Templates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Message = "\r\n<div>\r\n	<h4>Hi there from {{ Name }} project</h4>\r\n	<ul>\r\n		<li>Id : {{ Id }};</li>\r\n        <li>Project Changed;</li>\r\n	</ul> \r\n</div>",
                            Subject = "Changes from {{Name}} project",
                            Type = "Smtp"
                        },
                        new
                        {
                            Id = 2,
                            Message = "\r\n<div>\r\n	<h4>Project {{ Name }} was Deleted</h4>\r\n	<ul>\r\n		<li>Id : {{ Id }};</li>\r\n        <li>Project Deleted;</li>\r\n	</ul> \r\n</div>",
                            Subject = "{{ Name }} project was deleted",
                            Type = "Smtp"
                        },
                        new
                        {
                            Id = 3,
                            Message = "Description: {{ Description }}",
                            Subject = "{{ Name }} project with id  {{ Id }} was changed",
                            Type = "Telegram"
                        },
                        new
                        {
                            Id = 4,
                            Message = "Description: {{ Description }}\n Id: {{ Id }}",
                            Subject = "{{ Name }} project was deleted",
                            Type = "Telegram"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}