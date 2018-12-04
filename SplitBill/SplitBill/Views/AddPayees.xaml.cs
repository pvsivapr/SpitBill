using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SplitBill.DependencyServices;
using Xamarin.Forms;
using System.Linq;

namespace SplitBill.Views
{
    public partial class AddPayees : ContentPage
    {
        public List<Contact> _ContactsList { get; set; }
        //private ObservableCollection<Contact> ContactsList { get; set; }
        //private ObservableCollection<Contact> selectedContactsList;
        private ObservableCollection<PayeeList> ContactsList { get; set; }
        private ObservableCollection<PayeeList> selectedContactsList;
        public AddPayees(List<Contact> _contactsList)
        {
            _ContactsList = _contactsList;
            ContactsList = new ObservableCollection<PayeeList>();
            foreach(var item in _ContactsList)
            {
                ContactsList.Add(new PayeeList{
                    ContactId = item.ContactId,
                    ContactName = item.ContactName,
                    DisplayName = item.DisplayName,
                    PrimaryContactNumber = item.PrimaryContactNumber,
                    AllContactNumbers = item.AllContactNumbers,
                    xyz = item.xyz,
                    //IsSelected = false,
                    //BackgroundColor = Color.Green
                });
            }
            BindingContext = this;
            InitializeComponent();
            contactsListView.ItemsSource = ContactsList;
            contactsListView.ItemSelected += ContactSelected;
            selectedContactsList = new ObservableCollection<PayeeList>();

            //var source = contactsListView.ItemTemplate;
            //source.PropertyChanged += 

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

        //void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        void ContactSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var _itemSelected = ((ListView)sender).SelectedItem;
                if (_itemSelected == null)
                {
                    return;
                }
                else
                {
                    var itemSelected = (PayeeList)_itemSelected;
                    if (itemSelected.BackgroundColor == Color.Green)
                    {
                        selectedContactsList.Add(itemSelected);
                        SelectedContactView selectedContact = new SelectedContactView(itemSelected)
                        {
                        };
                        selectedContactsStack.Children.Add(selectedContact);
                        selectedContact.ItemUnSelected += (object _sender, EventArgs _e) =>
                        {
                            try
                            {
                                var _owner = (SelectedContactView)_sender;
                                var _unSelectedItem = _owner.PayeeList;
                                var _selectedItem = ContactsList.Where<PayeeList>(X => X.ContactId == _unSelectedItem.ContactId).FirstOrDefault();
                                _selectedItem.BackgroundColor = Color.Green;
                                var unSelectedItem = selectedContactsList.Where<PayeeList>(X => X.ContactId == _unSelectedItem.ContactId).FirstOrDefault();
                                if (unSelectedItem != null)
                                {
                                    selectedContactsList.Remove(unSelectedItem);
                                }
                                selectedContactsStack.Children.Remove(_owner);
                            }
                            catch (Exception ex)
                            {
                                DataPrintx.PrintException(ex);
                            }
                        };
                        itemSelected.BackgroundColor = Color.Maroon;
                    }
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
        public PayeeList PayeeList { get; set; }
        public SelectedContactView(PayeeList _payeeList)
        {
            PayeeList = _payeeList;
            SetLayout();
        }

        void SetLayout()
        {
            try
            {
                Label nameLabel = new Label()
                {
                    Text = PayeeList.DisplayName,
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

    public class PayeeList : System.ComponentModel.INotifyPropertyChanged
    {
        public string ContactId { get; set; }
        public string ContactName { get; set; }
        public string DisplayName { get; set; }
        public string PrimaryContactNumber { get; set; }
        public List<double> AllContactNumbers { get; set; }
        public string xyz { get; set; }

        public bool isSelected = false;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("isSelected"));
                }
            } 
        }

        public Color backgroundColor = Color.Green;
        public Color BackgroundColor
        {
            get
            {
                return backgroundColor;
            }
            set
            {
                if (backgroundColor != value)
                {
                    backgroundColor = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("backgroundColor"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate {};

    }
}
