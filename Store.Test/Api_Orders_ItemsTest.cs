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
    using Store.Controllers.Orders;

    public class Api_Orders_ItemsTests : IDisposable
    {
        private readonly TestServer testServer;
        private readonly HttpClient client;

        public Api_Orders_ItemsTests()
        {
            testServer = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Testing")
                .ConfigureServices(services =>
                    {
                        //TODO setup your mocked services here
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
        public async Task Delete_Should_Do_Something_Useful_orderId_itemId()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public async Task Patch_Should_Do_Something_Useful_orderId_itemId_quantity()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public async Task Put_Should_Do_Something_Useful_orderId_itemId_quantity()
        {
            throw new NotImplementedException();
        }
    }
}
