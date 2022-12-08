using Listing.App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listing.App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditAccountPage : ContentPage
	{
        private static CompleteUser login_user;
        private static string username;
		public EditAccountPage ()
		{
			InitializeComponent ();

            txt_username.Text = username;
            txt_email.Text = login_user.Email;
            txt_name.Text = login_user.Name;
            txt_last_name.Text = login_user.LastName;
            txt_age.Text = login_user.Age.ToString();
            txt_url_image.Text = login_user.UrlImage;
            txt_institution.Text = login_user.Institution;

            btn_save.Clicked += Btn_save_Clicked;
		}

        private async void Btn_save_Clicked(object sender, EventArgs e)
        {
            CompleteUser edited_user = new CompleteUser
            {
                Name = txt_name.Text,
                LastName = txt_last_name.Text,
                Age = int.Parse(txt_age.Text),
                Email = txt_email.Text,
                UrlImage = txt_url_image.Text,
                Institution = txt_institution.Text
            };

            string json = JsonConvert.SerializeObject(edited_user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            string url = "https://listingdb.azurewebsites.net/api/User/" + login_user.Id.ToString();

            string ch = "?";
            if (!(txt_url_image.Text is null) && txt_url_image.Text.Length > 0)
            {
                url += ch + "url_image=" + txt_url_image.Text;
                ch = "&";
            }
            if (!(txt_institution.Text is null) && txt_institution.Text.Length > 0)
            {
                url += ch + "institution=" + txt_institution.Text;
                ch = "&";
            }


            HttpClient client = new HttpClient();
            var response = await client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Info","Profile updated","Ok");
            }
            else
            {
                await DisplayAlert("Error", "Please double check your data", "Ok");

            }  
        }

        public static void InitEditAccountPage(CompleteUser lu, string un)
        {
            login_user = lu;
            username = un;
        }
    }
}