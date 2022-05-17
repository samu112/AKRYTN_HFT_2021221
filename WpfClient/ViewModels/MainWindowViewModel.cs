using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RelayCommand ManageUsersCommand { get; set; }
        public RelayCommand ManageBooksCommand { get; set; }
        public RelayCommand ManagePublishersCommand { get; set; }
        public RelayCommand ManageCartItemsCommand { get; set; }

        public MainWindowViewModel()
        {
            ManageUsersCommand = new RelayCommand(OpenUsersWindow);
            ManageBooksCommand = new RelayCommand(OpenBooksWindow);
            ManagePublishersCommand = new RelayCommand(OpenPublishersWindow);
            ManageCartItemsCommand = new RelayCommand(OpenCartItemsWindow);
        }

        private void OpenUsersWindow()
        {
            new UsersWindow().Show();
        }
        private void OpenBooksWindow()
        {
            new BooksWindow().Show();
        }
        private void OpenPublishersWindow()
        {
            new PublishersWindow().Show();
        }
        private void OpenCartItemsWindow()
        {
            new CartItemsWindow().Show();
        }
    }
}
