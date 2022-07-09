using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess.Models
{
    public partial class DepartmentEmployeeContext : DbContext
    {
        public DepartmentEmployeeContext()
        {
        }

        public DepartmentEmployeeContext(DbContextOptions<DepartmentEmployeeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountDb> AccountDbs { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=(local);database=DepartmentEmployee;uid=quannpm;pwd=admin;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountDb>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__AccountD__1788CCAC6E94F4FF");

                entity.ToTable("AccountDB");

                entity.Property(e => e.UserId)
                    .HasMaxLength(20)
                    .HasColumnName("UserID");

                entity.Property(e => e.AccountFullName).HasMaxLength(100);

                entity.Property(e => e.AccountPassword)
                    .IsRequired()
                    .HasMaxLength(90);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(20)
                    .HasColumnName("DepartmentID");

                entity.Property(e => e.DepartmentDescription).HasMaxLength(200);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(20)
                    .HasColumnName("DepartmentID");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.JobTitle).HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Employee__Depart__145C0A3F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
