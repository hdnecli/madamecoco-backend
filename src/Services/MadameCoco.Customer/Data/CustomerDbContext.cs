using MadameCoco.Customer.Entities;
using Microsoft.EntityFrameworkCore;

namespace MadameCoco.Customer.Data;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
    }

    public DbSet<Entities.Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Entities.Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Email).IsRequired();

            entity.OwnsOne(e => e.Address, address =>
            {
                address.Property(a => a.AddressLine).HasColumnName("AddressLine").IsRequired();
                address.Property(a => a.City).HasColumnName("City").IsRequired();
                address.Property(a => a.Country).HasColumnName("Country").IsRequired();
                address.Property(a => a.CityCode).HasColumnName("CityCode").IsRequired();
            });
        });
    }
}
