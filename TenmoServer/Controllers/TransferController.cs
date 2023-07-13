using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TenmoServer.DAO;
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
    public class TransferController : ControllerBase
    {
        private readonly ITransferDao dao;
        public TransferController(ITransferDao transferDao)
        {
            dao = transferDao;
        }

        [HttpGet("{id}")]
        public ActionResult<Transfer> GetTransferByTransferId(int id)
        {
            int userId = int.Parse(User.FindFirst("sub")?.Value);
            IList<Transfer> userTransfers = dao.GetTransfersByUserId(userId);
            Transfer transfer = dao.GetTransferByTransferId(id);
            if (transfer != null)
            {
                return Ok(transfer);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("user")]
        public ActionResult<List<Transfer>> GetTransfersByUserId()
        {
            int userId = int.Parse(User.FindFirst("sub")?.Value);
            IList<Transfer> transfers = dao.GetTransfersByUserId(userId);
            if (transfers != null)
            {
                return Ok(transfers);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("transfer_status/{id}")]
        public ActionResult<List<Transfer>> GetTransferByStatus(int id)
        {
            IList<Transfer> transfers = dao.GetTransfersByStatus(id);
            if (transfers != null)
            {
                return Ok(transfers);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost()]
        public ActionResult<Transfer> CreateSendTransfer(int recieverUserId, int amount, Transfer transfer)
        {
            if(transfer.AccountFrom == recieverUserId)
            {
                return Conflict();
            }
            Transfer transfer1 = dao.CreateSendTransfer(recieverUserId, amount, transfer);
            //if (transfer1.Amount > )

            return Created($"/transfer/{transfer1.TransferId}", transfer1);
        }

    }
}
