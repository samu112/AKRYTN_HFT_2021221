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
    class CartsWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Cart> Carts { get; set; }

        public ObservableCollection<int> AllUserIds { get; set; }

        private Cart _selectedCart;

        public Cart SelectedCart
        {
            get => _selectedCart;
            set
            {
                _selectedCart = new Cart();
                CopyCart(value, _selectedCart);

                OnPropertyChanged();
            }
        }

        private int _selectedCartIndex;

        public int SelectedCartIndex
        {
            get => _selectedCartIndex;
            set
            {
                SetProperty(ref _selectedCartIndex, value);
            }
        }

        public RelayCommand AddCartCommand { get; set; }
        public RelayCommand EditCartCommand { get; set; }
        public RelayCommand DeleteCartCommand { get; set; }
        public RelayCommand GetCartPriceCommand { get; set; }

        public CartsWindowViewModel()
        {
            Carts = new ObservableCollection<Cart>();
            //Get Carts
            _apiClient
                .GetAsync<List<Cart>>("http://localhost:8921/cart")
                .ContinueWith((carts) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        carts.Result.ForEach((cart) =>
                        {
                            Carts.Add(cart);
                        });
                    });
                });
            //Get users
            AllUserIds = new ObservableCollection<int>();
            _apiClient
                .GetAsync<List<User>>("http://localhost:8921/user")
                .ContinueWith((users) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        users.Result.ForEach((user) =>
                        {
                            AllUserIds.Add(user.u_id);
                        });
                    });
                });
            AddCartCommand = new RelayCommand(AddCart);
            EditCartCommand = new RelayCommand(EditCart);
            DeleteCartCommand = new RelayCommand(DeleteCart);
            GetCartPriceCommand = new RelayCommand(GetCartPrice);
        }

        private void GetCartPrice()
        {
            var price = _apiClient.GetCartPrice("http://localhost:8921/cart", SelectedCart.c_id);
            MessageBox.Show("The full price is: " + price);
        }

        private void CopyCart(Cart cartCopiedFrom, Cart cartCopiedFor)
        {
            if (cartCopiedFrom != null && cartCopiedFor != null)
            {
                cartCopiedFor.c_id = cartCopiedFrom.c_id;
                cartCopiedFor.c_creditcardNumber = cartCopiedFrom.c_creditcardNumber;
                cartCopiedFor.c_billingAddress = cartCopiedFrom.c_billingAddress;
                cartCopiedFor.c_deliver = cartCopiedFrom.c_deliver;
                cartCopiedFor.c_user_id = cartCopiedFrom.c_user_id;
                cartCopiedFor.CartItem = cartCopiedFrom.CartItem;
            }
        }

        private void AddCart()
        {
            if (SelectedCart == null)
            {
                SelectedCart = new Cart();
            }
            if (AllUserIds.Any(x => x == SelectedCart.c_user_id) == true)
            {
                MessageBox.Show("1 user can only have 1 cart!");   
            }
            else
            {
                Cart newCart = new Cart()
                {
                    c_creditcardNumber = SelectedCart.c_creditcardNumber,
                    c_billingAddress = SelectedCart.c_billingAddress,
                    c_deliver = SelectedCart.c_deliver,
                    c_user_id = SelectedCart.c_user_id,
                };

                _apiClient
                    .PostAsync(newCart, "http://localhost:8921/cart")
                    .ContinueWith((task) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if
                            (
                                //Check if something is null
                                String.IsNullOrEmpty(SelectedCart.c_id.ToString()) && String.IsNullOrWhiteSpace(SelectedCart.c_id.ToString()) ||
                                String.IsNullOrEmpty(SelectedCart.c_creditcardNumber) && String.IsNullOrWhiteSpace(SelectedCart.c_creditcardNumber) ||
                                String.IsNullOrEmpty(SelectedCart.c_deliver.ToString()) && String.IsNullOrWhiteSpace(SelectedCart.c_deliver.ToString()) ||
                                String.IsNullOrEmpty(SelectedCart.c_billingAddress) && String.IsNullOrWhiteSpace(SelectedCart.c_billingAddress) ||
                                String.IsNullOrEmpty(SelectedCart.c_user_id.ToString()) && String.IsNullOrWhiteSpace(SelectedCart.c_user_id.ToString())
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

        private void EditCart()
        {
            List<int> UserIdListWithoutCurrentId = new List<int>();
            foreach (var id in AllUserIds)
            {
                if (id!= SelectedCart.c_user_id)
                {
                    UserIdListWithoutCurrentId.Add(id);
                }
            }
            if (UserIdListWithoutCurrentId.Any(x => x == Carts[SelectedCartIndex].c_user_id) == true)
            {
                MessageBox.Show("1 user can only have 1 cart!");
            }
            else
            {
                _apiClient
                   .PutAsync(SelectedCart, "http://localhost:8921/cart")
                   .ContinueWith((task) =>
                   {
                       Application.Current.Dispatcher.Invoke(() =>
                       {
                           if
                           (
                               //Check if something is null
                               String.IsNullOrEmpty(SelectedCart.c_id.ToString()) && String.IsNullOrWhiteSpace(SelectedCart.c_id.ToString()) ||
                               String.IsNullOrEmpty(SelectedCart.c_creditcardNumber) && String.IsNullOrWhiteSpace(SelectedCart.c_creditcardNumber) ||
                               String.IsNullOrEmpty(SelectedCart.c_deliver.ToString()) && String.IsNullOrWhiteSpace(SelectedCart.c_deliver.ToString()) ||
                               String.IsNullOrEmpty(SelectedCart.c_billingAddress) && String.IsNullOrWhiteSpace(SelectedCart.c_billingAddress) ||
                               String.IsNullOrEmpty(SelectedCart.c_user_id.ToString()) && String.IsNullOrWhiteSpace(SelectedCart.c_user_id.ToString())
                           )
                           {
                               MessageBox.Show("You have to fill everything!");
                           //Refresh the list with new data
                           ListBoxRefresh();
                           }
                           else
                           {
                               Carts[SelectedCartIndex].c_id = SelectedCart.c_id;
                               Carts[SelectedCartIndex].c_creditcardNumber = SelectedCart.c_creditcardNumber;
                               Carts[SelectedCartIndex].c_deliver = SelectedCart.c_deliver;
                               Carts[SelectedCartIndex].c_billingAddress = SelectedCart.c_billingAddress;
                               Carts[SelectedCartIndex].c_user_id = SelectedCart.c_user_id;
                               ListBoxRefresh();
                           }
                       });
                   });
            }
        }

        private void DeleteCart()
        {
            try
            {
                _apiClient
                    .DeleteAsync(SelectedCart.c_id, "http://localhost:8921/cart")
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
                .GetAsync<List<Cart>>("http://localhost:8921/cart")
                .ContinueWith((carts) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Carts.Clear();
                        carts.Result.ForEach((cart) =>
                        {
                            Carts.Add(cart);
                        });
                    });
                });
        }
    }
}
