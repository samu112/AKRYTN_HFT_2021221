using AKRYTN_HFT_2021221.Models;
using AKRYTN_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    class CartLogic : ICartLogic
    {
        private readonly ICartRepository repo;

        public CartLogic()
        {
            this.repo = new CartRepository(new Data.BookStoreDbContext());
        }
        //Constructor overload for testing.
        public CartLogic(ICartRepository repo)
        {
            this.repo = repo;
        }

        //NON-CRUD METHODS:

        public IEnumerable<string> GetCartItems(int id)
        {
            throw new NotImplementedException();
        }
        
        //CRUD METHODS:

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public Cart GetCart(int id)
        {
            return this.repo.GetOneById(id);
        }

        public IEnumerable<Cart> GetCarts()
        {
            return this.repo.GetAll().ToList();
        }

        public void InsertCart(Cart cart)
        {
            this.repo.Insert(cart);
        }

        public void UpdateCartBillingAddress(int id, string newAddress)
        {
            this.repo.UpdateBillingAddress(id, newAddress);
        }

        public void UpdateCartcreditcardNumber(int id, string newCardNumber)
        {
            this.repo.UpdateCreditCard(id, newCardNumber);
        }

        public void UpdateCartDeliver(int id, bool newStatus)
        {
            this.repo.UpdateDeliver(id, newStatus);
        }

        public void UpdateCartStatus(int id, bool newStatus)
        {
            this.repo.UpdateStatus(id, newStatus);
        }

        public void UpdateCartsUser(int id, int newId)
        {
            this.repo.UpdateUserId(id, newId);
        }
    }
}
