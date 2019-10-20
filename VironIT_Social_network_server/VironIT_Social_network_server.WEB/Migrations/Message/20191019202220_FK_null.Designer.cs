﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VironIT_Social_network_server.DAL.Context;

namespace VironIT_Social_network_server.WEB.Migrations.Message
{
    [DbContext(typeof(MessageContext))]
    [Migration("20191019202220_FK_null")]
    partial class FK_null
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("VironIT_Social_network_server.DAL.Model.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ForwardFromEmail")
                        .HasColumnType("text");

                    b.Property<string>("FromEmail")
                        .HasColumnType("text");

                    b.Property<int>("MessageMediaId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<string>("ToEmail")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MessageMediaId")
                        .IsUnique();

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("VironIT_Social_network_server.DAL.Model.MessageMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("MediaId")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MessagesMedia");
                });

            modelBuilder.Entity("VironIT_Social_network_server.DAL.Model.Message", b =>
                {
                    b.HasOne("VironIT_Social_network_server.DAL.Model.MessageMedia", "MessageMedia")
                        .WithOne("Message")
                        .HasForeignKey("VironIT_Social_network_server.DAL.Model.Message", "MessageMediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
