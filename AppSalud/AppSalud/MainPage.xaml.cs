using AppSalud.NewFolder1;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppSalud
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            btn_entrar.Clicked += Btn_entrar_Clicked;
        }

        private void Btn_entrar_Clicked(object sender, EventArgs e)
        {
            if (txt_nombre.Text==null || txt_nombre.Text.Length == 0)
            {
                DisplayAlert("Error","Ingrese su nombre","Ok");
            }
            else
            {
                Navigation.PushAsync(new Page1(txt_nombre.Text.ToString()));
            }
            
        }
    }
}
