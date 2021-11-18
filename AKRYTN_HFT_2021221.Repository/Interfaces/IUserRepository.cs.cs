using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        void UpdateName(int id, string newName);

        void UpdateRegDate(int id, DateTime newDate);

        void UpdateAddress(int id, string newAddres);

        void UpdateEmail(int id, string newEmail);

    }
}
