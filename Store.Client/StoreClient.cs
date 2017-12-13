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

        public void Execute(RestRequest request)
        {
            var response = _restClient.Execute(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                throw new ApplicationException(message, response.ErrorException);
            }            
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

        public IEnumerable<Business.Order> GetOrders()
        {
            var request = new RestRequest { Resource = "Orders" };

            var orders = Execute<List<Transport.Order>>(request);
            return Model.Mapping.Instance.Map<IEnumerable<Business.Order>>(orders);
        }

        public Business.Order GetOrder(long orderId)
        {
            var request = new RestRequest { Resource = "Orders/{orderId}" };
            request.AddParameter("orderId", orderId, ParameterType.UrlSegment);

            var order = Execute<Transport.Order>(request);
            return Model.Mapping.Instance.Map<Business.Order>(order);
        }

        public long CreateOrder()
        {
            var request = new RestRequest { Resource = "Orders", Method = Method.PUT };            

            var newOrderId = Execute<long>(request);

            return newOrderId;
        }

        public Business.Order PatchOrder(Business.Order order)
        {
            var request = new RestRequest { Resource = "Orders/{orderId}", Method = Method.PATCH };
            request.AddParameter("orderId", order.Id, ParameterType.UrlSegment);

            var orderParam = Model.Mapping.Instance.Map<Transport.Order>(order);            
            request.AddParameter("application/json-patch+json", orderParam.Items.ToJson(), ParameterType.RequestBody);

            var orderUpdated = Execute<Transport.Order>(request);
            return Model.Mapping.Instance.Map<Business.Order>(orderUpdated);
        }

        public void DeleteOrder(long orderId)
        {
            var request = new RestRequest { Resource = "Orders/{orderId}", Method = Method.DELETE };
            request.AddParameter("orderId", orderId, ParameterType.UrlSegment);

            Execute(request);            
        }

        public Business.Order AddQuantityToOrder(Business.Item item, Business.Order order, int quantity)
        {
            var request = new RestRequest { Resource = "/orders/{orderId}/items/{itemId}", Method = Method.PUT };
            request.AddParameter("orderId", order.Id , ParameterType.UrlSegment);
            request.AddParameter("itemId", item.Id, ParameterType.UrlSegment);

            request.AddParameter("application/json", quantity, ParameterType.RequestBody);

            var orderUpdated = Execute<Transport.Order>(request);
            return Model.Mapping.Instance.Map<Business.Order>(orderUpdated);
        }

        public Business.Order UpdateQuantityInOrder(Business.Item item, Business.Order order, int quantity)
        {
            var request = new RestRequest { Resource = "/orders/{orderId}/items/{itemId}", Method = Method.PATCH };
            request.AddParameter("orderId", order.Id, ParameterType.UrlSegment);
            request.AddParameter("itemId", item.Id, ParameterType.UrlSegment);

            request.AddParameter("application/json", quantity, ParameterType.RequestBody);

            var orderUpdated = Execute<Transport.Order>(request);
            return Model.Mapping.Instance.Map<Business.Order>(orderUpdated);
        }

    }
}
