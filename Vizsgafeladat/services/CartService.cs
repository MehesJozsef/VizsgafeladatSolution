using CommonLibrary.MODEL;
using System.Net.Http.Json;
using Vizsgafeladat.VIEWMODEL;

namespace Vizsgafeladat.services
{
    public class CartService
    {
        private readonly HttpClient _httpClient;

        public List<CartItem> CartItems { get; private set; } = new List<CartItem>();

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Termék hozzáadása a kosárhoz. Ha létezik, növeli a mennyiséget.
        /// </summary>
        public void AddToCart(Product product, int quantity)
        {
            var existingItem = CartItems.FirstOrDefault(x => x.Product.Product_id == product.Product_id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                CartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity,
                    Price = product.Price
                });
            }
        }

        /// <summary>
        /// Termék eltávolítása a kosárból.
        /// </summary>
        public void RemoveFromCart(Product product)
        {
            CartItems.RemoveAll(x => x.Product.Product_id == product.Product_id);
        }

        /// <summary>
        /// Teljes kosár kiürítése.
        /// </summary>
        public void ClearCart()
        {
            CartItems.Clear();
        }

        /// <summary>
        /// A kosárban lévő termékek összesített árának kiszámítása.
        /// </summary>
        public int GetTotalPrice()
        {
            return CartItems.Sum(x => x.Price.GetValueOrDefault() * x.Quantity.GetValueOrDefault()); // Ha null lenne ne hibázzon
        }

        /// <summary>
        /// Elküldi a rendelést az API-nak:
        /// előbb a rendelés fej adatait (OrderHead), majd minden tételt (OrderItem) külön-külön.
        /// </summary>
        /// <returns>Az új rendelés azonosítója, vagy 0 hiba esetén.</returns>
        public async Task<int> SaveOrderAsync()
        {
            if (CartItems.Count == 0)
                return 0;

            // 1. OrderHead mentése
            var orderHead = new OrderHead
            {
                UserId = UserSession.CurrentUser.Id,
                OrderDate = DateTime.Now,
                TotalAmount = GetTotalPrice(),
                IsPaid = false,
                StatusId = 1,
                Remark = ""
            };

            var response = await _httpClient.PostAsJsonAsync("api/order", orderHead);
            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }

            var createdOrderHead = await response.Content.ReadFromJsonAsync<OrderHead>();
            if (createdOrderHead == null)
            {
                return 0;
            }

            int orderHeadId = createdOrderHead.OrderHeadId;

            // 2. OrderItem tételek mentése
            foreach (var item in CartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderHeadId = orderHeadId,
                    ProductId = item.Product.Product_id,
                    Quantity = item.Quantity.GetValueOrDefault(),
                    UnitPrice = item.Price.GetValueOrDefault()
                };

                var itemResponse = await _httpClient.PostAsJsonAsync("api/orderitem", orderItem);
                if (!itemResponse.IsSuccessStatusCode)
                {
                    return 0;
                }
            }

            // 3. Kosár ürítése
            ClearCart();

            return orderHeadId;
        }
    }
}
