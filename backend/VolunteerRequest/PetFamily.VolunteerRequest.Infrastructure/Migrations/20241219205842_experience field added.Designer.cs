﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.VolunteerRequest.Infrastructure.Data;

#nullable disable

namespace PetFamily.VolunteerRequest.Infrastructure.Migrations
{
    [DbContext(typeof(VolunteersRequestWriteDbContext))]
    [Migration("20241219205842_experience field added")]
    partial class experiencefieldadded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("volunteer_requests")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.VolunteerRequest.Domain.AggregateRoot.UserRestriction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("BanReason")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ban_reason");

                    b.Property<DateTime>("BannedUntil")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("banned_until");

                    b.Property<Guid>("DiscussionId")
                        .HasColumnType("uuid")
                        .HasColumnName("discussion_id");

                    b.HasKey("Id")
                        .HasName("pk_user_restrictions");

                    b.ToTable("user_restrictions", "volunteer_requests");
                });

            modelBuilder.Entity("PetFamily.VolunteerRequest.Domain.AggregateRoot.VolunteerRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uuid")
                        .HasColumnName("admin_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("DiscussionId")
                        .HasColumnType("uuid")
                        .HasColumnName("discussion_id");

                    b.Property<string>("RejectionComment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("rejection_comment");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.ComplexProperty<Dictionary<string, object>>("VolunteerInfo", "PetFamily.VolunteerRequest.Domain.AggregateRoot.VolunteerRequest.VolunteerInfo#VolunteerRequestInfo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("description");

                            b1.Property<int>("Experience")
                                .HasColumnType("integer")
                                .HasColumnName("experience");

                            b1.Property<string>("Requisites")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("requisites");

                            b1.ComplexProperty<Dictionary<string, object>>("Email", "PetFamily.VolunteerRequest.Domain.AggregateRoot.VolunteerRequest.VolunteerInfo#VolunteerRequestInfo.Email#Email", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<string>("Mail")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("email");
                                });

                            b1.ComplexProperty<Dictionary<string, object>>("FullName", "PetFamily.VolunteerRequest.Domain.AggregateRoot.VolunteerRequest.VolunteerInfo#VolunteerRequestInfo.FullName#FullName", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<string>("LastName")
                                        .HasColumnType("text")
                                        .HasColumnName("last_name");

                                    b2.Property<string>("MiddleName")
                                        .HasColumnType("text")
                                        .HasColumnName("middle_name");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("first_name");
                                });

                            b1.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PetFamily.VolunteerRequest.Domain.AggregateRoot.VolunteerRequest.VolunteerInfo#VolunteerRequestInfo.PhoneNumber#PhoneNumber", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<string>("Number")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("phone_number");
                                });
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteer_requests");

                    b.ToTable("volunteer_requests", "volunteer_requests");
                });
#pragma warning restore 612, 618
        }
    }
}
