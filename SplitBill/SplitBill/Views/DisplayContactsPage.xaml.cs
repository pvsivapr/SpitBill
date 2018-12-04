using System;
using System.Collections.Generic;
using SplitBill.DependencyServices;
using Xamarin.Forms;

namespace SplitBill.Views
{
    public partial class DisplayContactsPage : ContentPage
    {
        public List<Contact> ContactsList { get; set; }
        public DisplayContactsPage(List<Contact> _contactsList)
        {
            ContactsList = _contactsList;
            BindingContext = this;
            InitializeComponent();
            contactsListView.ItemSelected += ContactSelected;
        }

        void BackButtonClicked(object sender, System.EventArgs e)
        {
            try
            {
                Navigation.PopModalAsync(false);
            }
            catch(Exception ex)
            {
                DataPrintx.PrintException(ex);
            }
        }

        void NextButtonClicked(object sender, System.EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync(new AddPayees(ContactsList));
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
                if(itemSelected == null)
                {
                    return;
                }
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                DataPrintx.PrintException(ex);
            }
        }

    }
}
