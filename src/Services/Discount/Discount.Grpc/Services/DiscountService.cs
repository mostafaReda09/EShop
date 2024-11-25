using Discount.Grpc.Data;
using Discount.Grpc.Entities;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountDbContext dbcontext) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon =await dbcontext.Coupons
                .FirstOrDefaultAsync(x=>x.ProductName==request.ProductName) ?? new Entities.Coupon() { ProductName="No Discount",Ammount=0,Description="No Discount"};
            return coupon.Adapt<CouponModel>();
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>()
                ??throw new RpcException(new Status(StatusCode.InvalidArgument,"Invalid request."));
            
            dbcontext.Coupons.Add(coupon);
            await dbcontext.SaveChangesAsync();
            return coupon.Adapt<CouponModel>();
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>()
           ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request."));

            dbcontext.Coupons.Update(coupon);
            await dbcontext.SaveChangesAsync();
            return coupon.Adapt<CouponModel>();
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext.Coupons
               .FirstOrDefaultAsync(x => x.ProductName == request.ProductName)
               ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request."));
            dbcontext.Remove(coupon);
            await dbcontext.SaveChangesAsync();
            return new DeleteDiscountResponse() { Success = true };
        }
    }
}
