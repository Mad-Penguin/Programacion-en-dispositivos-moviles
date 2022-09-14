using AppSalud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSalud.NewFolder1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1(string nombre)
        {
            InitializeComponent();
            lbl_nombre.Text = nombre;
            btn_imc.Clicked += Btn_imc_Clicked;
        }

        private void Btn_imc_Clicked(object sender, EventArgs e)
        {
            if(txt_edad.Text==null || txt_edad.Text.Length==0 ||
               txt_altura.Text == null || txt_altura.Text.Length == 0 ||
               txt_peso.Text == null || txt_peso.Text.Length == 0)
            {
                DisplayAlert("Error", "Todos los campos son obligatorios", "Ok");
                return;
            }

            Persona persona = new Persona();
            persona.Nombre = lbl_nombre.Text;
            persona.Edad =  int.Parse(txt_edad.Text);
            persona.Altura = float.Parse(txt_altura.Text);
            persona.Peso = float.Parse(txt_peso.Text);

            Navigation.PushAsync(new IMCPage(persona));
        }
    }
}