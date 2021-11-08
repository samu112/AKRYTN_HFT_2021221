using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        void UpdateQuantity(int id, int newQuantity);

        void UpdateBookId(int id, int newBookId);

    }
}
