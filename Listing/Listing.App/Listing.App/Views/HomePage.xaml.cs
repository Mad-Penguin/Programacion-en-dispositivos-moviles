using Listing.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listing.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<ListModel> Items { get; set; }
        private ObservableCollection<ListModel> temp_items;
        private static CompleteUser login_user;

        public HomePage()
        {
            InitializeComponent();
            showLists();
            return;
        }

        public static void InitHomePage(CompleteUser lu)
        {
            login_user = lu;
        }

        protected override void OnAppearing()
        {
            showLists();
        }

        private async void showLists()
        {
            var token = await SecureStorage.GetAsync("token");
            
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string url = "https://listingdb.azurewebsites.net/api/List?top=10";
            var resultado = await client.GetAsync(url);

            if (!resultado.IsSuccessStatusCode) return;

            var resultado_json = resultado.Content.ReadAsStringAsync().Result;
            List<ListModel> lista_registros = new List<ListModel>();
            lista_registros = ListModel.FromJson(resultado_json);

            temp_items = new ObservableCollection<ListModel>(lista_registros);
            HomeList.ItemsSource = temp_items;
            return;
        }
       

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;


            //await DisplayAlert("Item Tapped", temp_items[e.ItemIndex].Title, "OK");
            ListContentPage.InitListContentPage(temp_items[e.ItemIndex]);
            await Shell.Current.GoToAsync($"{nameof(ListContentPage)}");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
