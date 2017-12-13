using System;
using Store.Client;
using RestSharp;

namespace Store.Client.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            const string baseUrl = "http://localhost:59801/";
            IRestClient restClient = new RestClient() { BaseUrl = new Uri(baseUrl) };

            var storeClient = new StoreClient(restClient);
            var items =  storeClient.GetItems();

            var firstItem = storeClient.GetItem(1);

            long newOrderId = storeClient.CreateOrder();
            var newOrder = storeClient.GetOrder(newOrderId);

            newOrderId = storeClient.CreateOrder();
            var oneMoreOrder = storeClient.GetOrder(newOrderId);

            var orders = storeClient.GetOrders();

        }
    }
}
