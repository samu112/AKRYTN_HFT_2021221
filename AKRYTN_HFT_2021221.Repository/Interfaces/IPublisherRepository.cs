using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    public interface IPublisherRepository: IRepository<Publisher>
    {
        void UpdateName(int id, string newName);

        void UpdateAddress(int id, string newAddress);

        void UpdateWebsite(int id, string newWebsite);

        void UpdateEmail(int id, string newEmail);
    }
}
