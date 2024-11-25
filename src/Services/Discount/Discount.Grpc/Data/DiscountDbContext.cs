using Discount.Grpc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Discount.Grpc.Data
{
    public class DiscountDbContext(DbContextOptions<DiscountDbContext> options) : DbContext(options)
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

    }
}
