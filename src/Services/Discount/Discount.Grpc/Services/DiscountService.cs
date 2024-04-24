using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(
        DiscountContext dbContext,
        ILogger<DiscountService> logger): DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CuponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var cupon = await dbContext
                .Cupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            cupon ??= new Cupon {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = string.Empty
                };
            logger.LogInformation("Discount is retrived for productName: {productName}, Amount: {Amount}", cupon.ProductName, cupon.Amount);

            var cuponModel = cupon.Adapt<CuponModel>();
            return cuponModel;
        }

        public override async Task<CuponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var cupon = request.Cupon.Adapt<Cupon>();
            if (cupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            dbContext.Cupons.Add(cupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully created. ProductName: {ProductName}", cupon.ProductName);
            var cuponModel = cupon.Adapt<CuponModel>();
            return cuponModel;
        }

        public override async Task<CuponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var cupon = request.Cupon.Adapt<Cupon>();
            if (cupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            dbContext.Cupons.Update(cupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully updated. ProductName: {ProductName}", cupon.ProductName);
            var cuponModel = cupon.Adapt<CuponModel>();
            return cuponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var cupon = await dbContext
                .Cupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if(cupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));

            dbContext.Cupons.Remove(cupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully deleted. ProductName: {ProductName}", cupon.ProductName);
            return new DeleteDiscountResponse { Success = true };
        }
    }
}
