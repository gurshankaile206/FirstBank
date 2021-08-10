using Microsoft.EntityFrameworkCore;

namespace FirstBank.Models
{
  public class FirstBankContext : DbContext
  {
    public virtual DbSet<Account> Accounts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAccount> CustomerAccount { get; set; }

    public FirstBankContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}