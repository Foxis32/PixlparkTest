using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace PixlparkTest.DataProvider
{
    public class DataAccessProvider
    {
        struct RequestTokenParameter
        {
            public string RequestToken { get; set; }
            public string Expires { get; set; }
            public bool Success { get; set; }
        }

        struct AccessTokenParameter
        {
            public string AccessToken { get; set; }
        }

        private readonly string _privateKey = "e8dc9c6a83b949c096ddb611e52bef29";
        private readonly string _publicKey = "dd6a71e250ae4a67a87d381cd5b69ec0";

        public string GetAccess()
        {
            var requestToken = GetRequestToken();
            var hashString = GetHash(requestToken + _privateKey);
            return GetAccessToken(requestToken,hashString);
        }

        private string GetRequestToken()
        {
            string requestToken;
            var tokenRequest = (HttpWebRequest)WebRequest.Create(@"http://api.pixlpark.com/oauth/requesttoken");
            tokenRequest.Method = WebRequestMethods.Http.Get;
            tokenRequest.Accept = @"application/json";

            using (var response = tokenRequest.GetResponse())
            {
                using (var str = new StreamReader(response.GetResponseStream()))
                {
                    var answer = str.ReadToEnd();
                    requestToken = JsonConvert.DeserializeObject<RequestTokenParameter>(answer).RequestToken;
                }
            }
            return requestToken;
        }

        private string GetAccessToken(string requestToken, string hashString)
        {
            string accessToken;
            var accessTokenAddress = string.Format(
                "http://api.pixlpark.com/oauth/accesstoken?oauth_token={0}&grant_type=api&username={1}&password={2}",
                Uri.EscapeDataString(requestToken), Uri.EscapeDataString(_publicKey),
                Uri.EscapeDataString(hashString));
            var accessRequest = (HttpWebRequest)WebRequest.Create(accessTokenAddress);
            accessRequest.Method = WebRequestMethods.Http.Get;
            accessRequest.Accept = @"application/json";
            using (var response = accessRequest.GetResponse())
            {
                using (var str = new StreamReader(response.GetResponseStream()))
                {
                    var answer = str.ReadToEnd();
                    accessToken = JsonConvert.DeserializeObject<AccessTokenParameter>(answer).AccessToken;
                }
            }
            return accessToken;
        }

        private string GetHash(string password)
        {
            var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.Default.GetBytes(password));
            return string.Join("", hash.Select(x => x.ToString("x2")).ToArray());
        } 
    }
}
