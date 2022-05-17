using AKRYTN_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfClient.Clients;

namespace WpfClient.ViewModels
{
    class CartItemsWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<CartItem> CartItems { get; set; }

        public ObservableCollection<int> AllBookIds { get; set; }
        public ObservableCollection<int> AllCartIds { get; set; }

        private CartItem _selectedCartItem;

        public CartItem SelectedCartItem
        {
            get => _selectedCartItem;
            set
            {
                _selectedCartItem = new CartItem();
                CopyCartItem(value, _selectedCartItem);

                OnPropertyChanged();
            }
        }

        private int _selectedCartItemIndex;

        public int SelectedCartItemIndex
        {
            get => _selectedCartItemIndex;
            set
            {
                SetProperty(ref _selectedCartItemIndex, value);
            }
        }

        public RelayCommand AddCartItemCommand { get; set; }
        public RelayCommand EditCartItemCommand { get; set; }
        public RelayCommand DeleteCartItemCommand { get; set; }

        public CartItemsWindowViewModel()
        {
            CartItems = new ObservableCollection<CartItem>();
            //Get CartItems
            _apiClient
                .GetAsync<List<CartItem>>("http://localhost:8921/cartItem")
                .ContinueWith((cartItems) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        cartItems.Result.ForEach((cartItem) =>
                        {
                            CartItems.Add(cartItem);
                        });
                    });
                });
            //Get Books
            AllBookIds = new ObservableCollection<int>();
            _apiClient
                .GetAsync<List<Book>>("http://localhost:8921/book")
                .ContinueWith((books) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        books.Result.ForEach((book) =>
                        {
                            AllBookIds.Add(book.b_id);
                        });
                    });
                });
            //Get Carts
            AllCartIds = new ObservableCollection<int>();
            _apiClient
                .GetAsync<List<Cart>>("http://localhost:8921/cart")
                .ContinueWith((carts) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        carts.Result.ForEach((cart) =>
                        {
                            AllCartIds.Add(cart.c_id);
                        });
                    });
                });
            AddCartItemCommand = new RelayCommand(AddCartItem);
            EditCartItemCommand = new RelayCommand(EditCartItem);
            DeleteCartItemCommand = new RelayCommand(DeleteCartItem);
        }

        private void CopyCartItem(CartItem cartItemCopiedFrom, CartItem cartItemCopiedFor)
        {
            if (cartItemCopiedFrom != null && cartItemCopiedFor != null)
            {
                cartItemCopiedFor.ci_id = cartItemCopiedFrom.ci_id;
                cartItemCopiedFor.ci_quantity = cartItemCopiedFrom.ci_quantity;
                cartItemCopiedFor.ci_book_id = cartItemCopiedFrom.ci_book_id;
                cartItemCopiedFor.ci_cart_id = cartItemCopiedFrom.ci_cart_id;
                cartItemCopiedFor.Book = cartItemCopiedFrom.Book;
                cartItemCopiedFor.Cart = cartItemCopiedFrom.Cart;
            }
        }

        private void AddCartItem()
        {
            if (SelectedCartItem == null)
            {
                SelectedCartItem = new CartItem();
            }
            else
            {
                CartItem newCartItem = new CartItem()
                {
                    ci_quantity = SelectedCartItem.ci_quantity,
                    ci_book_id = SelectedCartItem.ci_book_id,
                    ci_cart_id = SelectedCartItem.ci_cart_id,
                };

                _apiClient
                    .PostAsync(newCartItem, "http://localhost:8921/cartItem")
                    .ContinueWith((task) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if
                            (
                                //Check if something is null
                                String.IsNullOrEmpty(SelectedCartItem.ci_id.ToString()) && String.IsNullOrWhiteSpace(SelectedCartItem.ci_id.ToString()) ||
                                String.IsNullOrEmpty(SelectedCartItem.ci_book_id.ToString()) && String.IsNullOrWhiteSpace(SelectedCartItem.ci_book_id.ToString()) ||
                                String.IsNullOrEmpty(SelectedCartItem.ci_cart_id.ToString()) && String.IsNullOrWhiteSpace(SelectedCartItem.ci_cart_id.ToString()) ||
                                String.IsNullOrEmpty(SelectedCartItem.ci_quantity.ToString()) && String.IsNullOrWhiteSpace(SelectedCartItem.ci_quantity.ToString())
                            )
                            {
                                MessageBox.Show("You have to fill everything!");
                            }
                            else
                            {
                                //Refresh the list with new data
                                ListBoxRefresh();
                            }
                        });
                    });
            }
        }

        private void EditCartItem()
        {
            _apiClient
               .PutAsync(SelectedCartItem, "http://localhost:8921/cartItem")
               .ContinueWith((task) =>
               {
                   Application.Current.Dispatcher.Invoke(() =>
                   {
                       if
                       (
                           //Check if something is null
                           String.IsNullOrEmpty(SelectedCartItem.ci_id.ToString()) && String.IsNullOrWhiteSpace(SelectedCartItem.ci_id.ToString()) ||
                           String.IsNullOrEmpty(SelectedCartItem.ci_book_id.ToString()) && String.IsNullOrWhiteSpace(SelectedCartItem.ci_book_id.ToString()) ||
                           String.IsNullOrEmpty(SelectedCartItem.ci_cart_id.ToString()) && String.IsNullOrWhiteSpace(SelectedCartItem.ci_cart_id.ToString()) ||
                           String.IsNullOrEmpty(SelectedCartItem.ci_quantity.ToString()) && String.IsNullOrWhiteSpace(SelectedCartItem.ci_quantity.ToString())
                       )
                       {
                           MessageBox.Show("You have to fill everything!");
                            //Refresh the list with new data
                            ListBoxRefresh();
                       }
                       else
                       {
                           CartItems[SelectedCartItemIndex].ci_id = SelectedCartItem.ci_id;
                           CartItems[SelectedCartItemIndex].ci_book_id = SelectedCartItem.ci_book_id;
                           CartItems[SelectedCartItemIndex].ci_cart_id = SelectedCartItem.ci_cart_id;
                           CartItems[SelectedCartItemIndex].ci_quantity = SelectedCartItem.ci_quantity;
                           ListBoxRefresh();
                       }
                   });
               });
        }

        private void DeleteCartItem()
        {
            try
            {
                _apiClient
                    .DeleteAsync(SelectedCartItem.ci_id, "http://localhost:8921/cartItem")
                    .ContinueWith((task) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            ListBoxRefresh();
                        });
                    });

            }
            catch (NullReferenceException)
            {
                //Happens if user spamming the delete button or no enitity is selected
                MessageBox.Show("There is nothing to delete :(");
            }

        }




        //Refresh the ListBox
        private void ListBoxRefresh()
        {
            _apiClient
                .GetAsync<List<CartItem>>("http://localhost:8921/cartItem")
                .ContinueWith((cartItems) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CartItems.Clear();
                        cartItems.Result.ForEach((cartItem) =>
                        {
                            CartItems.Add(cartItem);
                        });
                    });
                });
        }
    }
}
