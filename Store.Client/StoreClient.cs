using System;
using RestSharp;
using System.Collections.Generic;
using RestSharp.Serializers;
using Store.Model.Transport;

namespace Store.Client
{
    public class StoreClient
    {
        IRestClient _restClient;

        public StoreClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var response = _restClient.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";                
                throw new ApplicationException(message, response.ErrorException);
            }
            return response.Data;            
        }

        public IEnumerable<Item> GetItems()
        {
            var request = new RestRequest
            {
                Resource = "Items"
            };            

            return Execute<List<Item>>(request);
        }

    }
}
