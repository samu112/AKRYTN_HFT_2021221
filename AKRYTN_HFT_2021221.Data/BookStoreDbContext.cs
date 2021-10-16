using AKRYTN_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AKRYTN_HFT_2021221_Data
{
    public class BookStoreDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<OrderCart_Connector> OrderCart_Connectors { get; set; }
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
            modelBuilder.Entity<OrderCart_Connector>().HasKey(conn => new { conn.occ_cartItem_id, conn.occ_cart_id });

            modelBuilder.Entity<OrderCart_Connector>(entity =>
            {
                entity.HasOne(conn => conn.Cart).
                WithMany(carts => carts.Conn).
                HasForeignKey(conn => conn.occ_cart_id).
                OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OrderCart_Connector>(entity =>
            {
                entity.HasOne(conn => conn.CartItem).
                WithMany(cartitems => cartitems.Conn).
                HasForeignKey(conn => conn.occ_cartItem_id).
                OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(user => user.Cart).
                WithOne(cart => cart.User).
                HasForeignKey<Cart>(user => user.c_Id).
                OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Book>(entity =>//Correct 100%
            {
                entity.HasOne(book => book.Publisher).
                WithMany(publisher => publisher.Books).
                HasForeignKey(book => book.b_publisher_id).
                OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasOne(cartitem => cartitem.Book).
                WithMany(book => book.CartItem).
                HasForeignKey(cartitem => cartitem.ci_book_id).
                OnDelete(DeleteBehavior.ClientSetNull);
            });

            //Seed

            //Users
            User user1 = new User() { u_Id = 1, u_Name="Philip Jones", u_RegDate = DateTime.Parse("2011.02.11 06:30:33"), u_Address= "6th Apple street US", u_Email= "PhilipJones@gmail.com", u_CartId=1 };
            User user2 = new User() { u_Id = 2, u_Name = "Harry Potter", u_RegDate = DateTime.Parse("2015.06.01 02:30:00"), u_Address = "4th Grape street US", u_Email = "HarryPotter@gmail.com", u_CartId = 2 };

            modelBuilder.Entity<User>().HasData(user1, user2);

            //Cart
            Cart cart1 = new Cart() { c_Id = 1, c_billingAddress = "16th DontCome Street Russia", c_creditcardNumber = "341111111111111", c_deliver = true };
            Cart cart2 = new Cart() { c_Id = 2, c_billingAddress = "14th Come Street Canada", c_creditcardNumber = "341111111511111", c_deliver = false };

            modelBuilder.Entity<Cart>().HasData(cart1, cart2);

            //Publisher

            Publisher publisher1 = new Publisher() { p_id = 1, p_name = "Mozaik", p_address="Szeged", p_email="supőport@mozaik.hu", p_website="mozaik.hu" };

            modelBuilder.Entity<Publisher>().HasData(publisher1);

            //Book

            Book book1 = new Book() { b_id = 1, b_title = "How to become Rich!", b_author = "Rich Richard", b_price = 25000, b_releaseDate = DateTime.Parse("2021.02.00 00:00:00"), b_publisher_id = 1 };
            
            modelBuilder.Entity<Book>().HasData(book1);

            //CartItem
            CartItem cartitem1 = new CartItem() { ci_id = 1, ci_book_id = 1, ci_quantity = 2 };

            modelBuilder.Entity<CartItem>().HasData(cartitem1);

            //Connector
            OrderCart_Connector connector = new OrderCart_Connector() { occ_id = 1, occ_cart_id = 1, occ_cartItem_id = 1 };
            
            modelBuilder.Entity<OrderCart_Connector>().HasData(connector);
        }
    }
}
