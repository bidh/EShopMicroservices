using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository
        (IBasketRepository repository, IDistributedCache cache) 
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string UserName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(UserName, cancellationToken);

            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }

            var basket = await repository.GetBasketAsync(UserName, cancellationToken);
            await cache.SetStringAsync(UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasketAsync(basket, cancellationToken);
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<bool> DeleteBasketAsync(string UserName, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasketAsync(UserName, cancellationToken);
            await cache.RemoveAsync(UserName, cancellationToken);
            return true;
        }
    }
}
