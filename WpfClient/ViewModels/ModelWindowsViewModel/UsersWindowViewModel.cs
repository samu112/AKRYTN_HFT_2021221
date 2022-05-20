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
using WpfClient.Clients;

namespace WpfClient.ViewModels
{
    class UsersWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<User> Users { get; set; }

        private User _selectedUser;

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser, value);
            }
        }

        private int _selectedUserIndex;

        public int SelectedUserIndex
        {
            get => _selectedUserIndex;
            set
            {
                SetProperty(ref _selectedUserIndex, value);
            }
        }

        private string year = "0";

        public string Year
        {
            get => year;
            set
            {
                SetProperty(ref year, value);
            }
        }

        public RelayCommand AddUserCommand { get; set; }
        public RelayCommand EditUserCommand { get; set; }
        public RelayCommand DeleteUserCommand { get; set; }
        public RelayCommand UserWithBookOlderThanXyearCommand { get; set; }

        public UsersWindowViewModel()
        {
            Users = new ObservableCollection<User>();

            _apiClient
                .GetAsync<List<User>>("http://localhost:8921/user")
                .ContinueWith((users) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        users.Result.ForEach((user) =>
                        {
                            Users.Add(user);
                        });
                    });
                });

            AddUserCommand = new RelayCommand(AddUser);
            EditUserCommand = new RelayCommand(EditUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            UserWithBookOlderThanXyearCommand = new RelayCommand(UserWithBookOlderThanXyear);
        }




        private void UserWithBookOlderThanXyear()
        {
            bool good = true;
            if (Year.Length > 0 && Year.Length < 4 && Year[0] != '0')
            {
                foreach (char c in Year)
                {
                    if (c < '0' || c > '9') { good = false; }
                }
            }
            else { good = false; }

            if (good == true)
            {
                var searchedUsers = _apiClient.UserWithBookOlderThanXyear("http://localhost:8921/user", Int32.Parse(Year));
                if (searchedUsers.Count > 0)
                {
                    string searchResultForThis = "";
                    for (int i = 0; i < searchedUsers.Count; i++)
                    {
                        searchResultForThis += (i + 1) + "; " + searchedUsers[i].u_name + "\n";
                    }
                    MessageBox.Show(searchResultForThis);
                }
                else { MessageBox.Show("No users found :("); }
            }
            else
            {
                MessageBox.Show("You have to give a valid number!");
            }

        }

        private void AddUser()
        {
            if (SelectedUser == null)
            {
                SelectedUser = new User();
            }
            if (SelectedUser.u_regDate == DateTime.MinValue)
            {
                MessageBox.Show("Invalid Date!");
            }
            else
            {
                User newUser = new User()
                {
                    u_name = SelectedUser.u_name,
                    u_regDate = SelectedUser.u_regDate,
                    u_email = SelectedUser.u_email,
                    u_address = SelectedUser.u_address
                }; 

                _apiClient
                    .PostAsync(newUser, "http://localhost:8921/user")
                    .ContinueWith((task) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if
                            (
                                //Check if something is null
                                String.IsNullOrEmpty(SelectedUser.u_id.ToString()) && String.IsNullOrWhiteSpace(SelectedUser.u_id.ToString()) ||
                                String.IsNullOrEmpty(SelectedUser.u_name) && String.IsNullOrWhiteSpace(SelectedUser.u_name) ||
                                String.IsNullOrEmpty(SelectedUser.u_regDate.ToString()) && String.IsNullOrWhiteSpace(SelectedUser.u_regDate.ToString()) ||
                                String.IsNullOrEmpty(SelectedUser.u_email) && String.IsNullOrWhiteSpace(SelectedUser.u_email) ||
                                String.IsNullOrEmpty(SelectedUser.u_address) && String.IsNullOrWhiteSpace(SelectedUser.u_address)
                            )
                            {
                                MessageBox.Show("You have to fill everything!");
                            }
                            else
                            {
                                //Refresh the list with new data
                                _apiClient
                                .GetAsync<List<User>>("http://localhost:8921/user")
                                .ContinueWith((users) =>
                                {
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        Users.Clear();
                                        users.Result.ForEach((user) =>
                                        {
                                            Users.Add(user);
                                        });
                                    });
                                });
                            }
                        });
                    });
            }
        }

        private void EditUser()
        {
            _apiClient
                .PutAsync(SelectedUser, "http://localhost:8921/user")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if 
                        (
                            //Check if something is null
                            String.IsNullOrEmpty(SelectedUser.u_id.ToString()) && String.IsNullOrWhiteSpace(SelectedUser.u_id.ToString()) ||
                            String.IsNullOrEmpty(SelectedUser.u_name) && String.IsNullOrWhiteSpace(SelectedUser.u_name) ||
                            String.IsNullOrEmpty(SelectedUser.u_regDate.ToString()) && String.IsNullOrWhiteSpace(SelectedUser.u_regDate.ToString()) ||
                            String.IsNullOrEmpty(SelectedUser.u_email) && String.IsNullOrWhiteSpace(SelectedUser.u_email) ||
                            String.IsNullOrEmpty(SelectedUser.u_address) && String.IsNullOrWhiteSpace(SelectedUser.u_address)
                        )
                        {
                            MessageBox.Show("You have to fill everything!");
                            //Refresh the list with new data
                            _apiClient
                                .GetAsync<List<User>>("http://localhost:8921/user")
                                .ContinueWith((users) =>
                                {
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        Users.Clear();
                                        users.Result.ForEach((user) =>
                                        {
                                            Users.Add(user);
                                        });
                                    });
                                });
                        }
                        else
                        {
                            Users[SelectedUserIndex].u_id = SelectedUser.u_id;
                            Users[SelectedUserIndex].u_name = SelectedUser.u_name;
                            Users[SelectedUserIndex].u_regDate = SelectedUser.u_regDate;
                            Users[SelectedUserIndex].u_email = SelectedUser.u_email;
                            Users[SelectedUserIndex].u_address = SelectedUser.u_address;
                        }
                    });
                });
        }

        private void DeleteUser()
        {
            try
            {
                _apiClient
                    .DeleteAsync(SelectedUser.u_id, "http://localhost:8921/user")
                    .ContinueWith((task) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Users.Remove(SelectedUser);
                        });
                    });
            }
            catch (NullReferenceException)
            {
                //Happens if user spamming the delete button or no enitity is selected
                MessageBox.Show("There is nothing to delet :(");
            }

        }
    }
}
