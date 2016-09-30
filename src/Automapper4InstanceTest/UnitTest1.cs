using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automapper5InstanceTest
{
    using System.Threading;

    using AutoMapper;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mapperConfig = new MapperConfiguration(
                config =>
                {
                    config.CreateMap<OrderContainer, OrderContainerDto>();
                    config.CreateMap<Order, OrderDto>();

                });

            var mapper = mapperConfig.CreateMapper();

            var order = new Order();
            var orderContainer = new OrderContainer();
            order.Container = orderContainer;
            orderContainer.A = order;
            orderContainer.B = order;



            var dto = mapper.Map<OrderContainerDto>(orderContainer);

            Assert.AreSame(dto.A, dto.B);
            Assert.AreSame(dto, dto.A.Container);
        }
    }

    public class OrderContainer
    {
        public Order A { get; set; }
        public Order B { get; set; }
    }

    public class Order
    {
        public int ID { get; set; }

        public OrderContainer Container { get; set; }
    }

    public class OrderContainerDto
    {
        public OrderDto A { get; set; }
        public OrderDto B { get; set; }
    }

    public class OrderDto
    {
        public int ID { get; set; }

        public OrderContainerDto Container { get; set; }
    }
}
