using AKRYTN_HFT_2021221.Data;
using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    public class ConnectorRepository : Repository<OrderCart_Connector>, IConnectorRepository
    {
        public ConnectorRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public override OrderCart_Connector GetOneById(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.occ_id == id);
        }

        public void UpdateCartId(int id, int newCartId)
        {
            var connector = this.GetOneById(id);
            connector.occ_cart_id = newCartId;
            this.dbContext.SaveChanges();
        }

        public void UpdateItemId(int id, int newItemId)
        {
            var connector = this.GetOneById(id);
            connector.occ_cartItem_id = newItemId;
            this.dbContext.SaveChanges();
        }
    }
}
