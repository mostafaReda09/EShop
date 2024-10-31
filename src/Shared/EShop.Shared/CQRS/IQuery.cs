using MediatR;


namespace EShop.Shared.CQRS
{
    public interface IQuery<out TRespone> : IRequest<TRespone> 
        where TRespone : notnull
    {
    }
}
