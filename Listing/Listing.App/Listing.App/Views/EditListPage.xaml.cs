using Listing.App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
	public partial class EditListPage : ContentPage
	{
        private static CompleteUser login_user;
        private static ListModel list;
        private static string username;
        private ObservableCollection<ListContent> temp_items;
        public EditListPage ()
		{
			InitializeComponent ();

            txt_title.Text = list.Title;

            showContent();

            btn_add_content.Clicked += Btn_add_content_Clicked;
            btn_save.Clicked += Btn_save_Clicked;

            message();

            
		}

        private async void message()
        {
            await DisplayAlert("Info","When you add or delete content the change is done. If you modify the title or a text content you need to hit the save button.", "Ok");
        }

        private async void Btn_save_Clicked(object sender, EventArgs e)
        {
            bool ok = true;
            // Update list title
            if(txt_title.Text is null || txt_title.Text.Length == 0)
            {
                ok = false;
            }
            else
            {
                string url = "https://listingdb.azurewebsites.net/api/List/" + list.Id.ToString() + "?title=" + txt_title.Text;
                HttpClient client = new HttpClient();
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);

                if (!response.IsSuccessStatusCode) ok = false;
            }
            
            // Update every content
            string base_url = "https://listingdb.azurewebsites.net/api/ListContent/";
            int idx = 0;
            foreach (var list_content in temp_items)
            {
                string url = base_url + list_content.Id.ToString() + "?content=" + list_content.Content;

                HttpClient client = new HttpClient();
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);

                if (!response.IsSuccessStatusCode) ok = false;
            }

            if (!ok) await DisplayAlert("Info","Some elements couldn't be saved. Remember that they can't be empty","Ok");
            else await DisplayAlert("Info","Everything was save correctly.","Ok");
        }

        private async void Btn_add_content_Clicked(object sender, EventArgs e)
        {
            if(txt_list_content.Text is null || txt_list_content.Text.Length == 0)
            {
                await DisplayAlert("Warning","The content can't be empty","Ok");
                return;
            }
            ListContent list_content = new ListContent
            {
                ListId = list.Id,
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

            list_content.Id = int.Parse(response.Content.ReadAsStringAsync().Result);
            temp_items.Add(list_content);
            ListContentView.ItemsSource = temp_items;

            txt_list_content.Text = "";
            txt_list_content.Focus();
        }

        protected async void DeleteButtonClicked(object sender, EventArgs e)
        {
            bool ok = await DisplayAlert("Warning", "Are you sure?", "Yes", "No");
            if (!ok) return;

            var item = (sender as Button).BindingContext as ListContent;

            var token = await SecureStorage.GetAsync("token");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string url = "https://listingdb.azurewebsites.net/api/ListContent/" + item.Id.ToString();
            var resultado = await client.DeleteAsync(url);

            if (!resultado.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "An unexpected error ocurred. Please try again.", "OK");
            }

            showContent();
        }

            public static void InitEditPage(CompleteUser lu, string un, ListModel l)
        {
            login_user = lu;
            username = un;
            list = l;
        }

        private async void showContent()
        {
            
            // Content of the list
            string url = "https://listingdb.azurewebsites.net/api/ListContent/ListID/" + list.Id.ToString();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                List<ListContent> list_list_content = new List<ListContent>();
                list_list_content = JsonConvert.DeserializeObject<List<ListContent>>(result);

                temp_items = new ObservableCollection<ListContent>(list_list_content);
                ListContentView.ItemsSource = temp_items;
            }
        }
    }
}