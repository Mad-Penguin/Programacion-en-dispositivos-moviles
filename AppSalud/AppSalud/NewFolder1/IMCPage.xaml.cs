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
    public partial class IMCPage : ContentPage
    {
        public IMCPage(Persona persona)
        {
            InitializeComponent();
            btn_recomendaciones.IsEnabled = false;

            var IMC = persona.Peso / (persona.Altura * persona.Altura);

            if(IMC < 18.5)
            {
                lbl_imc.Text = "Peso bajo";
                lbl_imc.BackgroundColor = Color.Yellow;
                img_imc.Source = new Uri("https://www.pngitem.com/pimgs/m/663-6631998_skinny-man-cartoon-group-with-81-items-tall.png");
            }else if(IMC < 24.9)
            {
                lbl_imc.Text = "Peso normal";
                lbl_imc.BackgroundColor = Color.LightGreen;
                img_imc.Source = new Uri("https://www.pngitem.com/pimgs/m/99-993274_normal-weight-cartoon-man-hd-png-download.png");
            }else if (IMC < 29.9)
            {
                lbl_imc.Text = "Sobre peso";
                lbl_imc.BackgroundColor = Color.Blue;
                img_imc.Source = new Uri("https://hifasdaterra.com/wp-content/uploads/2019/06/Grasa_abdominal.png");
            }else if (IMC < 34.9)
            {
                lbl_imc.Text = "Obesidad";
                lbl_imc.BackgroundColor = Color.Orange;
                img_imc.Source = new Uri("https://static.wikia.nocookie.net/simpsons/images/2/20/King-Size_Homer_Tapped_Out.png");
            }
            else
            {
                lbl_imc.Text = "Obesidad extrema";
                lbl_imc.BackgroundColor = Color.DarkSalmon;
                img_imc.Source = new Uri("https://papik.pro/en/uploads/posts/2022-06/1656167775_2-papik-pro-p-funny-drawings-fat-man-2.png");
            }

            btn_recomendaciones.IsEnabled = true;
            btn_recomendaciones.Clicked += Btn_recomendaciones_Clicked;
        }

        private void Btn_recomendaciones_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecomendacionesPage(lbl_imc.Text));
        }
    }
}