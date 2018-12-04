using System;
using System.Collections.Generic;
using SplitBill.DependencyServices;
using Xamarin.Forms;

namespace SplitBill.Views
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
        }

        async void SubmitButtonClicked(object sender, System.EventArgs e)
        {
            try
            {
                //var list = await DependencyService.Get<IContacts>().GetAllContacts();
                List<Contact> list = new List<Contact>();
                list.Add(new Contact { ContactId = "1", DisplayName = "Sivaprasad", PrimaryContactNumber = "9898989898" });
                list.Add(new Contact { ContactId = "2", DisplayName = "Sivaprasad Reddy", PrimaryContactNumber = "6565656565" });
                list.Add(new Contact { ContactId = "3", DisplayName = "Venkata Sivaprasad", PrimaryContactNumber = "7894545513" });
                list.Add(new Contact { ContactId = "4", DisplayName = "Prasad Reddy", PrimaryContactNumber = "1256456455" });
                list.Add(new Contact { ContactId = "5", DisplayName = "PVSPReddy", PrimaryContactNumber = "9895654656" });
                list.Add(new Contact { ContactId = "6", DisplayName = "PVSIVAPR", PrimaryContactNumber = "7454548585" });
                list.Add(new Contact { ContactId = "7", DisplayName = "Venkata", PrimaryContactNumber = "3256874562" });
                list.Add(new Contact { ContactId = "8", DisplayName = "Prasad", PrimaryContactNumber = "4563214565" });

                await Navigation.PushModalAsync(new DisplayContactsPage(list));
            }
            catch (Exception ex)
            {
                DataPrintx.PrintException(ex);
            }
        }
    }
}
