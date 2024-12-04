using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;


namespace Ordering.Infrastructure.Data.Configurations
{
    internal class OrderCongiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(
                    orderId => orderId.Value,
                    dbId => OrderId.Of(dbId));
            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(x => x.OrderId);

            builder.ComplexProperty(
                  o => o.ShippingAddress, addressBuilder =>
                  {
                      addressBuilder.Property(a => a.FirstName)
                      .HasMaxLength(50)
                      .IsRequired();

                      addressBuilder.Property(a=>a.LastName)
                      .HasMaxLength(50)
                      .IsRequired();

                      addressBuilder.Property(a=>a.EmailAddress)
                      .HasMaxLength(180)
                      .IsRequired();

                      addressBuilder.Property(a=>a.AddressLine)
                      .HasMaxLength(180)
                      .IsRequired();

                      addressBuilder.Property(a => a.Country)
                      .HasMaxLength(50)
                      .IsRequired(); 
                      
                      addressBuilder.Property(a => a.State)
                      .HasMaxLength(50)
                      .IsRequired(); 
                      
                      addressBuilder.Property(a => a.ZipCode)
                      .HasMaxLength(50)
                      .IsRequired();
                  }
                );

            builder.ComplexProperty(
                  o => o.BillingAddress, addressBuilder =>
                  {
                      addressBuilder.Property(a => a.FirstName)
                      .HasMaxLength(50)
                      .IsRequired();

                      addressBuilder.Property(a => a.LastName)
                      .HasMaxLength(50)
                      .IsRequired();

                      addressBuilder.Property(a => a.EmailAddress)
                      .HasMaxLength(180)
                      .IsRequired();

                      addressBuilder.Property(a => a.AddressLine)
                      .HasMaxLength(180)
                      .IsRequired();

                      addressBuilder.Property(a => a.Country)
                      .HasMaxLength(50)
                      .IsRequired();

                      addressBuilder.Property(a => a.State)
                      .HasMaxLength(50)
                      .IsRequired();

                      addressBuilder.Property(a => a.ZipCode)
                      .HasMaxLength(50)
                      .IsRequired();
                  }
                );
            builder.ComplexProperty(
                 o => o.Payement, payementBuilder =>
                 {
                     payementBuilder
                     .Property(p => p.CardName)
                     .HasMaxLength(50);

                     payementBuilder
                      .Property(p => p.CardNumber)
                      .HasMaxLength(24).IsRequired();

                     payementBuilder
                      .Property(p => p.Expiration)
                      .HasMaxLength(10);

                     payementBuilder
                     .Property(p => p.CVV)
                     .HasMaxLength(3);

                     payementBuilder.Property(p => p.PayementMethod);
                 }
                );
            builder.Property(o => o.OrderStatus)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                  s => s.ToString(),
                  dbStatus =>(OrderStatus) Enum.Parse(typeof(OrderStatus), dbStatus));
                
        }
    }
}
