using MadameCoco.Order.Entities;
using Microsoft.EntityFrameworkCore;

namespace MadameCoco.Order.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<Entities.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Entities.Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).IsRequired();
            entity.OwnsOne(e => e.ShippingAddress, address =>
            {
                address.Property(a => a.AddressLine).HasColumnName("ShippingAddressLine").IsRequired();
                address.Property(a => a.City).HasColumnName("ShippingCity").IsRequired();
                address.Property(a => a.Country).HasColumnName("ShippingCountry").IsRequired();
                address.Property(a => a.CityCode).HasColumnName("ShippingCityCode").IsRequired();
            });

            entity.HasMany(e => e.Items)
                  .WithOne()
                  .HasForeignKey("OrderId")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProductName).IsRequired();
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
        });
    }
}
