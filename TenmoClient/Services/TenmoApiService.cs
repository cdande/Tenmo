using RestSharp;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using TenmoClient.Models;
using TenmoServer.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;
        //protected static RestClient client;

        public TenmoApiService(string apiUrl) : base(apiUrl)
        {
            //if (client == null)
            //{
            //    client = new RestClient(apiUrl);
            //}
        }

        // Add methods to call api here...
        public List<ApiUser> GetAllUsers()
        {
            RestRequest request = new RestRequest("user");
            IRestResponse<List<ApiUser>> response = client.Get<List<ApiUser>>(request);
            CheckForError(response);
            return response.Data;
        }
        public ApiUser GetUserByUserId(int id)
        {
            RestRequest request = new RestRequest("id");
            IRestResponse<ApiUser> response = client.Get<ApiUser>(request);
            CheckForError(response);
            return response.Data;
        }
        public Account GetAccountByCurrentUserId()
        {
            RestRequest request = new RestRequest($"account/user/");
            IRestResponse<Account> response = client.Get<Account>(request);
            CheckForError(response);
            return response.Data;
        }
        public Account GetAccountByUserId(int id)
        {
            RestRequest request = new RestRequest($"account/user/{id}");
            IRestResponse<Account> response = client.Get<Account>(request);
            CheckForError(response);
            return response.Data;
        }
        public decimal GetBalanceByUserId()
        {
            RestRequest request = new RestRequest("account/balance");
            IRestResponse<decimal> response = client.Get<decimal>(request);
            CheckForError(response);
            return (decimal)response.Data;
        }
        public ApiUser GetUserByUsername(string username)
        {
            RestRequest request = new RestRequest("login");
            IRestResponse<ApiUser> response = client.Get<ApiUser>(request);
            CheckForError(response);
            return response.Data;
        }
        public List<ApiTransfer> GetTransfersByUserId()
        {
            RestRequest request = new RestRequest("transfer/user");
            IRestResponse<List<ApiTransfer>> response = client.Get<List<ApiTransfer>>(request);
            CheckForError(response);
            return response.Data;
        }
        public User GetUserByAccountId(int id)
        {
            RestRequest request = new RestRequest($"user/account/{id}");
            IRestResponse<User> response = client.Get<User>(request);
            CheckForError(response);
            return response.Data;
        }
        public ApiTransfer CreateSendTransfer(ApiTransfer transfer)
        {
            RestRequest request = new RestRequest("transfer");
            request.AddJsonBody(transfer);
            IRestResponse<ApiTransfer> response = client.Post<ApiTransfer>(request);
            CheckForError(response);
            return response.Data;
        }


        //public List<User> GetUsers()
        //{
        //    RestRequest request = new RestRequest("")
        //}
        //public ApiUser GetUserByUsername(string username)
        //{
        //    RestRequest request = new RestRequest("")
        //}

    }
}
