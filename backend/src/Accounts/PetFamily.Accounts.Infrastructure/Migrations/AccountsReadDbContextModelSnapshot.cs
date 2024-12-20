﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.Accounts.Infrastructure.Data;

#nullable disable

namespace PetFamily.Accounts.Infrastructure.Migrations
{
    [DbContext(typeof(AccountsReadDbContext))]
    partial class AccountsReadDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("accounts")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.Core.Dtos.Accounts.AdminAccountDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FristName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("UserId1")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id1");

                    b.HasKey("Id")
                        .HasName("pk_admin_accounts");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_admin_accounts_user_id");

                    b.HasIndex("UserId1")
                        .HasDatabaseName("ix_admin_accounts_user_id1");

                    b.ToTable("admin_accounts", "accounts");
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Accounts.ParticipantAccountDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FristName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("UserId1")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id1");

                    b.HasKey("Id")
                        .HasName("pk_participant_accounts");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_participant_accounts_user_id");

                    b.HasIndex("UserId1")
                        .HasDatabaseName("ix_participant_accounts_user_id1");

                    b.ToTable("participant_accounts", "accounts");
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Accounts.RoleDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", "accounts");
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Accounts.UserDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FristName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<string>("SocialNewroks")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("social_newroks");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", "accounts");
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Accounts.UserRolesDto", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_roles_role_id");

                    b.ToTable("user_roles", "accounts");
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Accounts.VolunteerAccountDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Experience")
                        .HasColumnType("integer")
                        .HasColumnName("experience");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<string>("Requisites")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("requisites");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("UserId1")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id1");

                    b.HasKey("Id")
                        .HasName("pk_volunteer_accounts");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_volunteer_accounts_user_id");

                    b.HasIndex("UserId1")
                        .HasDatabaseName("ix_volunteer_accounts_user_id1");

                    b.ToTable("volunteer_accounts", "accounts");
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Accounts.AdminAccountDto", b =>
                {
                    b.HasOne("PetFamily.Core.Dtos.Accounts.UserDto", null)
                        .WithOne("AdminAccount")
                        .HasForeignKey("PetFamily.Core.Dtos.Accounts.AdminAccountDto", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_admin_accounts_users_user_id");

                    b.HasOne("PetFamily.Core.Dtos.Accounts.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_admin_accounts_users_user_id1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Accounts.ParticipantAccountDto", b =>
                {
                    b.HasOne("PetFamily.Core.Dtos.Accounts.UserDto", null)
                        .WithOne("ParticipantAccount")
                        .HasForeignKey("PetFamily.Core.Dtos.Accounts.ParticipantAccountDto", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_participant_accounts_users_user_id");

                    b.HasOne("PetFamily.Core.Dtos.Accounts.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_participant_accounts_users_user_id1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Accounts.UserRolesDto", b =>
                {
                    b.HasOne("PetFamily.Core.Dtos.Accounts.RoleDto", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_roles_role_id");

                    b.HasOne("PetFamily.Core.Dtos.Accounts.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_users_user_id");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Accounts.VolunteerAccountDto", b =>
                {
                    b.HasOne("PetFamily.Core.Dtos.Accounts.UserDto", null)
                        .WithOne("VolunteerAccount")
                        .HasForeignKey("PetFamily.Core.Dtos.Accounts.VolunteerAccountDto", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_volunteer_accounts_users_user_id");

                    b.HasOne("PetFamily.Core.Dtos.Accounts.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_volunteer_accounts_users_user_id1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetFamily.Core.Dtos.Accounts.UserDto", b =>
                {
                    b.Navigation("AdminAccount");

                    b.Navigation("ParticipantAccount");

                    b.Navigation("VolunteerAccount");
                });
#pragma warning restore 612, 618
        }
    }
}
