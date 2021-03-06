﻿// <auto-generated />
using System;
using Competicao.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Competicao.Migrations
{
    [DbContext(typeof(TorneioDbContext))]
    [Migration("20210224214626_TorneioData")]
    partial class TorneioData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Competicao.Models.Time", b =>
                {
                    b.Property<long?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("TI_ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)")
                        .HasColumnName("TI_NOME");

                    b.Property<long?>("TorneioID")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("TOR_ID");

                    b.HasKey("ID");

                    b.HasIndex("TorneioID");

                    b.ToTable("TI_TIMES");
                });

            modelBuilder.Entity("Competicao.Models.Torneio", b =>
                {
                    b.Property<long?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("TOR_ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Criacao")
                        .HasColumnType("datetime2")
                        .HasColumnName("TOR_CRIACAO");

                    b.Property<DateTime>("Modificacao")
                        .HasColumnType("datetime2")
                        .HasColumnName("TOR_MODIFICACAO");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)")
                        .HasColumnName("TOR_NOME");

                    b.HasKey("ID");

                    b.ToTable("TOR_TORNEIOS");
                });

            modelBuilder.Entity("Competicao.Models.Time", b =>
                {
                    b.HasOne("Competicao.Models.Torneio", "Torneio")
                        .WithMany("Times")
                        .HasForeignKey("TorneioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Torneio");
                });

            modelBuilder.Entity("Competicao.Models.Torneio", b =>
                {
                    b.Navigation("Times");
                });
#pragma warning restore 612, 618
        }
    }
}
