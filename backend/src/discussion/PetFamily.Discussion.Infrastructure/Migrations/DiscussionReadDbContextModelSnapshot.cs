﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.Discussion.Infrastructure.Data;

#nullable disable

namespace PetFamily.Discussion.Infrastructure.Migrations
{
    [DbContext(typeof(DiscussionReadDbContext))]
    partial class DiscussionReadDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("discussions")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.Core.Dtos.Discussion.DiscussionDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uuid")
                        .HasColumnName("admin_id");

                    b.Property<Guid>("RelationId")
                        .HasColumnType("uuid")
                        .HasColumnName("relation_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.HasKey("Id")
                        .HasName("pk_discussions");

                    b.ToTable("discussions", "discussions", t =>
                        {
                            t.Property("Id")
                                .HasColumnName("id1");
                        });
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Discussion.MessageDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid?>("DiscussionDtoId")
                        .HasColumnType("uuid")
                        .HasColumnName("discussion_dto_id");

                    b.Property<Guid>("DiscussionId")
                        .HasColumnType("uuid")
                        .HasColumnName("discussion_id");

                    b.Property<bool>("IsEdited")
                        .HasColumnType("boolean")
                        .HasColumnName("is_edited");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.HasKey("Id")
                        .HasName("pk_messages");

                    b.HasIndex("DiscussionDtoId")
                        .HasDatabaseName("ix_messages_discussion_dto_id");

                    b.ToTable("messages", "discussions", t =>
                        {
                            t.Property("Id")
                                .HasColumnName("id1");
                        });
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Discussion.MessageDto", b =>
                {
                    b.HasOne("PetFamily.Core.Dtos.Discussion.DiscussionDto", null)
                        .WithMany("Messages")
                        .HasForeignKey("DiscussionDtoId")
                        .HasConstraintName("fk_messages_discussions_discussion_dto_id");
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Discussion.DiscussionDto", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}