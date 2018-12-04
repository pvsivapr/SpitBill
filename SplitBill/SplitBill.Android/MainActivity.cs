using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SplitBill.Droid
{
    [Activity(Label = "SplitBill", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            GetAllContacts();

            LoadApplication(new App());
        }

        public async Task<List<string>> GetAllContacts()
        {
            List<string> contactList = null;
            try
            {
                var uri = Android.Provider.ContactsContract.Contacts.ContentUri;
                string[] projection = {
                    Android.Provider.ContactsContract.Contacts.InterfaceConsts.Id,
                    Android.Provider.ContactsContract.Contacts.InterfaceConsts.DisplayName
                };
                //var cursor = this.ManagedQuery(uri, projection, null, null, null);
                using (var cursor = this.ManagedQuery(uri, projection, null, null, null))
                {
                    contactList = new List<string>();
                    if (cursor.MoveToFirst())
                    {
                        do
                        {
                            contactList.Add(cursor.GetString(cursor.GetColumnIndex(projection[1])));
                        } while (cursor.MoveToNext());
                    }
                }
            }
            catch (Exception ex)
            {
                DataPrintx.PrintException(ex);
            }
            return contactList;
        }

    }
}

