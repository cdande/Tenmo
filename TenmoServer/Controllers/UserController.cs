using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using TenmoServer.DAO;
using TenmoServer.Exceptions;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UserController : Controller
    {

        private readonly IUserDao userDao;
        public UserController(IUserDao userDao)
        {
            this.userDao = userDao;
        }
        [Authorize]
        [HttpGet()]
        public ActionResult<IList<User>> GetUsers()
        {
            IList<User> users = new List<User>();
            users = userDao.GetUsers();
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("account/{id}")]
        public ActionResult<User> GetUserByAccountId(int id)
        {
            User user = null;
            user = userDao.GetUserByAccountId(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }


    }
}
