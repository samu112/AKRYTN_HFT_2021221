using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        void UpdateTitle(int id, string newTitle);

        void UpdatePublisherid(int id, int newPublisherId);

        void UpdateAuthor(int id, string newAuthor);

        void UpdatePrice(int id, double newPrice);

        void UpdateReleaseDate(int id, DateTime newReleaseDate);

    }
}
