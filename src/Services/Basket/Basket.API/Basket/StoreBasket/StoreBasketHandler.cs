using Basket.API.Data;
using Basket.API.Entities;
using Carter;
using EShop.Shared.CQRS;
using FluentValidation;
using MediatR;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketCommandResponse>;
    public record StoreBasketCommandResponse(string UserName);
    public class StoreBasketCommandEndPint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketCommand command, ISender sender) =>
            {

                var result = await sender.Send(command);
                return Results.Created($"/basket/{result.UserName}",result);
            })
              .WithName("Store Basket")
              .WithDescription("Store Basket")
              .WithSummary("Store Basket")
              .Produces<StoreBasketCommandResponse>();

              
        }
    }
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
          RuleFor(x=>x.Cart).NotNull().WithMessage("Cart Can not be null");
          RuleFor(x=>x.Cart.UserName).NotEmpty().WithMessage("User name is required");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketCommandResponse>
    {
        public async Task<StoreBasketCommandResponse> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {

            var result = await repository.StoreBasketAsync(command.Cart, cancellationToken);
            return new StoreBasketCommandResponse(result.UserName);
        }
    }
}
