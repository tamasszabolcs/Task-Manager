﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManager.Data;

#nullable disable

namespace TaskManager.Migrations
{
    [DbContext(typeof(TaskManagerContext))]
    partial class TaskManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("TaskManager.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TaskManager.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("TaskManager.Models.Task1", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Progress")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StatusId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskManager.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("SaltedPassword")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskManager.Models.Category", b =>
                {
                    b.HasOne("TaskManager.Models.User", null)
                        .WithMany("Categories")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TaskManager.Models.Status", b =>
                {
                    b.HasOne("TaskManager.Models.User", null)
                        .WithMany("Status")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TaskManager.Models.Task1", b =>
                {
                    b.HasOne("TaskManager.Models.User", null)
                        .WithMany("Tasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskManager.Models.User", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Status");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}