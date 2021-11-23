using RestSharp;
using CryptoTracker.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AppCenter.Crashes;

namespace CryptoTracker
{
    public partial class MainPage : ContentPage
    {

        private string CoinAPIKey = "E105897B-2C00-42B7-B3A3-ED9D27C0AD6F";
        private string CoinImgUrl = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_64/";

        public MainPage()
        {
            InitializeComponent();
            coinListView.ItemsSource = GetCoinAPI();
            
        }

        private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            coinListView.ItemsSource = GetCoinAPI();
        }

        public List<CoinAPI> GetCoinAPI()
        {
            List<CoinAPI> coins;

            var client = new RestClient("http://rest.coinapi.io/v1/assets?filter_asset_id=BTC;ETH;XMR;LTC;DOGE");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-CoinAPI-Key", CoinAPIKey);

            var response = client.Execute(request);
            coins = JsonConvert.DeserializeObject<List<CoinAPI>>(response.Content);

            foreach (var c in coins)
            {
                if (!String.IsNullOrEmpty(c.Id_icon))
                {
                    c.Icon_url = CoinImgUrl + c.Id_icon.Replace("-", "") + ".png";
                }
                else
                {
                    c.Id_icon = "";
                }
            }
            return coins;
        }

        private void CrashBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                Crashes.GenerateTestCrash();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex, new Dictionary<string, string>() { { "Crash", "true" } });
            }
        }
    }
}