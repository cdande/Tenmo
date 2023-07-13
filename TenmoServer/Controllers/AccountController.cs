using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TenmoServer.DAO;
using TenmoServer.Exceptions;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDAO dao;

        public AccountController(IAccountDAO accountDao)
        {
            dao = accountDao;
        }

        [HttpGet()]
        public ActionResult<List<Account>> GetAccounts()
        {
            IList<Account> accounts = dao.GetAccounts();
            if(accounts != null)
            {
                return Ok(accounts);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("user")]
        public ActionResult<Account> GetAccountByCurrentUserId()
        {
            int userId = int.Parse(User.FindFirst("sub")?.Value);
            Account account = dao.GetAccountByUserId(userId);
            if(account != null)
            {
                return Ok(account);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("user/{id}")]
        public ActionResult<Account> GetAccountByUserId(int id)
        {
            Account account = dao.GetAccountByUserId(id);
            if(account != null)
            {
                return Ok(account);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("balance")]
        public ActionResult<decimal> GetBalanceByUserId()
        {
            int userId = int.Parse(User.FindFirst("sub")?.Value);
            decimal balance = dao.GetBalanceByUserId(userId);
            if(balance != null)
            {
                return Ok(balance);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
