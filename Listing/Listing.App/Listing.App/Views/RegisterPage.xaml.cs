using Listing.App.Models;
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
    public partial class RegisterPage : ContentPage
    {
        private static string username, email, password;
        public RegisterPage()
        {
            InitializeComponent();

            txt_username.Text = username;
            txt_email.Text = email;
            txt_password.Text = password;

            txt_age.Text = "";
            txt_name.Text = "";
            txt_last_name.Text = "";
            txt_url_image.Text = "";
            txt_institution.Text = "";

            btn_register.Clicked += Btn_register_Clicked;
        }

        private async void Btn_register_Clicked(object sender, EventArgs e)
        {
            if(txt_username.Text is null || txt_password.Text is null || txt_email.Text is null){
                await DisplayAlert("Info", "You need to enter an username, email and password","Ok");
                return;
            }
            User usuario = new User
            {
                UserName = txt_username.Text,
                Password = txt_password.Text,
                Email = txt_email.Text
            };

            CompleteUser new_user = new CompleteUser
            {
                Name = txt_name.Text is null ? "" : txt_name.Text,
                LastName = txt_last_name.Text is null ? "" : txt_last_name.Text,
                Age = int.Parse(txt_age.Text is null ? "0" : txt_age.Text),
                Email = txt_email.Text,
                UrlImage = txt_url_image.Text is null ? "" : txt_url_image.Text,
                Institution = txt_institution.Text is null ? "" : txt_institution.Text
            };

            string json = JsonConvert.SerializeObject(usuario);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            string url = "https://listingdb.azurewebsites.net/api/Account/registrar";

            HttpClient client = new HttpClient();
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                json = JsonConvert.SerializeObject(new_user);
                content = new StringContent(json, Encoding.UTF8, "application/json");
                url = "https://listingdb.azurewebsites.net/api/User";

                response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {

                    await DisplayAlert("Info", "Your account has been registered", "Ok");

                    AccountPage.InitAccountPage(new_user, usuario.UserName);
                    HomePage.InitHomePage(new_user);
                    ListContentPage.InitListContentPage(new_user);
                    FavoritesPage.InitFavoritesPage(new_user);

                    json = JsonConvert.SerializeObject(usuario);
                    content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                    url = "https://listingdb.azurewebsites.net/api/Account/login";

                    response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        Token token = new Token();
                        token = JsonConvert.DeserializeObject<Token>(result);

                        string curr_token = token.TokenToken.ToString();
                        await SecureStorage.SetAsync("token", curr_token);

                        await Shell.Current.GoToAsync("//HomePage");
                    }
                }
                else
                {
                    // Delete from asp net user
                    await DisplayAlert("Error", "Please double check your data", "Ok");
                }

               
            }
            else
            {
                await DisplayAlert("Error", "Try with a different username or a valid email", "Ok");
            }
        }

        public static void InitRegisterPage(string u, string e, string p)
        {
            username = u;
            email = e;
            password = p;
        }
    }
}