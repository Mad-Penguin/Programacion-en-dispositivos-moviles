using Listing.App.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listing.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginRegisterPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public LoginRegisterPage()
        {
            InitializeComponent();

            txt_username.Text = "";
            txt_email.Text = "";
            txt_password.Text = "";

            btn_login.Clicked += Btn_login_Clicked;
            btn_register.Clicked += Btn_register_Clicked;
        }

        private async void Btn_register_Clicked(object sender, EventArgs e)
        {
            RegisterPage.InitRegisterPage(
                txt_username.Text is null ? "" : txt_username.Text ,
                txt_email.Text is null ? "" : txt_email.Text,
                txt_password.Text is null ? "" : txt_password.Text
            );
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        private async Task<CompleteUser> getCompleteUser(string email)
        {
            string url = "https://listingdb.azurewebsites.net/api/User";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                List<CompleteUser> complete_users = new List<CompleteUser>();
                complete_users = JsonConvert.DeserializeObject< List<CompleteUser> >(result);

                var complete_user = complete_users.FirstOrDefault(x => x.Email == email);
                return complete_user;
            }

            return null;
        }

        private async void Btn_login_Clicked(object sender, EventArgs e)
        {
            User usuario = new User
            {
                UserName = txt_username.Text,
                Password = txt_password.Text,
                Email = txt_email.Text
            };

            string json = JsonConvert.SerializeObject(usuario);
            StringContent content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            string url = "https://listingdb.azurewebsites.net/api/Account/login";

            var response = await _httpClient.PostAsync(url, content);
            var complete_user = await getCompleteUser(usuario.Email);

            if (complete_user is null){
                await DisplayAlert("Error", "Couldn't login. Double check your Username, Password and Email.", "Ok");
                return;
            }

            AccountPage.InitAccountPage(complete_user, usuario.UserName);
            HomePage.InitHomePage(complete_user);
            ListContentPage.InitListContentPage(complete_user);
            FavoritesPage.InitFavoritesPage(complete_user);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                Token token = new Token();
                token = JsonConvert.DeserializeObject<Token>(result);

                string curr_token = token.TokenToken.ToString();
                await SecureStorage.SetAsync("token", curr_token);

                //await Navigation.PushAsync(new AppShell());
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await DisplayAlert("Error", "Couldn't login. Double check your Username, Password and Email.", "Ok");
            }

            /*if (txt_nombre.Text==null || txt_nombre.Text.Length == 0)
            {
                DisplayAlert("Error","Ingrese su nombre","Ok");
            }
            else
            {
                Navigation.PushAsync(new Page1(txt_nombre.Text.ToString()));
            }*/

        }
    }
}