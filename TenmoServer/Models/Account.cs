namespace TenmoServer.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }

        //public int CheckBalance(int accountId)
        //{
        //    if (accountId < 0
        //}
    }
}
