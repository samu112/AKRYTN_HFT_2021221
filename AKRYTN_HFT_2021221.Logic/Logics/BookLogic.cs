using AKRYTN_HFT_2021221.Models;
using AKRYTN_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Data;
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
        private IPublisherRepository publisherRepo;

        //Constructor overload for testing.
        public BookLogic(IBookRepository bookRepo, ICartItemRepository cartItemRepo, ICartRepository cartRepo, IPublisherRepository publisherRepo)
        {
            this.bookRepo = bookRepo;
            this.cartItemRepo = cartItemRepo;
            this.cartRepo = cartRepo;
            this.publisherRepo = publisherRepo;
        }

        //NON-CRUD METHODS:

        //Get publisher of the book
        public Publisher GetBookPublisher(int id)
        {
           Book book = bookRepo.GetOneById(id);
           Publisher publisher = publisherRepo.GetOneById(book.b_publisher_id);
           return publisher;
        }

        public IEnumerable<IGrouping<string, Book>> GetBooksGroupedByPublisher()
        {
            var everybook = bookRepo.GetAll();
            var everypublisher = publisherRepo.GetAll();
            var books = (from book in everybook
                         join publisher in everypublisher
                         on book.b_publisher_id equals publisher.p_id
                         group book by publisher.p_name into g
                         select g).AsEnumerable(); ;
            return books;
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
            Book idlessBook = new Book();

            //Title check
            if (!string.IsNullOrEmpty(book.b_title) && !string.IsNullOrWhiteSpace(book.b_title))
            {
                idlessBook.b_title = book.b_title;
            }
            else { throw new ArgumentNullException("Book title must have a value!"); }
            //Author check
            if (!string.IsNullOrEmpty(book.b_author) && !string.IsNullOrWhiteSpace(book.b_author))
            {
                idlessBook.b_title = book.b_title;
            }
            else { throw new ArgumentNullException("Book author must have a value!"); }
            //Price check
            if (!string.IsNullOrEmpty(book.b_price.ToString()))
            {
                if (book.b_price >= 0) { idlessBook.b_price = book.b_price; }
                else { throw new ArgumentException("The price cannot be less than 0!"); }
            }
            else { throw new ArgumentNullException("Book must have a price!"); }
            //ReleaseDate check
            if (book.b_releaseDate != DateTime.MinValue)
            {
                idlessBook.b_releaseDate = book.b_releaseDate;
            }
            else { throw new ArgumentException("Add a valid release date!"); }
            //PublisherId check
            if (!string.IsNullOrEmpty(book.b_publisher_id.ToString()))
            {
                if (book.b_publisher_id > 0)
                {
                    var allPublishers = publisherRepo.GetAll();
                    var publisher = allPublishers.Where(publisher => publisher.p_id == book.b_publisher_id);
                    if (publisher.Count() == 0)
                    {
                        throw new ArgumentException($"There is no publisher with id: {book.b_publisher_id}");
                    }
                    else { idlessBook.b_publisher_id = book.b_publisher_id; }
                }

            }
            else { throw new ArgumentNullException("Book publisher id must have a value"); }

            //Succesfull addition
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
