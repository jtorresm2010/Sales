using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Sales.Helpers;
using Sales.Models;

namespace Sales.Services
{
    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    //Message = "Turn on your internet, ya dingus",
                    Message = Languages.TurnOnInet,
                };
            }


            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("http://www.google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    //Message = "No connection =("
                    Message = Languages.NoInternet,
                };
            }




            return new Response
            {
                IsSuccess = true,
            };
        }




        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            //GetList<T> es una lista generica que se debe especificar al momento de usarse
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(answer);

                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response //Declaracion de un nuevo objeto con las siguientes propiedades
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

        }

    }
}
