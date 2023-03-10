using LMSBlazor.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace LMSBlazor.Plugins.SqlServer
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employee_Leave> Employee_Leaves { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<LeaveApplicationLog> LeaveApplicationLogs { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee_Leave>()
                .HasKey(el => new { el.LeaveId, el.EmployeeId });

            modelBuilder.Entity<Employee_Leave>()
                .HasOne(el => el.Leave)
                .WithMany(l => l.Employee_Leaves)
                .HasForeignKey(el => el.LeaveId);

            modelBuilder.Entity<Employee_Leave>()
                .HasOne(el => el.Employee)
                .WithMany(e => e.Employee_Leaves)
                .HasForeignKey(el => el.EmployeeId);

            modelBuilder.Entity<Leave>().HasData(
                new Leave { LeaveId = 1, LeaveName = "Cuti Tahunan" },
                new Leave { LeaveId = 2, LeaveName = "Cuti Sakit" },
                new Leave { LeaveId = 3, LeaveName = "Cuti Bersalin (Perempuan)" },
                new Leave { LeaveId = 4, LeaveName = "Cuti Bersalin (Lelaki)" }
                );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, EmployeeName = "Aminudin Ab Kahar", EmployeeAge = 30, HireDate=DateTime.Now.AddYears(-1).AddDays(-12),EmployeeEmail="amin@idwal.com.my",EmployeePhoneNumber="011-33272978",TotalLeaveDays = 54 },
                new Employee { EmployeeId = 2, EmployeeName = "Mohd Nor Bin Md Tan", EmployeeAge = 50, HireDate=DateTime.Now.AddYears(-20),EmployeeEmail="mdNor@idwal.com.my", TotalLeaveDays = 0 }
                );

            modelBuilder.Entity<Employee_Leave>().HasData(
                new Employee_Leave { EmployeeId = 1, LeaveId = 1, DaysLeft = 12},
                new Employee_Leave { EmployeeId = 1, LeaveId = 2, DaysLeft = 12 },
                new Employee_Leave { EmployeeId = 1, LeaveId = 4, DaysLeft = 30 }
                );
        }
    }
}