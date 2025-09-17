using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CompanyEFCore.Models;

public partial class CompanyContext : DbContext
{
    public CompanyContext()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentLocation> DepartmentLocations { get; set; }

    public virtual DbSet<Dependent> Dependents { get; set; }

    public virtual DbSet<EmpManageDep> EmpManageDeps { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=COMPANY;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;Connection Timeout=60;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Dnum).HasName("PK__departme__2D5F4C731CA5CFDE");

            entity.ToTable("department", "COMPANY");

            entity.Property(e => e.Dnum).HasColumnName("dnum");
            entity.Property(e => e.Dname)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("dname");
            entity.Property(e => e.Hiredate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("hiredate");
            entity.Property(e => e.Ssn).HasColumnName("ssn");

            entity.HasOne(d => d.SsnNavigation).WithMany(p => p.Departments)
                .HasForeignKey(d => d.Ssn)
                .HasConstraintName("FK__department__ssn__3C69FB99");
        });

        modelBuilder.Entity<DepartmentLocation>(entity =>
        {
            entity.HasKey(e => new { e.Location, e.Dnum }).HasName("PK__departme__63FF149A573A8DE3");

            entity.ToTable("department_locations", "COMPANY");

            entity.Property(e => e.Location)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Dnum).HasColumnName("dnum");

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.DepartmentLocations)
                .HasForeignKey(d => d.Dnum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__department__dnum__4D94879B");
        });

        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.HasKey(e => new { e.Depname, e.Essn }).HasName("PK__dependen__B7BF5A4393E30AE7");

            entity.ToTable("dependent", "COMPANY");

            entity.Property(e => e.Depname)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("depname");
            entity.Property(e => e.Essn).HasColumnName("essn");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gender");

            entity.HasOne(d => d.EssnNavigation).WithMany(p => p.Dependents)
                .HasForeignKey(d => d.Essn)
                .HasConstraintName("FK__dependent__essn__534D60F1");
        });

        modelBuilder.Entity<EmpManageDep>(entity =>
        {
            entity.HasKey(e => new { e.Essn, e.Pnum }).HasName("PK__emp_mana__201ED3140E214220");

            entity.ToTable("emp_manage_dep", "COMPANY");

            entity.Property(e => e.Essn).HasColumnName("essn");
            entity.Property(e => e.Pnum).HasColumnName("pnum");
            entity.Property(e => e.WorkingHours).HasColumnName("working_hours");

            entity.HasOne(d => d.EssnNavigation).WithMany(p => p.EmpManageDeps)
                .HasForeignKey(d => d.Essn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__emp_manage__essn__49C3F6B7");

            entity.HasOne(d => d.PnumNavigation).WithMany(p => p.EmpManageDeps)
                .HasForeignKey(d => d.Pnum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__emp_manage__pnum__48CFD27E");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Ssn).HasName("PK__employee__DDDF0AE7FEA82AB1");

            entity.ToTable("employee", "COMPANY");

            entity.Property(e => e.Ssn).HasColumnName("ssn");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Dnum).HasColumnName("dnum");
            entity.Property(e => e.First)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Last)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last");
            entity.Property(e => e.Managerid).HasColumnName("managerid");

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Dnum)
                .HasConstraintName("FK__employee__dnum__4AB81AF0");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.Managerid)
                .HasConstraintName("FK__employee__manage__38996AB5");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Pnum).HasName("PK__project__41674BCFF4B1E8D6");

            entity.ToTable("project", "COMPANY");

            entity.Property(e => e.Pnum).HasColumnName("pnum");
            entity.Property(e => e.City)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Dnum).HasColumnName("dnum");
            entity.Property(e => e.Location)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Pname)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("pname");

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Dnum)
                .HasConstraintName("FK__project__dnum__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
