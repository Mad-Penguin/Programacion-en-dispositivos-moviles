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
using Newtonsoft.Json;

namespace Listing.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        private static CompleteUser login_user;
        private static string username;
        private string default_img_url = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png";
        private ObservableCollection<ListModel> temp_items;

        public AccountPage()
        {
            InitializeComponent();

            image_user.Source = new Uri( (login_user.UrlImage is null || login_user.UrlImage.Length==0) ? default_img_url : login_user.UrlImage );
            label_username.Text = username;
            label_institution.Text = login_user.Institution;

            showLists();

            logout_profile.Clicked += Logout_profile_Clicked;
            edit_profile.Clicked += Edit_profile_Clicked;
            delete_profile.Clicked += Delete_profile_Clicked;

            create_list.Clicked += Create_list_Clicked;
        }

        private async void Logout_profile_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginRegisterPage");
        }

        private async void Delete_profile_Clicked(object sender, EventArgs e)
        {
            bool ok = await DisplayAlert("Warning","Are you sure? This action can't be undone and you won't be able to have the same username again.", "Yes", "No");
            if (!ok) return;

            string url = "https://listingdb.azurewebsites.net/api/User/" + login_user.Id;
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Info", "Your account has been deleted.","Ok");
                await Shell.Current.GoToAsync("//LoginRegisterPage");
            }
            else
            {
                await DisplayAlert("Error", "An unexpected error ocurred. Please try again.", "Ok");
            }
        }

        private async void Edit_profile_Clicked(object sender, EventArgs e)
        {
            EditAccountPage.InitEditAccountPage(login_user, username);
            await Shell.Current.GoToAsync($"{nameof(EditAccountPage)}");
        }

        private async void refreshPage()
        {
            string url = "https://listingdb.azurewebsites.net/api/User/Email/" + login_user.Email;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            var result = response.Content.ReadAsStringAsync().Result;
            CompleteUser complete_user = new CompleteUser();
            complete_user = JsonConvert.DeserializeObject<CompleteUser>(result);

            login_user = complete_user;

            image_user.Source = new Uri((login_user.UrlImage is null || login_user.UrlImage.Length == 0) ? default_img_url : login_user.UrlImage);
            label_username.Text = username;
            label_institution.Text = login_user.Institution;
            showLists();
        }

        protected override void OnAppearing()
        {
            refreshPage();
        }

        protected override bool OnBackButtonPressed()
        {
            refreshPage();
            return true;
        }

        protected async void EditButtonClicked(object sender, EventArgs e)
        {
            var item = (sender as Button).BindingContext as ListModel;
            EditListPage.InitEditPage(login_user, username, item);
            await Shell.Current.GoToAsync($"{nameof(EditListPage)}");
        }

        protected async void DeleteButtonClicked(object sender, EventArgs e)
        {
            bool ok = await DisplayAlert("Warning", "Are you sure?", "Yes", "No");
            if (!ok) return;

            var item = (sender as Button).BindingContext as ListModel;

            var token = await SecureStorage.GetAsync("token");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string url = "https://listingdb.azurewebsites.net/api/List/" + item.Id.ToString();
            var resultado = await client.DeleteAsync(url);

            if (!resultado.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "An unexpected error ocurred. Please try again.", "OK");
            }

            showLists();
        }

        private async void Create_list_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(CreateListPage)}");
        }

        public static void InitAccountPage(CompleteUser cu, string un)
        {
            login_user = cu;
            username = un;

            CreateListPage.InitCreateListPage(cu);
        }

        private async void showLists()
        {
            var token = await SecureStorage.GetAsync("token");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string url = "https://listingdb.azurewebsites.net/api/List/OwnerId/" + login_user.Id.ToString();
            var resultado = await client.GetAsync(url);

            if (!resultado.IsSuccessStatusCode) return;

            var resultado_json = resultado.Content.ReadAsStringAsync().Result;
            List<ListModel> lista_registros = new List<ListModel>();
            lista_registros = ListModel.FromJson(resultado_json);

            temp_items = new ObservableCollection<ListModel>(lista_registros);
            AccountList.ItemsSource = temp_items;
            return;
        }

        /*async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;


            await DisplayAlert("Item Tapped", temp_items[e.ItemIndex].Title, "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }*/
    }
}