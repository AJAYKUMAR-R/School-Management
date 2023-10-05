using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Models;

public partial class CrudContext : DbContext
{
    public CrudContext()
    {
    }

    public CrudContext(DbContextOptions<CrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }
    public virtual DbSet<GetTotal> GetTotal { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CRUD;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__A2F7EDF4292788A8");

            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("Student_id");
            entity.Property(e => e.StudentName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("studentName");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__UserProf__4D11D63CE7BBA608");

            entity.ToTable("UserProfile");

            entity.HasIndex(e => e.Email, "UQ__UserProf__AB6E6164E13F0DE4").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("studentId");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.Isdeleted).HasDefaultValueSql("((1))");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(64)
                .HasColumnName("passwordHash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(128)
                .HasColumnName("passwordSalt");
            entity.Property(e => e.RefreshExpireTime).HasColumnType("datetime");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Roles)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("studentName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
