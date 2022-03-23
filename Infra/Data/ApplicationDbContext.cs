using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using OrderRequest.Domain.Products;

namespace OrderRequest.Infra.Data;

public class ApplicationDbContext:DbContext 
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
  protected override void OnConfiguring(DbContextOptionsBuilder options){
    options.UseSqlServer();
  }

  public DbSet<Product> Products {get;set;}
  public DbSet<Category> Categories {get;set;}
  
  protected override void OnModelCreating(ModelBuilder builder){
    builder.Ignore<Notification>();
    builder.Entity<Product>()
      .Property(p=>p.Name).HasMaxLength(255);
    
  }
  protected override void ConfigureConventions(ModelConfigurationBuilder configuration){
    configuration.Properties<string>()
      .HaveMaxLength(100);
  }

}