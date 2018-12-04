using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SplitBill.DependencyServices;
using Xamarin.Forms;

namespace SplitBill.Views
{
    public partial class AddPayees : ContentPage
    {
        public List<Contact> _ContactsList { get; set; }
        private ObservableCollection<Contact> ContactsList { get; set; }
        private ObservableCollection<Contact> selectedContactsList;
        public AddPayees(List<Contact> _contactsList)
        {
            _ContactsList = _contactsList;
            ContactsList = new ObservableCollection<Contact>();
            foreach(var item in _ContactsList)
            {
                ContactsList.Add(item);
            }
            BindingContext = this;
            InitializeComponent();
            contactsListView.ItemsSource = ContactsList;
            contactsListView.ItemSelected += ContactSelected;
            selectedContactsList = new ObservableCollection<Contact>();
        }

        void BackButtonClicked(object sender, System.EventArgs e)
        {
            try
            {
                Navigation.PopModalAsync(false);
            }
            catch (Exception ex)
            {
                DataPrintx.PrintException(ex);
            }
        }

        void NextButtonClicked(object sender, System.EventArgs e)
        {
            try
            {
                Navigation.PopModalAsync(false);
            }
            catch (Exception ex)
            {
                DataPrintx.PrintException(ex);
            }
        }

        void ContactSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var itemSelected = ((ListView)sender).SelectedItem;
                if (itemSelected == null)
                {
                    return;
                }
                else
                {
                    var _itemSelected = (Contact)itemSelected;
                    selectedContactsList.Add(_itemSelected);
                    SelectedContactView selectedContact = new SelectedContactView(_itemSelected)
                    {
                    };
                    selectedContactsStack.Children.Add(selectedContact);
                    selectedContact.ItemUnSelected += (object _sender, EventArgs _e) =>
                    {
                        try
                        {
                            var _owner = (SelectedContactView)_sender;
                            selectedContactsStack.Children.Remove(_owner);
                            selectedContactsList.Remove(_itemSelected);
                        }
                        catch (Exception ex)
                        {
                            DataPrintx.PrintException(ex);
                        }
                    };
                }
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                DataPrintx.PrintException(ex);
            }
        }
    }

    public class SelectedContactView : ContentView
    {
        public event EventHandler<EventArgs> ItemUnSelected;
        public Contact Contact { get; set; }
        public SelectedContactView(Contact _contact)
        {
            Contact = _contact;
            SetLayout();
        }

        void SetLayout()
        {
            try
            {
                Label nameLabel = new Label()
                {
                    Text = Contact.DisplayName,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                Image closeImage = new Image()
                {
                    Source = "error16.png",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                TapGestureRecognizer closeImageTap = new TapGestureRecognizer();
                closeImageTap.NumberOfTapsRequired = 1;
                closeImageTap.Tapped += (object sender, EventArgs e) => 
                {
                    try
                    {
                        var handler = ItemUnSelected;
                        if(handler != null)
                        {
                            handler(this, e);
                        }
                    }
                    catch(Exception ex)
                    {
                        DataPrintx.PrintException(ex);
                    }
                };
                closeImage.GestureRecognizers.Add(closeImageTap);
                Grid gridHolder = new Grid()
                {
                    RowDefinitions = {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto }
                        //new RowDefinition { Height = new GridLength(6, GridUnitType.Star) },
                        //new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                    },
                    ColumnDefinitions = {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    },
                    BackgroundColor = Color.Gray,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };
                gridHolder.Children.Add(nameLabel, 0, 0);
                gridHolder.Children.Add(closeImage, 1, 0);
                StackLayout holder = new StackLayout()
                {
                    Children = { gridHolder },
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                Content = holder;
            }
            catch(Exception ex)
            {
                DataPrintx.PrintException(ex);
            }
        }
    }
}
