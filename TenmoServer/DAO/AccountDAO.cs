using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TenmoServer.Exceptions;
using TenmoServer.Models;
using TenmoServer.Security;
using TenmoServer.Security.Models;
namespace TenmoServer.DAO
{
    public class AccountDAO : IAccountDAO
    {
        private readonly string connectionString;

        public AccountDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account GetAccountByUserId(int id)
        {
            Account account = null;

            string sql = "SELECT account_id, user_id, balance FROM account WHERE user_id = @user_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        account = MapRowToAccount(reader);
                    }
                }
            }
            catch (SqlException x)
            {
                throw new DaoException("SQL exception occurred", x);
            }
            return account;
        }
        public IList<Account> GetAccounts()
        {
            IList<Account> accounts = new List<Account>();

            string sql = "SELECT account_id, user_id, balance FROM account";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Account account = MapRowToAccount(reader);
                        accounts.Add(account);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return accounts;
        }
        public decimal GetBalanceByUserId(int id)
        {
            decimal balance = 0M;
            string sql = "SELECT balance FROM account WHERE user_id = @user_id";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        balance = Convert.ToDecimal(reader["balance"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL Exception Occurred", ex);
            }
            return balance;
        }
        
        
        private Account MapRowToAccount(SqlDataReader reader)
        {
            Account account = new Account();
            account.AccountId = Convert.ToInt32(reader["account_id"]);
            account.UserId = Convert.ToInt32(reader["user_id"]);
            account.Balance = Convert.ToDecimal(reader["balance"]);
            return account;
        }
    }
}
