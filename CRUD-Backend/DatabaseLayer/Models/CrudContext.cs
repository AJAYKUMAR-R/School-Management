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

    public virtual DbSet<BankDetail> BankDetails { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentFee> StudentFees { get; set; }

    public virtual DbSet<StudentProfile> StudentProfiles { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }
    public virtual DbSet<GetTotal> GetTotal { get; set; }
    public virtual DbSet<GetFee> GetFee { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CRUD;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankDetail>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.StudentId, "UQ__BankDeta__4D11D63D31E3E0AA").IsUnique();

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("accountNumber");
            entity.Property(e => e.BankDetailsId)
                .ValueGeneratedOnAdd()
                .HasColumnName("bankDetailsId");
            entity.Property(e => e.Branch)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("branch");
            entity.Property(e => e.IfcsCode).HasColumnName("ifcsCode");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.StudentId).HasColumnName("studentId");

            entity.HasOne(d => d.Student).WithOne()
                .HasForeignKey<BankDetail>(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BankDetai__isDel__3B0BC30C");
        });

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

            entity.HasIndex(e => e.StudentId, "UQ__StudentF__4D11D63DC227B778").IsUnique();

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
                .HasConstraintName("FK__StudentFe__isDel__2F9A1060");
        });

        modelBuilder.Entity<StudentProfile>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("StudentProfile");

            entity.HasIndex(e => e.StudentId, "UQ__StudentP__4D11D63DFD5E36D9").IsUnique();

            entity.Property(e => e.AnnualIncome)
                .HasColumnType("money")
                .HasColumnName("annualIncome");
            entity.Property(e => e.FatherName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fatherName");
            entity.Property(e => e.FatherOccupation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fatherOccupation");
            entity.Property(e => e.Gender)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.MotherName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("motherName");
            entity.Property(e => e.MotherOccupation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("motherOccupation");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.StudentId).HasColumnName("studentId");
            entity.Property(e => e.StudentProfileId)
                .ValueGeneratedOnAdd()
                .HasColumnName("studentProfileId");

            entity.HasOne(d => d.Student).WithOne()
                .HasForeignKey<StudentProfile>(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentPr__isDel__336AA144");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserProf__CB9A1CFF71F8F480");

            entity.ToTable("UserProfile");

            entity.HasIndex(e => e.UserMail, "UQ__UserProf__DF9290BD839C8EDE").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isdeleted");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(64)
                .HasColumnName("passwordHash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(128)
                .HasColumnName("passwordSalt");
            entity.Property(e => e.Pincode).HasColumnName("pincode");
            entity.Property(e => e.RefreshExpireTime)
                .HasColumnType("datetime")
                .HasColumnName("refreshExpireTime");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("refreshToken");
            entity.Property(e => e.Roles)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("roles");
            entity.Property(e => e.UserGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("userGuid");
            entity.Property(e => e.UserMail)
                .HasMaxLength(255)
                .HasColumnName("userMail");
            entity.Property(e => e.UserName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
