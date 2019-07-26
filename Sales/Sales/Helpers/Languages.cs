using Sales.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Sales.Resources;

namespace Sales.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }
        public static string TurnOnInet
        {
            get { return Resource.TurnOnInet; }
        }
        public static string Error
        {
            get { return Resource.Error; }
        }
        public static string Products
        {
            get { return Resource.Products; }
        }
        public static string NoInternet
        {
            get { return Resource.NoInternet; }
        }
        public static string AddProducts
        {
            get { return Resource.AddProducts; }
        }

    }
}
