using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using Competicao.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Competicao.Data
{
    public class TorneioDbContext : DbContext
    {
        public DbSet<Torneio> Torneios { get; set; }
        public DbSet<Time> Times { get; set; }

        public TorneioDbContext(DbContextOptions<TorneioDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Torneio>(t =>
            {
                t.ToTable("TOR_TORNEIOS");

                t.Property(p => p.Nome)
                .HasColumnName("TOR_NOME")
                .IsRequired()
                .HasMaxLength(35);

                t.Property(p => p.ID)
                .HasColumnName("TOR_ID")
                .IsRequired();

                t.Property(p => p.Criacao)
                .HasColumnName("TOR_CRIACAO")
                .IsRequired();

                t.Property(p => p.Modificacao)
                .HasColumnName("TOR_MODIFICACAO")
                .IsRequired();

                t.HasKey(pk => pk.ID);

                t.HasMany(p => p.Times)
                .WithOne(p => p.Torneio)
                .HasForeignKey(fk => fk.TorneioID);
            });

            builder.Entity<Time>(t =>
            {
                t.ToTable("TI_TIMES");

                t.Property(p => p.ID)
                .HasColumnName("TI_ID")
                .IsRequired();

                t.Property(p => p.Nome)
                .HasColumnName("TI_NOME")
                .IsRequired()
                .HasMaxLength(35);

                t.Property(p => p.TorneioID)
                .HasColumnName("TOR_ID")
                .IsRequired();

                t.HasKey(pk => pk.ID);

                t.HasOne(p => p.Torneio)
                .WithMany(p => p.Times)
                .HasForeignKey(fk => fk.TorneioID);
            });
        }
    }
}