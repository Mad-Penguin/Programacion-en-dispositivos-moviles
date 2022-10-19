using AppSalud.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSalud.NewFolder1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private HttpClient client = new HttpClient();
        public Page1(string nombre)
        {
            InitializeComponent();
            lbl_nombre.Text = nombre;
            btn_imc.Clicked += Btn_imc_Clicked;
        }

        private async void Btn_imc_Clicked(object sender, EventArgs e)
        {
            if(txt_edad.Text==null || txt_edad.Text.Length==0 ||
               txt_altura.Text == null || txt_altura.Text.Length == 0 ||
               txt_peso.Text == null || txt_peso.Text.Length == 0)
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios", "Ok");
                return;
            }

            var token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = "https://saludjose.azurewebsites.net/api/salud";
            url = "https://saludomar.azurewebsites.net/api/salud";

            IMC_Model imc = new IMC_Model();
            imc.Peso = float.Parse(txt_peso.Text);
            imc.Altura = float.Parse(txt_altura.Text);
            imc.ImcImc = imc.Peso / (imc.Altura * imc.Altura);

            string imc_json = JsonConvert.SerializeObject(imc);
            StringContent content = new StringContent(imc_json, Encoding.UTF8, "application/json");

            var respuesta = await client.PostAsync(url, content);
            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Ok","Registro guardado","Ok");
            }
            else
            {
                await DisplayAlert("Error", "Ocurrio un error al hacer el registro", "Ok");
            }

            Persona persona = new Persona();
            persona.Nombre = lbl_nombre.Text;
            persona.Edad =  int.Parse(txt_edad.Text);
            persona.Altura = float.Parse(txt_altura.Text);
            persona.Peso = float.Parse(txt_peso.Text);

            await Navigation.PushAsync(new IMCPage(persona));
        }
    }
}