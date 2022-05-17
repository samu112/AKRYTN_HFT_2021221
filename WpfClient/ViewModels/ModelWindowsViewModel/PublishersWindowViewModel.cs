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
    class PublishersWindowViewModel : ObservableRecipient
    {
        private ApiClient _apiClient = new ApiClient();

        public ObservableCollection<Publisher> Publishers { get; set; }

        private Publisher _selectedPublisher;

        public Publisher SelectedPublisher
        {
            get => _selectedPublisher;
            set
            {
                _selectedPublisher = new Publisher();
                CopyPublisher(value, _selectedPublisher);

                OnPropertyChanged();
            }
        }

        private int _selectedPublisherIndex;

        public int SelectedPublisherIndex
        {
            get => _selectedPublisherIndex;
            set
            {
                SetProperty(ref _selectedPublisherIndex, value);
            }
        }

        public RelayCommand AddPublisherCommand { get; set; }
        public RelayCommand EditPublisherCommand { get; set; }
        public RelayCommand DeletePublisherCommand { get; set; }

        public PublishersWindowViewModel()
        {
            Publishers = new ObservableCollection<Publisher>();
            //GetPublishers
            _apiClient
                .GetAsync<List<Publisher>>("http://localhost:8921/publisher")
                .ContinueWith((publishers) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        publishers.Result.ForEach((publisher) =>
                        {
                            Publishers.Add(publisher);
                        });
                    });
                });
            AddPublisherCommand = new RelayCommand(AddPublisher);
            EditPublisherCommand = new RelayCommand(EditPublisher);
            DeletePublisherCommand = new RelayCommand(DeletePublisher);
        }

        private void CopyPublisher(Publisher publisherCopiedFrom, Publisher publisherCopiedFor)
        {
            if (publisherCopiedFrom != null && publisherCopiedFor != null)
            {
                publisherCopiedFor.p_id = publisherCopiedFrom.p_id;
                publisherCopiedFor.p_name = publisherCopiedFrom.p_name;
                publisherCopiedFor.p_website = publisherCopiedFrom.p_website;
                publisherCopiedFor.p_email = publisherCopiedFrom.p_email;
                publisherCopiedFor.p_address = publisherCopiedFrom.p_address;
                publisherCopiedFor.Books = publisherCopiedFrom.Books;
            }
        }

        private void AddPublisher()
        {
            if (SelectedPublisher == null)
            {
                SelectedPublisher = new Publisher();
            }
            else
            {
                Publisher newPublisher = new Publisher()
                {
                    p_name = SelectedPublisher.p_name,
                    p_address = SelectedPublisher.p_address,
                    p_email = SelectedPublisher.p_email,
                    p_website = SelectedPublisher.p_website,
                };

                _apiClient
                    .PostAsync(newPublisher, "http://localhost:8921/publisher")
                    .ContinueWith((task) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if
                            (
                                //Check if something is null
                                String.IsNullOrEmpty(SelectedPublisher.p_id.ToString()) && String.IsNullOrWhiteSpace(SelectedPublisher.p_id.ToString()) ||
                                String.IsNullOrEmpty(SelectedPublisher.p_name) && String.IsNullOrWhiteSpace(SelectedPublisher.p_name) ||
                                String.IsNullOrEmpty(SelectedPublisher.p_address) && String.IsNullOrWhiteSpace(SelectedPublisher.p_address) ||
                                String.IsNullOrEmpty(SelectedPublisher.p_email) && String.IsNullOrWhiteSpace(SelectedPublisher.p_email) ||
                                String.IsNullOrEmpty(SelectedPublisher.p_website) && String.IsNullOrWhiteSpace(SelectedPublisher.p_website)
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

        private void EditPublisher()
        {
            _apiClient
               .PutAsync(SelectedPublisher, "http://localhost:8921/publisher")
               .ContinueWith((task) =>
               {
                   Application.Current.Dispatcher.Invoke(() =>
                   {
                       if
                       (
                           //Check if something is null
                                String.IsNullOrEmpty(SelectedPublisher.p_id.ToString()) && String.IsNullOrWhiteSpace(SelectedPublisher.p_id.ToString()) ||
                                String.IsNullOrEmpty(SelectedPublisher.p_name) && String.IsNullOrWhiteSpace(SelectedPublisher.p_name) ||
                                String.IsNullOrEmpty(SelectedPublisher.p_address) && String.IsNullOrWhiteSpace(SelectedPublisher.p_address) ||
                                String.IsNullOrEmpty(SelectedPublisher.p_email) && String.IsNullOrWhiteSpace(SelectedPublisher.p_email) ||
                                String.IsNullOrEmpty(SelectedPublisher.p_website) && String.IsNullOrWhiteSpace(SelectedPublisher.p_website)
                       )
                       {
                           MessageBox.Show("You have to fill everything!");
                            //Refresh the list with new data
                            ListBoxRefresh();
                       }
                       else
                       {
                           Publishers[SelectedPublisherIndex].p_id = SelectedPublisher.p_id;
                           Publishers[SelectedPublisherIndex].p_name = SelectedPublisher.p_name;
                           Publishers[SelectedPublisherIndex].p_address = SelectedPublisher.p_address;
                           Publishers[SelectedPublisherIndex].p_email = SelectedPublisher.p_email;
                           Publishers[SelectedPublisherIndex].p_website = SelectedPublisher.p_website;
                           ListBoxRefresh();
                       }
                   });
               });
        }

        private void DeletePublisher()
        {
            try
            {
                _apiClient
                    .DeleteAsync(SelectedPublisher.p_id, "http://localhost:8921/publisher")
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
                .GetAsync<List<Publisher>>("http://localhost:8921/publisher")
                .ContinueWith((publishers) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Publishers.Clear();
                        publishers.Result.ForEach((publisher) =>
                        {
                            Publishers.Add(publisher);
                        });
                    });
                });
        }
    }
}
