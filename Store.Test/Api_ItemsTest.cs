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

    public class Api_ItemsTests : IDisposable
    {
        private readonly TestServer testServer;
        private readonly HttpClient client;

        public Api_ItemsTests()
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
        public async Task Get_Should_Do_Something_Useful()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public async Task Get_Should_Do_Something_Useful_itemId()
        {
            throw new NotImplementedException();
        }
    }
}
