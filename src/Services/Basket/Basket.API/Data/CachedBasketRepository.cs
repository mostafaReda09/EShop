using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository,IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
           
            await basketRepository.DeleteBasketAsync(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cahedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if(!string.IsNullOrWhiteSpace(cahedBasket))
            {
               return JsonSerializer.Deserialize<ShoppingCart>(cahedBasket)!;
            } 
            var basket= await basketRepository.GetBasketAsync(userName, cancellationToken);
            await cache.SetStringAsync(userName,JsonSerializer.Serialize(basket));
            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {

           var basketInDb= await basketRepository.StoreBasketAsync(basket, cancellationToken);
            await cache.SetStringAsync(basketInDb.UserName,JsonSerializer.Serialize(basketInDb));
            return basketInDb;
        }
    }
}
