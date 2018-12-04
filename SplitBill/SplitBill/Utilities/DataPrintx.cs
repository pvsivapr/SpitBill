using System;

using Xamarin.Forms;

namespace SplitBill//.Utilities
{
    public class DataPrintx : ContentPage
    {
        public DataPrintx(){}

        public static void PrintException(Exception ex)
        {
            var msg = ex.Message + "\n" + ex.StackTrace;
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }
}

