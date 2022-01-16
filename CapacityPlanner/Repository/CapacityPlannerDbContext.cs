using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CapacityPlanner.Models;

#nullable disable

namespace CapacityPlanner.Repository
{
    public partial class CapacityPlannerDbContext : DbContext
    {
        public CapacityPlannerDbContext()
        {
        }

        public CapacityPlannerDbContext(DbContextOptions<CapacityPlannerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Affectation> Affectations { get; set; }
        public virtual DbSet<Collaborateur> Collaborateurs { get; set; }
        public virtual DbSet<Projet> Projets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CapacityPlanner;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Affectation>(entity =>
            {
                entity.HasOne(d => d.Collaborateur)
                    .WithMany(p => p.Affectations)
                    .HasForeignKey(d => d.CollaborateurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Affectations_Collaborateurs");

                entity.HasOne(d => d.Projet)
                    .WithMany(p => p.Affectations)
                    .HasForeignKey(d => d.ProjetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Affectations_Projets");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
