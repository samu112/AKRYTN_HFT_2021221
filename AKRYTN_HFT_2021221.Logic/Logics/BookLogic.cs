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

        public void ChangeBook(int id, Book book)
        {
            string newTitle = book.b_title;
            string newAuthor = book.b_author;
            double newPrice = book.b_price;
            DateTime newRelease = book.b_releaseDate;
            int newPublisher = book.b_publisher_id;

            Book oldBook = this.bookRepo.GetOneById(id);

            if (oldBook.b_title != newTitle)
            {
                this.bookRepo.UpdateTitle(id, newTitle);
            }
            if (oldBook.b_author != newAuthor)
            {
                this.bookRepo.UpdateAuthor(id, newAuthor);
            }
            if (oldBook.b_price != newPrice)
            {
                this.bookRepo.UpdatePrice(id, newPrice);
            }
            if (oldBook.b_releaseDate != newRelease)
            {
                this.bookRepo.UpdateReleaseDate(id, newRelease);
            }
            if (oldBook.b_publisher_id != newPublisher)
            {
                this.bookRepo.UpdatePublisherid(id, newPublisher);
            }
        }
    }
}
