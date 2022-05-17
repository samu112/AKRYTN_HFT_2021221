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
    class BooksWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Book> Books { get; set; }

        public ObservableCollection<int> AllPublisherIds { get; set; }

        private Book _selectedBook;

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = new Book();
                CopyBook(value, _selectedBook);

                OnPropertyChanged();
                //SetProperty(ref _selectedBook, value);
            }
        }

        private int _selectedBookIndex;

        public int SelectedBookIndex
        {
            get => _selectedBookIndex;
            set
            {
                SetProperty(ref _selectedBookIndex, value);
            }
        }

        public RelayCommand AddBookCommand { get; set; }
        public RelayCommand EditBookCommand { get; set; }
        public RelayCommand DeleteBookCommand { get; set; }

        public BooksWindowViewModel()
        {
            Books = new ObservableCollection<Book>();
            //GetBooks
            _apiClient
                .GetAsync<List<Book>>("http://localhost:8921/book")
                .ContinueWith((books) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        books.Result.ForEach((book) =>
                        {
                            Books.Add(book);
                        });
                    });
                });
            //Get publishers
            AllPublisherIds = new ObservableCollection<int>();
            _apiClient
                .GetAsync<List<Publisher>>("http://localhost:8921/publisher")
                .ContinueWith((publishers) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        publishers.Result.ForEach((publisher) =>
                        {
                            AllPublisherIds.Add(publisher.p_id);
                        });
                    });
                });
            AddBookCommand = new RelayCommand(AddBook);
            EditBookCommand = new RelayCommand(EditBook);
            DeleteBookCommand = new RelayCommand(DeleteBook);
        }

        private void CopyBook(Book bookCopiedFrom, Book bookCopiedFor)
        {
            if (bookCopiedFrom != null && bookCopiedFor != null)
            {
                bookCopiedFor.b_id = bookCopiedFrom.b_id;
                bookCopiedFor.b_author = bookCopiedFrom.b_author;
                bookCopiedFor.b_price = bookCopiedFrom.b_price;
                bookCopiedFor.b_publisher_id = bookCopiedFrom.b_publisher_id;
                bookCopiedFor.b_releaseDate = bookCopiedFrom.b_releaseDate;
                bookCopiedFor.b_title = bookCopiedFrom.b_title;
                bookCopiedFor.CartItem = bookCopiedFrom.CartItem;
                bookCopiedFor.Publisher = bookCopiedFrom.Publisher;
            }
        }

        private void AddBook()
        {
            if (SelectedBook == null)
            {
                SelectedBook = new Book();
            }
            if (SelectedBook.b_releaseDate == DateTime.MinValue)
            {
                MessageBox.Show("Invalid Date!");
            }
            else
            {
                Book newBook = new Book()
                {
                    b_title=SelectedBook.b_title,
                    b_releaseDate = SelectedBook.b_releaseDate,
                    b_author=SelectedBook.b_author,
                    b_price=SelectedBook.b_price,
                    b_publisher_id=SelectedBook.b_publisher_id
                };

                _apiClient
                    .PostAsync(newBook, "http://localhost:8921/book")
                    .ContinueWith((task) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if
                            (
                                //Check if something is null
                                String.IsNullOrEmpty(SelectedBook.b_id.ToString()) && String.IsNullOrWhiteSpace(SelectedBook.b_id.ToString()) ||
                                String.IsNullOrEmpty(SelectedBook.b_title) && String.IsNullOrWhiteSpace(SelectedBook.b_title) ||
                                String.IsNullOrEmpty(SelectedBook.b_releaseDate.ToString()) && String.IsNullOrWhiteSpace(SelectedBook.b_releaseDate.ToString()) ||
                                String.IsNullOrEmpty(SelectedBook.b_author) && String.IsNullOrWhiteSpace(SelectedBook.b_author) ||
                                String.IsNullOrEmpty(SelectedBook.b_price.ToString()) && String.IsNullOrWhiteSpace(SelectedBook.b_price.ToString()) ||
                                String.IsNullOrEmpty(SelectedBook.b_publisher_id.ToString()) && String.IsNullOrWhiteSpace(SelectedBook.b_publisher_id.ToString())
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

        private void EditBook()
        {
             _apiClient
                .PutAsync(SelectedBook, "http://localhost:8921/book")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if
                        (
                            //Check if something is null
                            String.IsNullOrEmpty(SelectedBook.b_id.ToString()) && String.IsNullOrWhiteSpace(SelectedBook.b_id.ToString()) ||
                            String.IsNullOrEmpty(SelectedBook.b_title) && String.IsNullOrWhiteSpace(SelectedBook.b_title) ||
                            String.IsNullOrEmpty(SelectedBook.b_releaseDate.ToString()) && String.IsNullOrWhiteSpace(SelectedBook.b_releaseDate.ToString()) ||
                            String.IsNullOrEmpty(SelectedBook.b_author) && String.IsNullOrWhiteSpace(SelectedBook.b_author) ||
                            String.IsNullOrEmpty(SelectedBook.b_price.ToString()) && String.IsNullOrWhiteSpace(SelectedBook.b_price.ToString()) ||
                            String.IsNullOrEmpty(SelectedBook.b_publisher_id.ToString()) && String.IsNullOrWhiteSpace(SelectedBook.b_publisher_id.ToString())
                        )
                        {
                            MessageBox.Show("You have to fill everything!");
                            //Refresh the list with new data
                            ListBoxRefresh();
                        }
                        else
                        {
                            Books[SelectedBookIndex].b_id = SelectedBook.b_id;
                            Books[SelectedBookIndex].b_title = SelectedBook.b_title;
                            Books[SelectedBookIndex].b_releaseDate = SelectedBook.b_releaseDate;
                            Books[SelectedBookIndex].b_author = SelectedBook.b_author;
                            Books[SelectedBookIndex].b_price = SelectedBook.b_price;
                            Books[SelectedBookIndex].b_publisher_id = SelectedBook.b_publisher_id;
                            ListBoxRefresh();
                        }
                    });
                });
        }

        private void DeleteBook()
        {
            try
            {
                _apiClient
                    .DeleteAsync(SelectedBook.b_id, "http://localhost:8921/book")
                    .ContinueWith((task) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            //Books.Remove(SelectedBook);
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
                .GetAsync<List<Book>>("http://localhost:8921/book")
                .ContinueWith((books) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Books.Clear();
                            books.Result.ForEach((book) =>
                            {
                                Books.Add(book);
                            });
                        });
                });
        }
    }
}

