using Listing.App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listing.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListContentPage : ContentPage
    {
        private static ListModel list_info;
        private readonly HttpClient _httpClient = new HttpClient();
        private SingleUser owner_user;
        private static CompleteUser login_user;
        private ObservableCollection<ListContent> temp_items;

        public ListContentPage()
        {
            InitializeComponent();
            favorite_checkbox.CheckedChanged += Favorite_checkbox_CheckedChanged;
            genContent();
        }

        public static void InitListContentPage(CompleteUser cu)
        {
            login_user = cu;
        }

        private async void Favorite_checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                // We need to add the list to the user's favorite list
                Favorite fav = new Favorite
                {
                    UserId = login_user.Id,
                    ListId = list_info.Id
                };

                var token = await SecureStorage.GetAsync("token");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string json = JsonConvert.SerializeObject(fav);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                string url = "https://listingdb.azurewebsites.net/api/Favorite";

                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Error", "An unexpected error ocurred. Please try again.", "Ok");
                    favorite_checkbox.IsChecked = false;
                }
            }
            else
            {
                // We need to delete the list to the user's favorite list
                string url = "https://listingdb.azurewebsites.net/api/Favorite/UserIDListID/" + login_user.Id.ToString() + "&" + list_info.Id.ToString();
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var resultado_json = response.Content.ReadAsStringAsync().Result;
                    Favorite fav = new Favorite();
                    fav = Favorite.FromJson(resultado_json);

                    var token = await SecureStorage.GetAsync("token");

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    string url_delete = "https://listingdb.azurewebsites.net/api/Favorite/" + fav.Id.ToString();
                    var response_delete = await client.DeleteAsync(url_delete);

                    if (!response_delete.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Error", "An unexpected error ocurred. Please try again.", "Ok");
                        favorite_checkbox.IsChecked = true;
                    }
                }
                else
                {
                    await DisplayAlert("Error", "An unexpected error ocurred. Please try again.", "Ok");
                    favorite_checkbox.IsChecked = true;
                }
            }
        }

        private async void genContent()
        {
            label_title.Text = list_info.Title;

            // Info about the owner
            string url = "https://listingdb.azurewebsites.net/api/User/" + list_info.OwnerId.ToString();

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                owner_user = new SingleUser();
                owner_user = JsonConvert.DeserializeObject<SingleUser>(result);

                label_owner.Text = owner_user.Name + " - " + owner_user.Institution;
            }

            // Favorite already?
            url = "https://listingdb.azurewebsites.net/api/Favorite/UserIDListID/" + login_user.Id.ToString() + "&" + list_info.Id.ToString();
            response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NoContent)
            {
                favorite_checkbox.CheckedChanged -= Favorite_checkbox_CheckedChanged;
                favorite_checkbox.IsChecked = true;
                favorite_checkbox.CheckedChanged += Favorite_checkbox_CheckedChanged;
            }
            else
            {
                // TODO: What to do here?
            }

            // Content of the list
            url = "https://listingdb.azurewebsites.net/api/ListContent/ListID/" + list_info.Id.ToString();
            response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                List<ListContent> list_list_content = new List<ListContent>();
                list_list_content = JsonConvert.DeserializeObject<List<ListContent>>(result);

                temp_items = new ObservableCollection<ListContent>(list_list_content);
                ListContentView.ItemsSource = temp_items;
            }

        }

        public static void InitListContentPage(ListModel l)
        {
            list_info = l;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
