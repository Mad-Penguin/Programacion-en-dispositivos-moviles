using Listing.App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listing.App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateListPage : ContentPage
	{
        private static CompleteUser owner;
        private int list_id = -1;
        private ObservableCollection<ListContent> temp_items = new ObservableCollection<ListContent>();
        public CreateListPage ()
		{
			InitializeComponent ();
            btn_create.Clicked += Btn_create_Clicked;
            btn_add_content.Clicked += Btn_add_content_Clicked;
		}

        public static void InitCreateListPage(CompleteUser cu)
        {
            owner = cu;
        }

        private async void Btn_add_content_Clicked(object sender, EventArgs e)
        {
            if (txt_list_content.Text is null || txt_list_content.Text.Length == 0)
            {
                await DisplayAlert("Warning", "The content can't be empty", "Ok");
                return;
            }

            ListContent list_content = new ListContent
            {
                ListId = list_id,
                Content = txt_list_content.Text
            };

            var token = await SecureStorage.GetAsync("token");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(list_content);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            string url = "https://listingdb.azurewebsites.net/api/ListContent";

            var response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "An unexpected error ocurred. Please try again.", "Ok");
            }

            temp_items.Add(list_content);
            ListContentView.ItemsSource = temp_items;

            txt_list_content.Text = "";
            txt_list_content.Focus();
        }

        private async void Btn_create_Clicked(object sender, EventArgs e)
        {
            ListCreation new_list = new ListCreation
            {
                OwnerId = owner.Id,
                Title = txt_list_title.Text
            };

            var token = await SecureStorage.GetAsync("token");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(new_list);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            string url = "https://listingdb.azurewebsites.net/api/List";

            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                list_id = int.Parse(response.Content.ReadAsStringAsync().Result);
                await DisplayAlert("Info", "Your list has been created. Now you can add content to it.", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "An unexpected error ocurred. Please try again.", "Ok");
                return;
            }

            txt_list_title.IsEnabled = false;
            btn_create.IsEnabled = false;
            btn_create.IsVisible = false;

            txt_list_content.IsVisible = true;
            btn_add_content.IsVisible = true;
            txt_list_content.IsEnabled = true;
			btn_add_content.IsEnabled = true;
        }
    }
}