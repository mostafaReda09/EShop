using EShop.Shared.Exceptions;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException(Guid id) : NotFoundException("product", key: id);
}
