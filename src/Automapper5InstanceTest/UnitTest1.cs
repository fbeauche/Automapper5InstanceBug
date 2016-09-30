using System;
using AutoMapper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automapper5InstanceTest
{
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
                    config.CreateMap<Order, OrderDto>()
                    // Next line is needed in version 5.1.1 only to avoid StackOverflow. In 4.2.1 it was not needed
                      .ForMember(s => s.Container, opt => opt.Ignore()); 
            
                });

            var mapper = mapperConfig.CreateMapper();


            var order = new Order();
            var orderContainer = new OrderContainer();
            order.Container = orderContainer;
            orderContainer.A = order; // A & B contains the same object instance
            orderContainer.B = order;

            var dto = mapper.Map<OrderContainerDto>(orderContainer);

            Assert.AreSame(dto.A, dto.B); // With 4.2.1, this is true
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
