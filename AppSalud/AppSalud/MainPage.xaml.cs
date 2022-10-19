using AppSalud.Models;
using AppSalud.NewFolder1;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppSalud
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public MainPage()
        {
            InitializeComponent();
            btn_entrar.Clicked += Btn_entrar_Clicked;
            btn_registrar.Clicked += Btn_registrar_Clicked;
        }

        private async void Btn_registrar_Clicked(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario
            {
                username = txt_usuario.Text,
                password = txt_password.Text,
                email = txt_email.Text
            };

            string json = JsonConvert.SerializeObject(usuario);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            string url = "https://saludjose.azurewebsites.net/api/cuentas/registrar";

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Aviso", "Registro exitoso, por favor inicia sesión", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo registrar", "Ok");
            }
        }

        private async void Btn_entrar_Clicked(object sender, EventArgs e)
        {
            //await DisplayAlert("Debug","Entro al click del boton entrar","Ok");
            Usuario usuario = new Usuario{
                username = txt_usuario.Text,
                password = txt_password.Text,
                email = txt_email.Text
            };
            
            string json = JsonConvert.SerializeObject(usuario);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            string url = "https://saludjose.azurewebsites.net/api/cuentas/login";
            url = "https://saludomar.azurewebsites.net/api/cuentas/login";

            HttpClient client2 = new HttpClient();

            var lista = await client2.GetAsync("https://saludomar.azurewebsites.net/api/salud/3");

            var response = await client2.PostAsync(url, content);

            var msj = response.StatusCode;
            var hola = "hola";

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                Token token = new Token();
                token = JsonConvert.DeserializeObject<Token>(result);

                string curr_token = token.TokenToken.ToString();
                await SecureStorage.SetAsync("token", curr_token);

                await Navigation.PushAsync(new Page1(txt_usuario.Text.ToString()));
            }
            else
            {
                await DisplayAlert("Error", "No se pudo iniciar sesión", "Ok");
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
