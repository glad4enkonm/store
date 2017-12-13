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
        }
    }
}
