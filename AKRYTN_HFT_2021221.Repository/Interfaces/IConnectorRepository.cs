using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    public interface IConnectorRepository : IRepository<OrderCart_Connector>
    {
        void UpdateCartId(int id, int newCartId);

        void UpdateItemId(int id, int newItemId);
    }
}
