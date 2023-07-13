using RestSharp;
using System.Dynamic;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using TenmoClient.Models;


namespace TenmoClient.Services
{
    public class TransferApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;
        //protected static RestClient client;

        public TransferApiService(string apiUrl) : base(apiUrl)
        {
            //if (client == null)
            //{
            //    client = new RestClient(apiUrl);
            //}
        }
    }
}
