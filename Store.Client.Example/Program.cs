using System;
using Store.Client;
using RestSharp;
using System.Collections.Generic;

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
            var secondItem = storeClient.GetItem(2);

            long newOrderId = storeClient.CreateOrder();
            var newOrder = storeClient.GetOrder(newOrderId);

            newOrderId = storeClient.CreateOrder();
            var oneMoreOrder = storeClient.GetOrder(newOrderId);

            var orders = storeClient.GetOrders();

            newOrder.QuantityByItemId = new Dictionary<long, int>() { {1, 11}, {2, 22 } };
            var orderUpdated = storeClient.PatchOrder(newOrder);

            storeClient.DeleteOrder(newOrderId);
            orders = storeClient.GetOrders();

            newOrderId = storeClient.CreateOrder();
            newOrder = storeClient.GetOrder(newOrderId);

            storeClient.AddQuantityToOrder(firstItem, newOrder, 7);
            storeClient.AddQuantityToOrder(secondItem, newOrder, 3);

            orders = storeClient.GetOrders();

            storeClient.UpdateQuantityInOrder(firstItem, newOrder, 77);

            orders = storeClient.GetOrders();

        }
    }
}
