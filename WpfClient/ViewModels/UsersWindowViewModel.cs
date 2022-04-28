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

        public RelayCommand AddUserCommand { get; set; }
        public RelayCommand EditUserCommand { get; set; }
        public RelayCommand DeleteUserCommand { get; set; }

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
        }

        private void AddUser()
        {

        }

        private void EditUser()
        {
            _apiClient
                .PutAsync(SelectedUser, "http://localhost:8921/user")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int tempUserIndex = SelectedUserIndex;
                        Users.Remove(SelectedUser);
                        SelectedUserIndex = tempUserIndex;
                        Users.Insert(SelectedUserIndex, SelectedUser);
                    });
                });
        }

        private void DeleteUser()
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
    }
}
