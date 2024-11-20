using Basket.API.Data;
using Basket.API.Entities;
using Carter;
using EShop.Shared.CQRS;
using MediatR;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName):IQuery<GetBasketResponse>;
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBaksetEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));
                return Results.Ok(result);
            })
              .WithName("Get Basket")
              .WithDescription("Get Basket With User Name")
              .Produces<GetBasketResponse>()
              .ProducesProblem(StatusCodes.Status404NotFound)
              .WithSummary("Get Basket");

               
        }
    }
    public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResponse>
    {
        public async Task<GetBasketResponse> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket= await repository.GetBasketAsync(query.UserName,cancellationToken);
            return new GetBasketResponse(basket);
        }
    }
}
