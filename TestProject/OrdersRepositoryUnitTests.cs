using Enteties;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    // Unit tests using Moq + MockQueryable to mock ApiShopContext without a real database.
    public class OrdersRepositoryUnitTests
    {
        private readonly Mock<ApiShopContext> _mockContext;
        private readonly IOrdersRepository _repository;

        public OrdersRepositoryUnitTests()
        {
            // ApiShopContext requires DbContextOptions - pass a dummy options instance so Moq can proxy the class
            var options = new DbContextOptionsBuilder<ApiShopContext>().Options;
            _mockContext = new Mock<ApiShopContext>(options);
            _repository = new OrdersRepository(_mockContext.Object);
        }

        // Helper: builds a mock DbSet<Order> that supports async LINQ (Include, FirstOrDefaultAsync, etc.)
        private static Mock<DbSet<Order>> BuildOrderMockDbSet(List<Order> data)
            => data.AsQueryable().BuildMockDbSet();

        [Fact]
        public async Task GetOrderById_ReturnsOrder_WithOrderItems_WhenOrderExists()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 2 },
                    new OrderItem { ProductId = 2, Quantity = 1 }
                }
            };
            var mockDbSet = BuildOrderMockDbSet(new List<Order> { order });
            _mockContext.Setup(c => c.Orders).Returns(mockDbSet.Object);

            // Act
            var result = await _repository.GetOrderById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
            Assert.Equal(2, result.OrderItems.Count);
        }

        [Fact]
        public async Task GetOrderById_ReturnsNull_WhenOrderDoesNotExist()
        {
            // Arrange
            var mockDbSet = BuildOrderMockDbSet(new List<Order>());
            _mockContext.Setup(c => c.Orders).Returns(mockDbSet.Object);

            // Act
            var result = await _repository.GetOrderById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetOrderById_ReturnsCorrectOrder_WhenMultipleOrdersExist()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { OrderId = 1, OrderItems = new List<OrderItem>() },
                new Order { OrderId = 2, OrderItems = new List<OrderItem> { new OrderItem { ProductId = 5, Quantity = 3 } } }
            };
            var mockDbSet = BuildOrderMockDbSet(orders);
            _mockContext.Setup(c => c.Orders).Returns(mockDbSet.Object);

            // Act
            var result = await _repository.GetOrderById(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.OrderId);
        }

        [Fact]
        public async Task AddOrder_ReturnsOrder_WhenOrderIsAdded()
        {
            // Arrange
            var order = new Order { OrderId = 1, OrderItems = new List<OrderItem>() };
            var mockDbSet = BuildOrderMockDbSet(new List<Order>());
            _mockContext.Setup(c => c.Orders).Returns(mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _repository.AddOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.OrderId, result.OrderId);
        }

        [Fact]
        public async Task AddOrder_ShouldCallSaveChangesAsync_Once()
        {
            // Arrange
            var order = new Order { OrderId = 1, OrderItems = new List<OrderItem>() };
            var mockDbSet = BuildOrderMockDbSet(new List<Order>());
            _mockContext.Setup(c => c.Orders).Returns(mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            await _repository.AddOrder(order);

            // Assert
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddOrder_ShouldCallAddAsync_OnOrdersDbSet()
        {
            // Arrange
            var order = new Order { OrderId = 1, OrderItems = new List<OrderItem>() };
            var mockDbSet = BuildOrderMockDbSet(new List<Order>());
            _mockContext.Setup(c => c.Orders).Returns(mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            await _repository.AddOrder(order);

            // Assert
            mockDbSet.Verify(d => d.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddOrder_WithOrderItems_ShouldReturnOrderWithItems()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 10, Quantity = 5 }
                }
            };
            var mockDbSet = BuildOrderMockDbSet(new List<Order>());
            _mockContext.Setup(c => c.Orders).Returns(mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _repository.AddOrder(order);

            // Assert
            Assert.Single(result.OrderItems);
            Assert.Equal(5, result.OrderItems.First().Quantity);
        }
    }
}
