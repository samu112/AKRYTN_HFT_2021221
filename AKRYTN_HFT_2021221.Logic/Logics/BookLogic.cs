using AKRYTN_HFT_2021221.Models;
using AKRYTN_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    public class BookLogic : IBookLogic
    {
        private IBookRepository bookRepo;
        private ICartItemRepository cartItemRepo;
        private ICartRepository cartRepo;

        public BookLogic()
        {
            this.bookRepo = new BookRepository(new Data.BookStoreDbContext());
        }
        //Constructor overload for testing.
        public BookLogic(IBookRepository bookRepo, ICartItemRepository cartItemRepo, ICartRepository cartRepo)
        {
            this.bookRepo = bookRepo;
            this.cartItemRepo = cartItemRepo;
            this.cartRepo = cartRepo;
        }

        //NON-CRUD METHODS:

        public IEnumerable<string> GetBookPublisherName(int id)
        {
            throw new NotImplementedException();
        }

        //CRUD METHODS:

        public bool DeleteBook(int id)
        {    
            if (bookRepo.GetAll().Any(book => book.b_id == id))
            {
                //Find all cartItems with this book, and find the carts that have these cartItems
                var GetCartItemsWithThisBook = cartItemRepo.GetAll().Where(cartItem => cartItem.ci_book_id == id);
                if (GetCartItemsWithThisBook.Count() != 0)
                {
                    var GetCartsWithTheseCartItems = cartRepo.GetAll().Where(cart => GetCartItemsWithThisBook.Any(cartItem => cartItem.ci_cart_id == cart.c_id));
                    if (GetCartsWithTheseCartItems.Count() != 0)
                    {
                        foreach (Cart cart in GetCartsWithTheseCartItems)
                        {
                            foreach (CartItem cartItem in GetCartItemsWithThisBook)
                            {
                                if (cartItem.ci_cart_id == cart.c_id)
                                {
                                    cartItemRepo.Remove(cartItem.ci_id);
                                }
                            }
                        }
                    }

                }//If we found cartItem with this book in it
                bookRepo.Remove(id);
                //If the book wasn't in any cartItem
                return true;
            } //If book with this Id does EXIST
            else
            {
                return false;
            } //If book with this Id does NOT exist
        }

        public Book GetBook(int id)
        {
            return this.bookRepo.GetOneById(id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return this.bookRepo.GetAll().ToList();
        }

        public void AddBook(Book book)
        {
            this.bookRepo.Insert(book);
        }

        public void ChangeBookAuthor(int id, string newAuthor)
        {
            this.bookRepo.UpdateAuthor(id, newAuthor);
        }

        public void ChangeBookTitle(int id, string newTitle)
        {
            this.bookRepo.UpdateTitle(id, newTitle);
        }

        public void ChangeBookPrice(int id, int newPrice)
        {
            this.bookRepo.UpdatePrice(id, newPrice);
        }

        public void ChangeBookPublisher(int id, int newid)
        {
            this.bookRepo.UpdatePublisherid(id, newid);
        }

        public void ChangeBookReleaseDate(int id, DateTime newDate)
        {
            this.bookRepo.UpdateReleaseDate(id, newDate);
        }
    }
}
