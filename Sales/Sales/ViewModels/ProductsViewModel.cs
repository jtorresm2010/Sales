using GalaSoft.MvvmLight.Command;
using Sales.Helpers;
using Sales.Models;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class ProductsViewModel : BaseViewModel /* Esta clase importada sirve para refrescar las propiedades cuando estas cambien */
    {

        private ApiService apiService;

        public ObservableCollection<Product> products;

        public bool isRefreshing;

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        public ObservableCollection<Product> Products {

            get { return this.products; }
            set { this.SetValue(ref this.products, value); } /* al momento de setear el atributo se ejecuta SetValue, que se encuentra en la clase BaseViewModel. Esto actualiza sus valores */

        }

        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();

        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();


            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            //var response = await this.apiService.GetList<Product>("https://jsonplaceholder.typicode.com", "/posts", "");
            var url = Application.Current.Resources["UrlAPI"].ToString(); // Esta operacion devuelve un objeto, se debe convertir con ToString
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var response = await this.apiService.GetList<Product>(url, prefix, "");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var list = (List<Product>)response.Result;

            var sublist = list.GetRange(5, 30);
            
            this.Products = new ObservableCollection<Product>(sublist);
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }
    }
}
