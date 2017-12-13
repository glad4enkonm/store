using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Store.Test
{
    using Store.Model;
    using Store.Model.Business.Repositories;
    using Store.Model.Business.Repositories.Exceptions;
    using System.Net;
    using Business = Store.Model.Business;
    using Transport = Store.Model.Transport;

    public class Api_OrdersTests : IDisposable
    {
        private readonly TestServer testServer;
        private readonly HttpClient client;

        private readonly Mock<IOrderRepository> repositoryMock;

        public Api_OrdersTests()
        {
            repositoryMock = new Mock<IOrderRepository>();

            testServer = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Testing")
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IOrderRepository>(repositoryMock.Object);
                })
            );

            client = testServer.CreateClient();
        }

        public void Dispose()
        {
            client.Dispose();
            testServer.Dispose();
        }

        [Fact]
        public async Task Get_Should_Return_Orders()
        {
            IEnumerable<Business.Order> orders = 
                new List<Business.Order>() {
                    new Business.Order() { Id = 100 },
                    new Business.Order() { Id = 102 } ,
                    new Business.Order() { Id = 103 }
                };

            repositoryMock
                .Setup(repo => repo.List())
                .ReturnsAsync(orders)
                .Verifiable();

            var response = await client.GetAsync($"/orders");
            var responceOrdersList =
                JsonConvert.DeserializeObject<Transport.OrderList>(await response.Content.ReadAsStringAsync());

            Assert.True(response.IsSuccessStatusCode);
            var ordersList =
                Mapping.Instance.Map<Transport.OrderList>(orders);
            Assert.Equal(ordersList.ToString(), responceOrdersList.ToString());

            repositoryMock.Verify();
        }

        [Fact]
        public async Task Get_Should_Return_Order_If_Id_Is_Valid_And_Order_Exist()
        {
            var order = new Business.Order
            {
                Id = 101,
                QuantityByItemId = new Dictionary<long, int>() { { 1, 2 }, { 3, 4 } }
            };

            repositoryMock
                .Setup(repo => repo.GetById(order.Id))
                .ReturnsAsync(order)
                .Verifiable();

            var response = await client.GetAsync($"/orders/{order.Id}");

            Assert.True(response.IsSuccessStatusCode);

            var responseOrder = JsonConvert.DeserializeObject<Transport.Order>(await response.Content.ReadAsStringAsync());

            Transport.Order modelOrder = Mapping.Instance.Map<Transport.Order>(order);
            Assert.Equal(modelOrder.ToString(), responseOrder.ToString());

            repositoryMock.Verify();
        }

        [Fact]
        public async Task Should_Return_404_When_Valid_But_Absent_Id()
        {
            repositoryMock
                .Setup(repo => repo.GetById(It.IsAny<long>()))
                .Throws<OrderNotFoundException>();

            var response = await client.GetAsync($"/oders/{0L}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Should_Return_400_When_Id_Invalid()
        {
            var response = await client.GetAsync("/orders/not-a-number");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_Return_Order_Id_If_It_Was_Created()
        {
            var newOrder = new Business.Order() { Id = 100};

            repositoryMock
                .Setup(repo => repo.Create())
                .ReturnsAsync(newOrder)
                .Verifiable();

            var response = await client.PutAsync($"/orders", null);
            var responceId = 
                JsonConvert.DeserializeObject<long>(await response.Content.ReadAsStringAsync());

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(newOrder.Id, responceId);

            repositoryMock.Verify();
        }

        //[Fact]
        //public async Task Delete_Should_Do_Something_Useful_orderId()
        //{
        //    throw new NotImplementedException();
        //}

        //[Fact]
        //public async Task Get_Should_Do_Something_Useful_orderId()
        //{
        //    throw new NotImplementedException();
        //}

        //[Fact]
        //public async Task Patch_Should_Do_Something_Useful_orderId_quantityList()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
