using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SignTrack;

namespace SignTrack.Migrations
{
    [DbContext(typeof(SignContext))]
    [Migration("20170421214803_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("SignTrack.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Phone")
                        .HasMaxLength(20);

                    b.Property<string>("PhoneId")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Students");
                });
        }
    }
}
