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

        public MainWindowViewModel()
        {
            ManageUsersCommand = new RelayCommand(OpenUsersWindow);
            ManageBooksCommand = new RelayCommand(OpenBooksWindow);
        }

        private void OpenUsersWindow()
        {
            new UsersWindow().Show();
        }
        private void OpenBooksWindow()
        {
            new BooksWindow().Show();
        }
    }
}
