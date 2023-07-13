using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        Transfer GetTransferByTransferId(int id);
        IList<Transfer> GetTransfersByUserId(int id);
        List<Transfer> GetTransfersByStatus(int statusId);
        Transfer CreateSendTransfer(int receiverUserId, int amount, Transfer transfer);
        //Transfer CreateRequestTransfer(int senderUserId);
    }
}
