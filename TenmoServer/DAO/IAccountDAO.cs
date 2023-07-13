using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        IList<Account> GetAccounts();
        Account GetAccountByUserId(int id);
        //Account CreateAccount(Account account);
        decimal GetBalanceByUserId(int id);

    }
}
