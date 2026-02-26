using AutoMapper;
using DTO_s;
using Enteties;
using Repositories;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        public async Task<OrdersDTO> GetOrderById(int id)
        {
            Order order = await _ordersRepository.GetOrderById(id);
            OrdersDTO orderDto = _mapper.Map<Order, OrdersDTO>(order);
            return orderDto;
        }

        public async Task<OrdersDTO> AddNewOrder(OrdersDTO orderDto)
        {
            Order order = _mapper.Map<OrdersDTO, Order>(orderDto);
            Order order1 = await _ordersRepository.AddOrder(order);
            OrdersDTO orderDto1 = _mapper.Map<Order, OrdersDTO>(order1);
            return orderDto1;
        }
    }
}
