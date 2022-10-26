﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VacunassistBackend.Data;

#nullable disable

namespace VacunnasistBackend.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("VacunassistBackend.Entities.AppliedVaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AppliedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AppliedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VaccineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VaccineId");

                    b.ToTable("AppliedVaccines");
                });

            modelBuilder.Entity("VacunassistBackend.Entities.DevelopedVaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("DevelopedVaccines");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Name = "COVID-19"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            Name = "Fiebre amarilla"
                        },
                        new
                        {
                            Id = 3,
                            IsActive = true,
                            Name = "Gripe"
                        });
                });

            modelBuilder.Entity("VacunassistBackend.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("HealthWorker")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Pregnant")
                        .HasColumnType("bit");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Calle Falsa 1234, La Plata",
                            BirthDate = new DateTime(2022, 10, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            DNI = "11111111",
                            Email = "admin@vacunassist.com",
                            FullName = "Administrador",
                            Gender = "other",
                            HealthWorker = false,
                            IsActive = true,
                            PasswordHash = "1000:7DQsHvyucqpp8sjbC/4BQgVSfrQogEnD:YT/9MDQW1JboLVfT3Jvtwg/+qxCeWhZx",
                            Pregnant = false,
                            Province = "Buenos Aires",
                            Role = "administrator",
                            UserName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Calle Falsa 2345, La Plata",
                            BirthDate = new DateTime(2022, 10, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            DNI = "22345678",
                            Email = "operador1@vacunassist.com",
                            FullName = "Luis Gutierrez",
                            Gender = "male",
                            HealthWorker = false,
                            IsActive = true,
                            PasswordHash = "1000:cA7agxstZBnXiTs2ArDU0tBeq6Z1WRk+:JCGud20r/MioSHly0d1T8MYlW39GHuOx",
                            Pregnant = false,
                            Province = "Buenos Aires",
                            Role = "operator",
                            UserName = "Operador1"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Calle Falsa 9874, Salta",
                            BirthDate = new DateTime(2022, 10, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            DNI = "89785451",
                            Email = "estefania@vacunassist.com",
                            FullName = "Estefania Borzi",
                            Gender = "female",
                            HealthWorker = false,
                            IsActive = true,
                            PasswordHash = "1000:F+FOkZi2+dm1gphhuwgqMMHhSbcgYCC1:x5F84rZijXSUhZQy0NZTC0oXwvFQ/xMV",
                            Pregnant = false,
                            Province = "Salta",
                            Role = "operator",
                            UserName = "Operador2"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Calle Falsa 9874, Salta",
                            BirthDate = new DateTime(2022, 10, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            DNI = "89785451",
                            Email = "jr@vacunassist.com",
                            FullName = "Jose Luis Rodriguez",
                            Gender = "male",
                            HealthWorker = false,
                            IsActive = true,
                            PasswordHash = "1000:jaREfrCLJk/Phalm0zCU//Am0AppGFpe:WhyUMgQwxNQTeIVqlHgbCB5PD+B6fxuj",
                            Pregnant = false,
                            Province = "Buenos Aires",
                            Role = "analyst",
                            UserName = "Analista1"
                        },
                        new
                        {
                            Id = 5,
                            Address = "Calle Falsa 4567, La Plata",
                            BirthDate = new DateTime(2022, 10, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            DNI = "11111111",
                            Email = "vacunador@email.com",
                            FullName = "Vacunador",
                            Gender = "other",
                            HealthWorker = false,
                            IsActive = true,
                            PasswordHash = "1000:3jqnxm8du+mUygW9ulN16nwqJWZ9sF7M:1K81725vzrBkqEs1f61mdrXE6mh77x5v",
                            Pregnant = false,
                            Province = "Buenos Aires",
                            Role = "vacunator",
                            UserName = "Vacunador"
                        });
                });

            modelBuilder.Entity("VacunassistBackend.Entities.AppliedVaccine", b =>
                {
                    b.HasOne("VacunassistBackend.Entities.User", "User")
                        .WithMany("Vaccines")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VacunassistBackend.Entities.DevelopedVaccine", "Vaccine")
                        .WithMany()
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Vaccine");
                });

            modelBuilder.Entity("VacunassistBackend.Entities.User", b =>
                {
                    b.Navigation("Vaccines");
                });
#pragma warning restore 612, 618
        }
    }
}
