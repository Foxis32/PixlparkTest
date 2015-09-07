using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using PixlparkTest.Model;

namespace PixlparkTest.DataProvider
{
    public class DataOrderProvider
    {
        private readonly string _accessToken;
        public DataOrderProvider()
        {
            var accessProvider = new DataAccessProvider();
            _accessToken = accessProvider.GetAccess();
        }

        public IEnumerable<Order> GetOrders()
        {
            return GetOrders(10, 0);
        }

        public IEnumerable<Order> GetOrders(int count, int skip)
        {
            var orders = new List<Order>();
            var orderAddress = string.Format("http://api.pixlpark.com/orders?oauth_token={0}&take={1}&skip={2}",
                Uri.EscapeDataString(_accessToken), count, skip);
            var orderRequest = (HttpWebRequest)WebRequest.Create(orderAddress);
            orderRequest.Method = WebRequestMethods.Http.Get;
            orderRequest.Accept = @"application/json";
            using (var response = orderRequest.GetResponse())
            {
                using (var str = new StreamReader(response.GetResponseStream()))
                {
                    var answer = str.ReadToEnd();
                    var root = JsonConvert.DeserializeObject<RootOrderParser>(answer);
                    orders.AddRange(root.Result);
                }
            }
            return orders;
        }
    }
}
