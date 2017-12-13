using System;
using RestSharp;
using System.Collections.Generic;
using Business = Store.Model.Business;
using Transport = Store.Model.Transport;

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

        public IEnumerable<Business.Item> GetItems()
        {
            var request = new RestRequest { Resource = "Items" };
            
            var items = Execute<List<Transport.Item>>(request);
            return Model.Mapping.Instance.Map<IEnumerable<Business.Item>>(items);
        }

        public Business.Item GetItem(long itemId)
        {
            var request = new RestRequest { Resource = "Items/{itemId}" };
            request.AddParameter("itemId", itemId, ParameterType.UrlSegment);

            var items = Execute<Transport.Item>(request);
            return Model.Mapping.Instance.Map<Business.Item>(items);
        }

    }
}
