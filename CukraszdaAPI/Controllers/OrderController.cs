using CommonLibrary.MODEL;
using Microsoft.AspNetCore.Mvc;

namespace CukraszdaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ProductDbContext _context = new ProductDbContext();

        /// <summary>
        /// Új rendelés fej (OrderHead) létrehozása.
        /// </summary>
        /// <param name="orderHead">A rendelés fej adatai.</param>
        /// <returns>Az új OrderHead objektum (generált ID-val).</returns>
        [HttpPost]
        public async Task<ActionResult<OrderHead>> CreateOrderHead([FromBody] OrderHead orderHead)
        {
            if (orderHead == null)
            {
                return BadRequest();
            }

            _context.OrderHeads.Add(orderHead);
            await _context.SaveChangesAsync();

            return Ok(orderHead);
        }

        /// <summary>
        /// Új rendelés tétel (OrderItem) hozzáadása egy rendeléshez.
        /// </summary>
        /// <param name="orderItem">A rendelés tétel adatai.</param>
        /// <returns>Status kód.</returns>
        [HttpPost("item")]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItem orderItem)
        {
            if (orderItem == null)
            {
                return BadRequest();
            }

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
