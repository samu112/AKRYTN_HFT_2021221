using AKRYTN_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AKRYTN_HFT_2021221.Data
{
    public class BookStoreDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        public BookStoreDbContext()
        {
            this.Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\BookStoreDb.mdf;integrated security=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasOne(cart => cart.User).
                WithOne(user => user.Cart).
                HasForeignKey<Cart>(cart => cart.c_user_id).
                OnDelete(DeleteBehavior.ClientSetNull);

            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasOne(cartItem => cartItem.Cart).
                WithMany(cart => cart.CartItem).
                HasForeignKey(cartItem => cartItem.ci_cart_id).
                OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasOne(cartItem => cartItem.Book).
                WithMany(book => book.CartItem).
                HasForeignKey(cartItem => cartItem.ci_book_id).
                OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(book => book.Publisher).
                WithMany(publisher => publisher.Books).
                HasForeignKey(book => book.b_publisher_id).
                OnDelete(DeleteBehavior.ClientSetNull);
            });



            //Seed

            //Users
            List<User> UserSeed = new List<User>() {
                new User() { u_id = 1, u_name = "Ingrid Parks", u_regDate = DateTime.Parse("2015. 12. 27. 16:04:11"), u_address = "72 Main St, North Reading MA 1864", u_email = "Ingrid.Parks@outlook.com" },
                new User() { u_id = 2, u_name = "Isabell Fisher", u_regDate = DateTime.Parse("2014. 01. 30. 14:51:29"), u_address = "30 Catskill, Catskill NY 12414", u_email = "Isabell.Fisher@aol.com" },
                new User() { u_id = 3, u_name = "Ciara Wu", u_regDate = DateTime.Parse("2014. 02. 03. 17:06:30"), u_address = "2055 Niagara Falls Blvd, Amherst NY 14228", u_email = "Ciara.Wu@aol.com" },
                new User() { u_id = 4, u_name = "Giovanna Gutierrez", u_regDate = DateTime.Parse("2017. 05. 15. 21:46:22"), u_address = "139 Merchant Place, Cobleskill NY 12043", u_email = "Giovanna.Gutierrez@aol.com" },
                new User() { u_id = 5, u_name = "Alma Russo", u_regDate = DateTime.Parse("2014. 04. 14. 7:58:02"), u_address = "311 RT 9W, Glenmont NY 12077", u_email = "Alma.Russo@aol.com" },
                new User() { u_id = 6, u_name = "Rylee Drake", u_regDate = DateTime.Parse("2014. 04. 08. 21:09:25"), u_address = "352 Palmer Road, Ware MA 1082", u_email = "Rylee.Drake@aol.com" },
                new User() { u_id = 7, u_name = "Mayra Stone", u_regDate = DateTime.Parse("2015. 11. 17. 21:43:38"), u_address = "4765 Commercial Drive, New Hartford NY 13413", u_email = "Mayra.Stone@protonmail.com" },
                new User() { u_id = 8, u_name = "Erik Richards", u_regDate = DateTime.Parse("2011. 07. 13. 20:56:43"), u_address = "300 Colony Place, Plymouth MA 2360", u_email = "Erik.Richards@aol.com" },
                new User() { u_id = 9, u_name = "Dominique Hoover", u_regDate = DateTime.Parse("2019. 03. 18. 7:48:16"), u_address = "425 Route 31, Macedon NY 14502", u_email = "Dominique.Hoover@aol.com" },
                new User() { u_id = 10, u_name = "Lesly Hurley", u_regDate = DateTime.Parse("2020. 07. 27. 10:34:21"), u_address = "555 Hubbard Ave-Suite 12, Pittsfield MA 1201", u_email = "Lesly.Hurley@mail.com" },
                new User() { u_id = 11, u_name = "Annika Dominguez", u_regDate = DateTime.Parse("2021. 05. 24. 1:26:29"), u_address = "2 Gannett Dr, Johnson City NY 13790", u_email = "Annika.Dominguez@aol.com" },
                new User() { u_id = 12, u_name = "Bria Marsh", u_regDate = DateTime.Parse("2009. 11. 04. 17:29:55"), u_address = "30 Memorial Drive, Avon MA 2322", u_email = "Bria.Marsh@outlook.com" },
                new User() { u_id = 13, u_name = "Lola Velasquez", u_regDate = DateTime.Parse("2009. 04. 25. 7:07:58"), u_address = "4133 Veterans Memorial Drive, Batavia NY 14020", u_email = "Lola.Velasquez@outlook.com" },
                new User() { u_id = 14, u_name = "Royce Mayo", u_regDate = DateTime.Parse("2013. 06. 14. 3:17:36"), u_address = "85 Crooked Hill Road, Commack NY 11725", u_email = "Royce.Mayo@aol.com" },
                new User() { u_id = 15, u_name = "Marilyn Zavala", u_regDate = DateTime.Parse("2012. 07. 06. 1:16:35"), u_address = "777 Brockton Avenue, Abington MA 2351", u_email = "Marilyn.Zavala@icloud.com" },
                new User() { u_id = 16, u_name = "Jaxon Arnold", u_regDate = DateTime.Parse("2020. 04. 08. 6:44:03"), u_address = "7155 State Rt 12 S, Lowville NY 13367", u_email = "Jaxon.Arnold@protonmail.com" },
                new User() { u_id = 17, u_name = "Quinton Lloyd", u_regDate = DateTime.Parse("2009. 12. 12. 18:25:22"), u_address = "506 State Road, North Dartmouth MA 2747", u_email = "Quinton.Lloyd@mail.ru" },
                new User() { u_id = 18, u_name = "Dayton Hurley", u_regDate = DateTime.Parse("2010. 09. 25. 10:22:31"), u_address = "579 Troy-Schenectady Road, Latham NY 12110", u_email = "Dayton.Hurley@mail.com" },
                new User() { u_id = 19, u_name = "Kasen Williams", u_regDate = DateTime.Parse("2020. 11. 29. 3:52:35"), u_address = "872 Route 13, Cortlandville NY 13045", u_email = "Kasen.Williams@outlook.com" },
                new User() { u_id = 20, u_name = "Dylan Gallegos", u_regDate = DateTime.Parse("2017. 07. 19. 5:20:06"), u_address = "1400 County Rd 64, Horseheads NY 14845", u_email = "Dylan.Gallegos@gmail.com" }
            };
            modelBuilder.Entity<User>().HasData(UserSeed);

            //Cart
            List<Cart> CartSeed = new List<Cart>() {
                new Cart() { c_id = 1, c_billingAddress = "5710 Mcfarland Blvd, Northport AL 35476", c_creditcardNumber = "5315932977302275", c_deliver = false, c_user_id = 1 },
                new Cart() { c_id = 2, c_billingAddress = "5100 Hwy 31, Calera AL 35040", c_creditcardNumber = "347119405332651", c_deliver = false, c_user_id = 2 },
                new Cart() { c_id = 3, c_billingAddress = "1706 Military Street South, Hamilton AL 35570", c_creditcardNumber = "5264832713838544", c_deliver = true, c_user_id = 3 },
                new Cart() { c_id = 4, c_billingAddress = "1600 Montclair Rd, Birmingham AL 35210", c_creditcardNumber = "2384122223196639", c_deliver = true, c_user_id = 4 },
                new Cart() { c_id = 5, c_billingAddress = "7100 Aaron Aronov Drive, Fairfield AL 35064", c_creditcardNumber = "4485117893742", c_deliver = true, c_user_id = 5 },
                new Cart() { c_id = 6, c_billingAddress = "969 Us Hwy 80 West, Demopolis AL 36732", c_creditcardNumber = "5187894751668278", c_deliver = false, c_user_id = 6 },
                new Cart() { c_id = 7, c_billingAddress = "4765 Commercial Drive, New Hartford NY 13413", c_creditcardNumber = "5179953965781879", c_deliver = false, c_user_id = 7 },
                new Cart() { c_id = 8, c_billingAddress = "220 Salem Turnpike, Norwich CT 6360", c_creditcardNumber = "4539507316629647", c_deliver = false, c_user_id = 8 },
                new Cart() { c_id = 9, c_billingAddress = "425 Route 31, Macedon NY 14502", c_creditcardNumber = "4716095270205054", c_deliver = true, c_user_id = 9 },
                new Cart() { c_id = 10, c_billingAddress = "6350 Cottage Hill Road, Mobile AL 36609", c_creditcardNumber = "6011213738128829", c_deliver = false, c_user_id = 10 },
                new Cart() { c_id = 11, c_billingAddress = "2 Gannett Dr, Johnson City NY 13790", c_creditcardNumber = "4349863320674", c_deliver = false, c_user_id = 11 },
                new Cart() { c_id = 12, c_billingAddress = "30 Memorial Drive, Avon MA 2322", c_creditcardNumber = "5100834225986660", c_deliver = false, c_user_id = 12 },
                new Cart() { c_id = 13, c_billingAddress = "4133 Veterans Memorial Drive, Batavia NY 14020", c_creditcardNumber = "349976302186607", c_deliver = false, c_user_id = 13 },
                new Cart() { c_id = 14, c_billingAddress = "85 Crooked Hill Road, Commack NY 11725", c_creditcardNumber = "4920330053168407", c_deliver = true, c_user_id = 14 },
                new Cart() { c_id = 15, c_billingAddress = "1970 S University Blvd, Mobile AL 36609", c_creditcardNumber = "4929099767259", c_deliver = true, c_user_id = 15 },
                new Cart() { c_id = 16, c_billingAddress = "7155 State Rt 12 S, Lowville NY 13367", c_creditcardNumber = "4485844413166023", c_deliver = true, c_user_id = 16 },
                new Cart() { c_id = 17, c_billingAddress = "506 State Road, North Dartmouth MA 2747", c_creditcardNumber = "4916392354322808", c_deliver = false, c_user_id = 17 },
                new Cart() { c_id = 18, c_billingAddress = "579 Troy-Schenectady Road, Latham NY 12110", c_creditcardNumber = "4929136861821249", c_deliver = true, c_user_id = 18 },
                new Cart() { c_id = 19, c_billingAddress = "164 Danbury Rd, New Milford CT 6776", c_creditcardNumber = "5431795895294471", c_deliver = false, c_user_id = 19 },
                new Cart() { c_id = 20, c_billingAddress = "1400 County Rd 64, Horseheads NY 14845", c_creditcardNumber = "4532131886847518", c_deliver = true, c_user_id = 20 }
            };

            modelBuilder.Entity<Cart>().HasData(CartSeed);


            //CartItem
            List<CartItem> CartItemSeed = new List<CartItem>() {
                new CartItem() { ci_id = 1, ci_book_id = 5, ci_quantity = 4, ci_cart_id = 6 },
                new CartItem() { ci_id = 2, ci_book_id = 3, ci_quantity = 3, ci_cart_id = 1 },
                new CartItem() { ci_id = 3, ci_book_id = 1, ci_quantity = 2, ci_cart_id = 6 },
                new CartItem() { ci_id = 4, ci_book_id = 1, ci_quantity = 4, ci_cart_id = 4 },
                new CartItem() { ci_id = 5, ci_book_id = 1, ci_quantity = 1, ci_cart_id = 1 },
                new CartItem() { ci_id = 6, ci_book_id = 2, ci_quantity = 3, ci_cart_id = 4 },
                new CartItem() { ci_id = 7, ci_book_id = 2, ci_quantity = 4, ci_cart_id = 3 },
                new CartItem() { ci_id = 8, ci_book_id = 2, ci_quantity = 4, ci_cart_id = 4 },
                new CartItem() { ci_id = 9, ci_book_id = 3, ci_quantity = 3, ci_cart_id = 1 },
                new CartItem() { ci_id = 10, ci_book_id = 3, ci_quantity = 3, ci_cart_id = 2 },
                new CartItem() { ci_id = 11, ci_book_id = 4, ci_quantity = 4, ci_cart_id = 2 },
                new CartItem() { ci_id = 12, ci_book_id = 4, ci_quantity = 1, ci_cart_id = 5 },
                new CartItem() { ci_id = 13, ci_book_id = 5, ci_quantity = 4, ci_cart_id = 2 },
                new CartItem() { ci_id = 14, ci_book_id = 9, ci_quantity = 4, ci_cart_id = 5 },
                new CartItem() { ci_id = 15, ci_book_id = 6, ci_quantity = 3, ci_cart_id = 6 },
                new CartItem() { ci_id = 16, ci_book_id = 4, ci_quantity = 4, ci_cart_id = 8 },
                new CartItem() { ci_id = 17, ci_book_id = 6, ci_quantity = 3, ci_cart_id = 7 },
                new CartItem() { ci_id = 18, ci_book_id = 8, ci_quantity = 3, ci_cart_id = 5 },
                new CartItem() { ci_id = 19, ci_book_id = 6, ci_quantity = 3, ci_cart_id = 7 },
                new CartItem() { ci_id = 20, ci_book_id = 8, ci_quantity = 4, ci_cart_id = 16 },
                new CartItem() { ci_id = 21, ci_book_id = 5, ci_quantity = 4, ci_cart_id = 9 },
                new CartItem() { ci_id = 22, ci_book_id = 11, ci_quantity = 2, ci_cart_id = 10 },
                new CartItem() { ci_id = 23, ci_book_id = 7, ci_quantity = 2, ci_cart_id = 3 },
                new CartItem() { ci_id = 24, ci_book_id = 7, ci_quantity = 4, ci_cart_id = 3 },
                new CartItem() { ci_id = 25, ci_book_id = 7, ci_quantity = 4, ci_cart_id = 9 },
                new CartItem() { ci_id = 26, ci_book_id = 10, ci_quantity = 1, ci_cart_id = 8 },
                new CartItem() { ci_id = 27, ci_book_id = 8, ci_quantity = 1, ci_cart_id = 7 },
                new CartItem() { ci_id = 28, ci_book_id = 12, ci_quantity = 2, ci_cart_id = 8 },
                new CartItem() { ci_id = 29, ci_book_id = 11, ci_quantity = 2, ci_cart_id = 11 }
            };

            modelBuilder.Entity<CartItem>().HasData(CartItemSeed);

            //Publisher
            List<Publisher> PublisherSeed = new List<Publisher>() {
                new Publisher() { p_id = 1, p_name = "Learn Literary Publisher", p_address = "East Elmhurst NY 11369, 7610 Victoria Street", p_email = "Learn.Literary.Publisher@LearnLiteraryPublisher.com", p_website = "LearnLiteraryPublisher.com" },
                new Publisher() { p_id = 2, p_name = "Book Bindings Publisher", p_address = "859 St Church Street, Ocean Springs MS 39564", p_email = "Book.Bindings.Publisher@BookBindingsPublisher.com", p_website = "BookBindingsPublisher.com" },
                new Publisher() { p_id = 3, p_name = "Library Liftoff Publisher", p_address = "Jackson NJ 08527, 987 Sunnyslope Lane", p_email = "Library.Liftoff.Publisher@LibraryLiftoffPublisher.com", p_website = "LibraryLiftoffPublisher.com" },
                new Publisher() { p_id = 4, p_name = "Bookbindingvio Publisher", p_address = "Kernersville NC 27284, 59 Smith Avenue", p_email = "Bookbindingvio.Publisher@BookbindingvioPublisher.com", p_website = "BookbindingvioPublisher.com" },
                new Publisher() { p_id = 5, p_name = "Table of Content Publisher", p_address = "743 N Walnutwood Street, Newburgh NY 12550", p_email = "Table.of.Content.Publisher@TableofContentPublisher.com", p_website = "TableofContentPublisher.com" },
                new Publisher() { p_id = 6, p_name = "Line Literary Publisher", p_address = "990 Fairway Street, Opa Locka FL 33054", p_email = "Line.Literary.Publisher@LineLiteraryPublisher.com", p_website = "LineLiteraryPublisher.com" },
                new Publisher() { p_id = 7, p_name = "Beach Bound Books Publisher", p_address = "773 Jefferson Street, Bethel Park PA 15102", p_email = "Beach.Bound.Books.Publisher@BeachBoundBooksPublisher.com", p_website = "BeachBoundBooksPublisher.com" },
                new Publisher() { p_id = 8, p_name = "Freshly Bound Publisher", p_address = "221 North Cactus Street, Merrimack NH 03054", p_email = "Freshly.Bound.Publisher@FreshlyBoundPublisher.com", p_website = "FreshlyBoundPublisher.com" },
                new Publisher() { p_id = 9, p_name = "Energize Shelf Publisher", p_address = "Morristown NJ 07960, 9210 Prospect Street", p_email = "Energize.Shelf.Publisher@EnergizeShelfPublisher.com", p_website = "EnergizeShelfPublisher.com" },
                new Publisher() { p_id = 10, p_name = "Page Turners Publisher", p_address = "Riverview FL 33569, 77 Newbridge Lane", p_email = "Page.Turners.Publisher@PageTurnersPublisher.com", p_website = "PageTurnersPublisher.com" },
                new Publisher() { p_id = 11, p_name = "Bookmarked Publisher", p_address = "Winter Haven FL 33880, 9672 East Prince Circle", p_email = "Bookmarked.Publisher@BookmarkedPublisher.com", p_website = "BookmarkedPublisher.com" },
                new Publisher() { p_id = 12, p_name = "Fresh Pages Publisher", p_address = "West Bend WI 53095, 53 Ridge Street", p_email = "Fresh.Pages.Publisher@FreshPagesPublisher.com", p_website = "FreshPagesPublisher.com" }
            };

            modelBuilder.Entity<Publisher>().HasData(PublisherSeed);

            //Book
            List<Book> BookSeed = new List<Book>() {
                new Book() { b_id = 1, b_title = "Butterfly of the stars", b_author = "Brook Hart", b_price = 3400, b_releaseDate = DateTime.Parse("2011. 01. 06. 0:00:00"), b_publisher_id = 1 },
                new Book() { b_id = 2, b_title = "Children of the sky", b_author = "Rory Werewolf", b_price = 1900, b_releaseDate = DateTime.Parse("1988. 08. 09. 0:00:00"), b_publisher_id = 2 },
                new Book() { b_id = 3, b_title = "Girl of our future", b_author = "Brook Flame", b_price = 7400, b_releaseDate = DateTime.Parse("1978. 05. 20. 0:00:00"), b_publisher_id = 3 },
                new Book() { b_id = 4, b_title = "Cautious of the east", b_author = "Ziggy Toxic", b_price = 2500, b_releaseDate = DateTime.Parse("1991. 10. 09. 0:00:00"), b_publisher_id = 4 },
                new Book() { b_id = 5, b_title = "Men with hazel eyes", b_author = "Arden Zenith", b_price = 5800, b_releaseDate = DateTime.Parse("1996. 09. 08. 0:00:00"), b_publisher_id = 5 },
                new Book() { b_id = 6, b_title = "Descendants of the prison", b_author = "River Mangabeast", b_price = 6100, b_releaseDate = DateTime.Parse("1988. 07. 17. 0:00:00"), b_publisher_id = 6 },
                new Book() { b_id = 7, b_title = "Commanding the graves", b_author = "Sidney Styx", b_price = 4500, b_releaseDate = DateTime.Parse("2016. 12. 03. 0:00:00"), b_publisher_id = 7 },
                new Book() { b_id = 8, b_title = "Carvings of the past", b_author = "Cody Traveller", b_price = 7800, b_releaseDate = DateTime.Parse("2018. 09. 27. 0:00:00"), b_publisher_id = 8 },
                new Book() { b_id = 9, b_title = "Changed by the end of the sun", b_author = "Aubrey Flame", b_price = 3200, b_releaseDate = DateTime.Parse("1989. 08. 04. 0:00:00"), b_publisher_id = 9 },
                new Book() { b_id = 10, b_title = "Athletes and herbs", b_author = "Daylen Phoenix", b_price = 2400, b_releaseDate = DateTime.Parse("1988. 03. 06. 0:00:00"), b_publisher_id = 10 },
                new Book() { b_id = 11, b_title = "Miracles in your garden", b_author = "Kris Scars", b_price = 200, b_releaseDate = DateTime.Parse("2015. 01. 27. 0:00:00"), b_publisher_id = 11 },
                new Book() { b_id = 12, b_title = "Simplicity of traditions", b_author = "Eli Mauler", b_price = 2300, b_releaseDate = DateTime.Parse("2011. 12. 17. 0:00:00"), b_publisher_id = 12 },
                new Book() { b_id = 13, b_title = "Assassins and veterans", b_author = "Quinn Engine", b_price = 1500, b_releaseDate = DateTime.Parse("2016. 06. 18. 0:00:00"), b_publisher_id = 4 },
                new Book() { b_id = 14, b_title = "Age in my nightmares", b_author = "Indiana Jelliclecat", b_price = 4900, b_releaseDate = DateTime.Parse("1996. 06. 05. 0:00:00"), b_publisher_id = 12 },
                new Book() { b_id = 15, b_title = "Boys of the galaxy", b_author = "Keenan Fae", b_price = 4200, b_releaseDate = DateTime.Parse("1979. 03. 08. 0:00:00"), b_publisher_id = 9 },
                new Book() { b_id = 16, b_title = "Ladylove of my mind", b_author = "Xander Marquis", b_price = 3700, b_releaseDate = DateTime.Parse("1976. 05. 12. 0:00:00"), b_publisher_id = 11 },
                new Book() { b_id = 17, b_title = "Enemy of the gods", b_author = "Lindy Parker", b_price = 200, b_releaseDate = DateTime.Parse("1988. 01. 01. 0:00:00"), b_publisher_id = 8},
                new Book() { b_id = 18, b_title = "Planes of the harvest", b_author = "Cain Sweetling", b_price = 2400, b_releaseDate = DateTime.Parse("1993. 05. 12. 0:00:00"), b_publisher_id = 2 },
                new Book() { b_id = 19, b_title = "Snakes of the west", b_author = "Keenan Fae", b_price = 7800, b_releaseDate = DateTime.Parse("1980. 01. 01. 0:00:00"), b_publisher_id = 5 },
                new Book() { b_id = 20, b_title = "Man and pig", b_author = "Mackenzie Night", b_price = 4700, b_releaseDate = DateTime.Parse("1995. 02. 27. 0:00:00"), b_publisher_id = 3 },
                new Book() { b_id = 21, b_title = "Simplicity of traditions", b_author = "Sam Punk", b_price = 3900, b_releaseDate = DateTime.Parse("1978. 04. 30. 0:00:00"), b_publisher_id = 7 },
                new Book() { b_id = 22, b_title = "Fate of the east", b_author = "Kris Raging", b_price = 8900, b_releaseDate = DateTime.Parse("2011. 10. 30. 0:00:00"), b_publisher_id = 10 },
                new Book() { b_id = 23, b_title = "Fungi of stone", b_author = "Damien Taylor", b_price = 1900, b_releaseDate = DateTime.Parse("2009. 04. 29. 0:00:00"), b_publisher_id = 8 },
                new Book() { b_id = 24, b_title = "Lions and snakes", b_author = "Kris Wisp", b_price = 8900, b_releaseDate = DateTime.Parse("2001. 09. 07. 0:00:00"), b_publisher_id = 12 },
                new Book() { b_id = 25, b_title = "Duck of fantasia", b_author = "Storm Velociraptor", b_price = 6800, b_releaseDate = DateTime.Parse("2005. 01. 18. 0:00:00"), b_publisher_id = 10 },
                new Book() { b_id = 26, b_title = "Pirate of history", b_author = "Reagan Poison", b_price = 6600, b_releaseDate = DateTime.Parse("2009. 07. 07. 0:00:00"), b_publisher_id = 6 },
                new Book() { b_id = 27, b_title = "Greed of the intruders", b_author = "Sidney Styx", b_price = 4200, b_releaseDate = DateTime.Parse("2020. 02. 17. 0:00:00"), b_publisher_id = 3 },
                new Book() { b_id = 28, b_title = "Defenders and trolls", b_author = "Griffin Dylan", b_price = 1800, b_releaseDate = DateTime.Parse("2000. 02. 23. 0:00:00"), b_publisher_id = 6 },
                new Book() { b_id = 29, b_title = "Loved by friendship", b_author = "Ximena Roseblood", b_price = 4900, b_releaseDate = DateTime.Parse("1993. 04. 14. 0:00:00"), b_publisher_id = 12 },
                new Book() { b_id = 30, b_title = "Wealth of the forest", b_author = "Primrose Darkstar", b_price = 5000, b_releaseDate = DateTime.Parse("2019. 02. 24. 0:00:00"), b_publisher_id = 8 },
                new Book() { b_id = 31, b_title = "Bags delusion", b_author = "Teagan Styx", b_price = 6900, b_releaseDate = DateTime.Parse("2015. 07. 18. 0:00:00"), b_publisher_id = 6 },
                new Book() { b_id = 32, b_title = "Slaves and knights", b_author = "Cody Traveller", b_price = 1200, b_releaseDate = DateTime.Parse("1998. 05. 14. 0:00:00"), b_publisher_id = 8 }
            };

            modelBuilder.Entity<Book>().HasData(BookSeed);
        }
    }
}
