using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    public interface ICartRepository : IRepository<Cart>
    {
        void UpdateCreditCard(int id, string newCreditCard);

        void UpdateBillingAddress(int id, string newBillingAddress);

        void UpdateDeliver(int id, bool newDeliver);

        void UpdateUserId(int id, int newUserId);

    }
}
