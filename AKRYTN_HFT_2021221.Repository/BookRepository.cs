using AKRYTN_HFT_2021221.Data;
using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public void UpdateTitle(int id, string newTitle)
        {
            var book = this.GetOneById(id);
            book.b_title = newTitle;
            this.dbContext.SaveChanges();
        }

        public override Book GetOneById(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.b_id == id);
        }

        public void UpdatePublisherid(int id, int newPublisherId)
        {
            var book = this.GetOneById(id);
            book.b_publisher_id = newPublisherId;
            this.dbContext.SaveChanges();
        }

        public void UpdateAuthor(int id, string newAuthor)
        {
            var book = this.GetOneById(id);
            book.b_author = newAuthor;
            this.dbContext.SaveChanges();
        }

        public void UpdatePrice(int id, double newPrice)
        {
            var book = this.GetOneById(id);
            book.b_price = newPrice;
            this.dbContext.SaveChanges();
        }

        public void UpdateReleaseDate(int id, DateTime newReleaseDate)
        {
            var book = this.GetOneById(id);
            book.b_releaseDate = newReleaseDate;
            this.dbContext.SaveChanges();
        }

    }
}
