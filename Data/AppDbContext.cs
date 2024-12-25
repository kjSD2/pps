using Microsoft.EntityFrameworkCore;
using pps.Models;

namespace pps.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankOffice> BankOffices { get; set; }
        public DbSet<BankAtm> BankAtms { get; set; }
        public DbSet<CreditAccount> CreditAccounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PaymentAccount> PaymentAccounts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bank>().HasKey(x => x.Id); 
            modelBuilder.Entity<BankAtm>().HasKey(x => x.Id);
            modelBuilder.Entity<BankOffice>().HasKey(x => x.Id);
            modelBuilder.Entity<CreditAccount>().HasKey(x => x.Id); 
            modelBuilder.Entity<Employee>().HasKey(x => x.Id); 
            modelBuilder.Entity<PaymentAccount>().HasKey(x => x.Id); 
            modelBuilder.Entity<User>().HasKey(x => x.Id);

            modelBuilder.Entity<Bank>().HasMany(b => b.BankOffices).WithOne(bo => bo.Bank).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BankOffice>().HasMany(bo => bo.Employees).WithOne(e => e.BankOffice).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>().HasMany(e => e.CreditAccounts).WithOne(ca => ca.Employee).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employee>().HasMany(e => e.BankAtms).WithOne(ba => ba.EmployeeAccompanying).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasMany(u => u.PaymentAccounts).WithOne(pa => pa.User).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentAccount>().HasMany(pa => pa.CreditAccounts).WithOne(ca => ca.PaymentAccount).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
