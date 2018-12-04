using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Net;
using Android.OS;
using Android.Provider;
using SplitBill.DependencyServices;
using SplitBill.Droid.DependencyServices;
using SplitBill;
using Xamarin.Forms;

[assembly : Dependency(typeof(AndroidContactsService))]
namespace SplitBill.Droid.DependencyServices
{
    public class AndroidContactsService : IContacts
    {
        public AndroidContactsService(){}

        public async Task<List<Contact>> GetAllContacts()
        {
            List<Contact> contactList = null;
            var activity = Forms.Context as Android.App.Activity;
            try
            {
                var uri = ContactsContract.Contacts.ContentUri;

                string[] projection =
                {
                    ContactsContract.Contacts.InterfaceConsts.Id,
                    ContactsContract.Contacts.InterfaceConsts.DisplayName,
                    ContactsContract.Contacts.InterfaceConsts.PhotoId
                };
                // CursorLoader introduced in Honeycomb (3.0, API11)
                //var loader = new Android.Content.CursorLoader(activity, uri, projection, null, null, null);
                var loader = new Android.Content.CursorLoader(activity, uri, null, null, null, null);
                var cursor = (Android.Database.ICursor)loader.LoadInBackground();

                try
                {
                    if (cursor.MoveToFirst())
                    {
                        contactList = new List<Contact>();
                        do
                        {
                            Contact contact = new Contact();
                            var Id = cursor.GetString(cursor.GetColumnIndex(projection[0]));
                            contact.DisplayName = cursor.GetString(cursor.GetColumnIndex(projection[1]));
                            contact.ContactId = Id;

                            try
                            {
                                if (Int32.Parse(cursor.GetString(cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.HasPhoneNumber))) > 0)
                                {
                                    var _uri = ContactsContract.Contacts.ContentUri;
                                    string[] _projection = new string[]{ ContactsContract.CommonDataKinds.Phone.Number, ContactsContract.CommonDataKinds.CommonColumns.Type };
                                    var cr = activity.ContentResolver.Query(ContactsContract.CommonDataKinds.Phone.ContentUri, _projection, ContactsContract.RawContactsColumns.ContactId + "=" + Id, null, null);
                                    if(cr.MoveToFirst())
                                    {
                                        do
                                        {
                                            var PrimaryContactNumber = cr.GetString(cr.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number));
                                            contact.PrimaryContactNumber = PrimaryContactNumber;
                                        }
                                        while (cr.MoveToNext());
                                    }
                                }
                            }
                            catch(Exception ex)
                            {
                                DataPrintx.PrintException(ex);
                            }
                            contactList.Add(contact);
                        } 
                        while (cursor.MoveToNext());
                    }
                }
                catch(Exception ex)
                {
                    DataPrintx.PrintException(ex);
                }
            }
            catch(Exception ex)
            {
                DataPrintx.PrintException(ex);
            }

            /*
            try
            {
                //var intent = new Intent(activity, typeof(MediaActivity));
                //intent.PutExtra("id", 2);
                //activity.StartActivity(intent);
                var uri = ContactsContract.Contacts.ContentUri;
                string[] projection = {
                    ContactsContract.Contacts.InterfaceConsts.Id,
                    ContactsContract.Contacts.InterfaceConsts.DisplayName
                };
                //var cursor = this.ManagedQuery(uri, projection, null, null, null);
                //using (var cursor = activity.ManagedQuery(uri, projection, null, null, null))
                //{
                //    contactList = new List<string>();
                //    if (cursor.MoveToFirst())
                //    {
                //        do
                //        {
                //            contactList.Add(cursor.GetString(cursor.GetColumnIndex(projection[1])));
                //        } while (cursor.MoveToNext());
                //    }
                //}

                //Android.Content.ContentResolver cur = activity.ContentResolver;

                using (var cursor = activity.ManagedQuery(uri, null, null, null, null))
                {
                    contactList = new List<Contact>();
                    if (cursor.MoveToFirst())
                    {
                        do
                        {
                            Contact contact = new Contact();
                            try
                            {
                                var id = cursor.GetString(cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.Id));
                                contact.ContactId = id;
                                contact.DisplayName = cursor.GetString(cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.DisplayNamePrimary));
                                contact.ContactName = cursor.GetString(cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.DisplayName));
                                List<double> allMobileNumbers = new List<double>();
                                if (Int32.Parse(cursor.GetString(cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.HasPhoneNumber))) > 0)
                                {
                                    
                                    using (var cr = activity.ManagedQuery(ContactsContract.CommonDataKinds.Phone.ContentUri, new string[] { ContactsContract.CommonDataKinds.Phone.InterfaceConsts.ContactId }, null, new String[] { id }, null))
                                    {
                                        do
                                        {
                                            //var cr = activity.ManagedQuery() //(ContactsContract.CommonDataKinds.Phone.ContentUri, new string[] { ContactsContract.CommonDataKinds.Phone.InterfaceConsts.ContactId }, null, new String[] { id }, null);
                                            contact.PrimaryContactNumber = cr.GetString(cr.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number));

                                            //contact.PrimaryContactNumber = cr.GetString(cr.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number));



                                        }
                                        while (cr.MoveToNext());
                                    }
                                }
                                //contact.DisplayName = cursor.GetString(cursor.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.DisplayNamePrimary));
                            }
                            catch (Exception ex)
                            {
                                DataPrintx.PrintException(ex);
                            }
                            contactList.Add(contact);
                        }
                        while (cursor.MoveToNext());
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            */
            return contactList;
        }
    }

    public class ContactsActivity : Android.App.Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public async Task<List<string>> GetAllContacts()
        {
            List<string> contactList = null;
            try
            {
                var uri = ContactsContract.Contacts.ContentUri;
                string[] projection = {
                    ContactsContract.Contacts.InterfaceConsts.Id,
                    ContactsContract.Contacts.InterfaceConsts.DisplayName
                };
                var cursor = this.ManagedQuery(uri, projection, null, null, null);
                //using (var cursor = this.ManagedQuery(uri, projection, null, null, null))
                //{
                //    contactList = new List<string>();
                //    if (cursor.MoveToFirst())
                //    {
                //        do
                //        {
                //            contactList.Add(cursor.GetString(cursor.GetColumnIndex(projection[1])));
                //        } while (cursor.MoveToNext());
                //    }
                //}
            }
            catch (Exception ex)
            {
                DataPrintx.PrintException(ex);
            }
            return contactList;
        }
    }
}

