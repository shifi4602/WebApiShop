using AutoMapper;
using DTO_s;
using Enteties;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrdersRepository _iOrdersRepository;
        IProductReposetory _productReposetory;
        IMapper _mapper;
        public OrderService(IOrdersRepository iOrdersRepository, IProductReposetory productReposetory, IMapper mapper)
        {
            _iOrdersRepository = iOrdersRepository;
            _productReposetory = productReposetory;
            _mapper = mapper;
        }

        public async Task<OrdersDTO> GetOrderById(int id)
        {
            Order order = await _iOrdersRepository.GetOrderById(id);
            OrdersDTO orderDTO = _mapper.Map<Order, OrdersDTO>(order);
            return orderDTO;
        }
        public async Task<OrdersDTO> AddNewOrder(OrdersDTO orderDTO)
        {
            Order order = _mapper.Map<OrdersDTO, Order>(orderDTO);
            Order orderRes = await _iOrdersRepository.AddOrder(order);
            OrdersDTO orderDtoRes = _mapper.Map<Order, OrdersDTO>(orderRes);
            return orderDtoRes;
        }

        public async Task<bool> CheckOrderSum(OrdersDTO order)
        {
            double? sum = 0;
            foreach (var item in order.OrderItems)
            {
                Product product = await _productReposetory.GetProductById(item.ProductId);
                if (product != null)
                    sum += product.Price * item.Quantity;
            }
            if (sum == order.OrderSum)
                return true;
            return false;
        }
    }
}
