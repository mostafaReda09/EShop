using EShop.Shared.Exceptions;

namespace Basket.API.Exceptions
{
    public class BasketNotFoundException(string username) : NotFoundException("Basket", username)
    {
    }
}
