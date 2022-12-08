using Listing.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Listing.App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavoritesPage : ContentPage
	{
        private ObservableCollection<ListModel> temp_items = new ObservableCollection<ListModel>();
        private static CompleteUser login_user;

        public FavoritesPage()
        {
            InitializeComponent();
            //showLists();
            return;
        }

        public static void InitFavoritesPage(CompleteUser lu)
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

            string url = "https://listingdb.azurewebsites.net/api/Favorite/UserID/" + login_user.Id.ToString();
            var resultado = await client.GetAsync(url);

            if (!resultado.IsSuccessStatusCode) return;

            var resultado_json = resultado.Content.ReadAsStringAsync().Result;
            List<FavoriteLists> list_favs = new List<FavoriteLists>();
            list_favs = FavoriteLists.FromJson(resultado_json);

            temp_items = new ObservableCollection<ListModel>();
            string url_get_list_by_id = "https://listingdb.azurewebsites.net/api/List/ListId/";
            foreach (var list in list_favs)
            {
                string url_qry = url_get_list_by_id + list.ListId.ToString();
                var list_result = await client.GetAsync(url_qry);

                if (!list_result.IsSuccessStatusCode) continue;

                var list_json = list_result.Content.ReadAsStringAsync().Result;
                SingleList real_list = new SingleList();
                real_list = SingleList.FromJson(list_json);

                ListModel temp_list = new ListModel
                {
                    Id = real_list.Id,
                    OwnerId = real_list.OwnerId,
                    Title = real_list.Title,
                    RegistrationDate = real_list.RegistrationDate
                };

                temp_items.Add(temp_list);
            }

            FavoriteList.ItemsSource = temp_items;
            return;
        }

    }
}