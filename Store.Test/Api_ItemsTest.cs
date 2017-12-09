using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Store.Test
{    
    using Store.Controllers;
    using Store.Domain;
    using Store.Domain.Exceptions;
    using Store.Domain.Repositories;
    using Store.Models;
    using System.Net;

    public class Api_ItemsTests : IDisposable
    {
        private readonly TestServer testServer;
        private readonly HttpClient client;

        private readonly Mock<IItemRepository> repositoryMock;

        public Api_ItemsTests()
        {
            repositoryMock = new Mock<IItemRepository>();            

            testServer = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Testing")
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IItemRepository>(repositoryMock.Object);
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
        public async Task Get_Should_Return_Item_If_Id_Is_Valid_And_Item_Exist()
        {
            var item = new Domain.Item
            {
                Id = 1000,
                Description = "Test",
                Price = 100.23M
            };

            repositoryMock
                .Setup(repo => repo.GetById(item.Id))
                .ReturnsAsync(item)
                .Verifiable();

            var response = await client.GetAsync($"/items/{item.Id}");

            Assert.True(response.IsSuccessStatusCode);

            var responseItem = JsonConvert.DeserializeObject<Models.Item>(await response.Content.ReadAsStringAsync());

            Models.Item modelItem = Mapping.Instance.Map<Models.Item>(item);
            Assert.Equal( modelItem, responseItem);

            repositoryMock.Verify();
        }

        [Fact]
        public async Task Should_Return_404_When_Valid_But_Absent_Id()
        {
            repositoryMock
                .Setup(repo => repo.GetById(It.IsAny<long>()))
                .Throws<ItemNotFoundException>();

            var response = await client.GetAsync($"/items/{0L}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Should_Return_400_When_Id_Invalid()
        {
            var response = await client.GetAsync("/items/not-a-number");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
