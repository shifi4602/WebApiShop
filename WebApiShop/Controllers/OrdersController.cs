using DTO_s;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersDTO>> GetOrderById(int id)
        {
            OrdersDTO orderDTO = await _orderService.GetOrderById(id);
            if (orderDTO != null)
            {
                return Ok(orderDTO);
            }
            else
                return NoContent();
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<OrdersDTO>> Post([FromBody] OrdersDTO orderDto)
        {
            OrdersDTO order1 = await _orderService.AddNewOrder(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { order1.OrderId }, order1);
        }
    }
}
