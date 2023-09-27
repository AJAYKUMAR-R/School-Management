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

    public virtual DbSet<StudentProfile> StudentProfiles { get; set; }

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

        modelBuilder.Entity<StudentProfile>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__StudentP__4D11D63CF559AADA");

            entity.ToTable("StudentProfile");

            entity.HasIndex(e => e.Email, "UQ__StudentP__AB6E616478329AF0").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("studentId");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(64)
                .HasColumnName("passwordHash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(128)
                .HasColumnName("passwordSalt");
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
