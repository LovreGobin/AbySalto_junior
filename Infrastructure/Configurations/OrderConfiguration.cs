using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Database.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.CustomerName)
                   .IsRequired()
                   .HasMaxLength(150);
            builder.Property(o => o.DeliveryAddress)
                   .IsRequired()
                   .HasMaxLength(250);
            builder.Property(o => o.ContactNumber)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(o => o.Currency)
                   .IsRequired()
                   .HasMaxLength(10);
            builder.Property(o => o.Note)
                   .HasMaxLength(500);
            builder.Property(o => o.OrderTime)
                   .IsRequired();
            builder.Property(o => o.Status)
                   .IsRequired();
            builder.Property(o => o.PaymentMethod)
                   .IsRequired();

            // Order → OrderItems (1:N)
            builder.HasMany(o => o.Items)
                   .WithOne(i => i.Order)
                   .HasForeignKey(i => i.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}