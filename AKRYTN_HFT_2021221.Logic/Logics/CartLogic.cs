using AKRYTN_HFT_2021221.Models;
using AKRYTN_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    public class CartLogic : ICartLogic
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

        public IEnumerable<string> GetItemsInThisCart(int id)
        {
            throw new NotImplementedException();
        }
        
        //CRUD METHODS:

        public bool DeleteCart(int id)
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

        public void AddNewCart(Cart cart)
        {
            this.repo.Insert(cart);
        }

        public void ChangeCartBillingAddress(int id, string newAddress)
        {
            this.repo.UpdateBillingAddress(id, newAddress);
        }

        public void ChangeCartcreditcardNumber(int id, string newCardNumber)
        {
            this.repo.UpdateCreditCard(id, newCardNumber);
        }

        public void ChangeCartDeliverStatus(int id, bool newStatus)
        {
            this.repo.UpdateDeliver(id, newStatus);
        }

        public void ChangeCartStatus(int id, bool newStatus)
        {
            this.repo.UpdateStatus(id, newStatus);
        }

        public void ChangeCartsUser(int id, int newId)
        {
            this.repo.UpdateUserId(id, newId);
        }
    }
}
