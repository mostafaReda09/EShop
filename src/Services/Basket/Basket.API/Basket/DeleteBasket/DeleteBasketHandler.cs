using Basket.API.Data;
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
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {

                var result = await sender.Send(new DeleteBasketCommand(userName));
                return Results.NoContent();
            })
              .WithName("Delete Basket")
              .WithDescription("Delete Basket")
              .WithSummary("Dlete Basket");


        }
    }

    public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResponse>
    {
        public async Task<DeleteBasketCommandResponse> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteBasketAsync(command.UserName,cancellationToken);
            return new DeleteBasketCommandResponse();
        }
    }
}
