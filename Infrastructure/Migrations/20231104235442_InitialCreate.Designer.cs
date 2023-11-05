﻿// <auto-generated />
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    [Migration("20231104235442_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("Infrastructure.Data.TodoItemEntity", b =>
                {
                    b.Property<int>("TodoItemEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<int>("TodoListEntityId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("TodoListId");

                    b.HasKey("TodoItemEntityId");

                    b.HasIndex("TodoListEntityId");

                    b.ToTable("TodoItem");
                });

            modelBuilder.Entity("Infrastructure.Data.TodoListEntity", b =>
                {
                    b.Property<int>("TodoListEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("TodoListEntityId");

                    b.ToTable("TodoList");
                });

            modelBuilder.Entity("Infrastructure.Data.TodoItemEntity", b =>
                {
                    b.HasOne("Infrastructure.Data.TodoListEntity", null)
                        .WithMany("Items")
                        .HasForeignKey("TodoListEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Data.TodoListEntity", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}