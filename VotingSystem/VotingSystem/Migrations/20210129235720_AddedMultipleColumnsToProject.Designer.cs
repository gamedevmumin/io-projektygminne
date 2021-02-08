﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VotingSystem.Models;
using VotingSystem.Models.Data;

namespace VotingSystem.Migrations
{
    [DbContext(typeof(VotingSystemDbContext))]
    [Migration("20210129235720_AddedMultipleColumnsToProject")]
    partial class AddedMultipleColumnsToProject
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EditionDraftProject", b =>
                {
                    b.Property<int>("EditionDraftsId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.HasKey("EditionDraftsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("EditionDraftProject");
                });

            modelBuilder.Entity("VotingSystem.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("District");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Repty"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Lasowice"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Bobrowniki Śląskie"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Opatowice"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Rybna"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Pniowiec"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Sowice"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Puferki"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Repecko"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Siwcowe"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Tłuczykąt"
                        });
                });

            modelBuilder.Entity("VotingSystem.Models.Edition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Editions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Edition");
                });

            modelBuilder.Entity("VotingSystem.Models.EditionDraft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("EditionDrafts");
                });

            modelBuilder.Entity("VotingSystem.Models.EditionParticipant", b =>
                {
                    b.Property<int?>("EditionId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("EditionId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("EditionParticipant");

                    b.HasDiscriminator<string>("Discriminator").HasValue("EditionParticipant");
                });

            modelBuilder.Entity("VotingSystem.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<long?>("EstimatedRealizationTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("VotingSystem.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VotingSystem.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("VotingSystem.Models.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("EditionId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("VoterPesel")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EditionId", "ProjectId", "VoterPesel")
                        .IsUnique()
                        .HasFilter("[VoterPesel] IS NOT NULL");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("VotingSystem.Models.ActiveEdition", b =>
                {
                    b.HasBaseType("VotingSystem.Models.Edition");

                    b.HasDiscriminator().HasValue("ActiveEdition");
                });

            modelBuilder.Entity("VotingSystem.Models.ConcludedEdition", b =>
                {
                    b.HasBaseType("VotingSystem.Models.Edition");

                    b.HasDiscriminator().HasValue("ConcludedEdition");
                });

            modelBuilder.Entity("VotingSystem.Models.ActiveEditionParticipant", b =>
                {
                    b.HasBaseType("VotingSystem.Models.EditionParticipant");

                    b.HasDiscriminator().HasValue("ActiveEditionParticipant");
                });

            modelBuilder.Entity("VotingSystem.Models.ConcludedEditionParticipant", b =>
                {
                    b.HasBaseType("VotingSystem.Models.EditionParticipant");

                    b.HasDiscriminator().HasValue("ConcludedEditionParticipant");
                });

            modelBuilder.Entity("EditionDraftProject", b =>
                {
                    b.HasOne("VotingSystem.Models.EditionDraft", null)
                        .WithMany()
                        .HasForeignKey("EditionDraftsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VotingSystem.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VotingSystem.Models.Edition", b =>
                {
                    b.HasOne("VotingSystem.Models.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId");

                    b.Navigation("District");
                });

            modelBuilder.Entity("VotingSystem.Models.EditionDraft", b =>
                {
                    b.HasOne("VotingSystem.Models.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("VotingSystem.Models.EditionParticipant", b =>
                {
                    b.HasOne("VotingSystem.Models.Edition", null)
                        .WithMany("Participants")
                        .HasForeignKey("EditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VotingSystem.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("VotingSystem.Models.Project", b =>
                {
                    b.HasOne("VotingSystem.Models.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("VotingSystem.Models.User", b =>
                {
                    b.HasOne("VotingSystem.Models.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("VotingSystem.Models.Vote", b =>
                {
                    b.HasOne("VotingSystem.Models.EditionParticipant", null)
                        .WithMany("Votes")
                        .HasForeignKey("EditionId", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VotingSystem.Models.Edition", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("VotingSystem.Models.EditionParticipant", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
