using Basket.API.Entities;
using Carter;
using EShop.Shared.CQRS;
using MediatR;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketCommandResponse>;
    public record DeleteBasketCommandResponse();
    public class StoreBasketCommandEndPint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket", async (string userName, ISender sender) =>
            {

                var result = await sender.Send(new DeleteBasketCommand(userName));
                return Results.NoContent();
            })
              .WithName("Delete Basket")
              .WithDescription("Delete Basket")
              .WithSummary("Dlete Basket");


        }
    }

    public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResponse>
    {
        public Task<DeleteBasketCommandResponse> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
