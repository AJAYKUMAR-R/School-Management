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

    public virtual DbSet<StudentFee> StudentFees { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }
    public virtual DbSet<GetTotal> GetTotal { get; set; }
    public virtual DbSet<GetFee> GetFee { get; set; }

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

        modelBuilder.Entity<StudentFee>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("StudentFee");

            entity.HasIndex(e => e.StudentId, "UQ__StudentF__4D11D63D5EE28FDF").IsUnique();

            entity.Property(e => e.BusFee)
                .HasColumnType("money")
                .HasColumnName("busFee");
            entity.Property(e => e.ExamFee)
                .HasColumnType("money")
                .HasColumnName("examFee");
            entity.Property(e => e.FeeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("feeId");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.IsPaid)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isPaid");
            entity.Property(e => e.PaidDate)
                .HasColumnType("datetime")
                .HasColumnName("paidDate");
            entity.Property(e => e.StudentId).HasColumnName("studentId");
            entity.Property(e => e.TotalFee).HasColumnType("money");
            entity.Property(e => e.TutionFee)
                .HasColumnType("money")
                .HasColumnName("tutionFee");

            entity.HasOne(d => d.Student).WithOne()
                .HasForeignKey<StudentFee>(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentFe__isDel__11158940");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__UserProf__4D11D63C18196091");

            entity.ToTable("UserProfile");

            entity.HasIndex(e => e.Email, "UQ__UserProf__AB6E61646A464879").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("studentId");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.Isdeleted).HasDefaultValueSql("((0))");
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
            entity.Property(e => e.StudentGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("studentGuid");
            entity.Property(e => e.StudentName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("studentName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
