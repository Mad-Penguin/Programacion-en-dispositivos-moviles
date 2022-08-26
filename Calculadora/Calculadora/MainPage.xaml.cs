using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculadora
{
    public partial class MainPage : ContentPage
    {
        bool already_decimal = false;
        string last_operation = "suma";
        float suma = 0;
        public MainPage()
        {
            
            InitializeComponent();

            boton_0.Clicked += Boton_0_Clicked;
            boton_1.Clicked += Boton_1_Clicked;
            boton_2.Clicked += Boton_2_Clicked;
            boton_3.Clicked += Boton_3_Clicked;
            boton_4.Clicked += Boton_4_Clicked;
            boton_5.Clicked += Boton_5_Clicked;
            boton_6.Clicked += Boton_6_Clicked;
            boton_7.Clicked += Boton_7_Clicked;
            boton_8.Clicked += Boton_8_Clicked;
            boton_9.Clicked += Boton_9_Clicked;

            boton_punto.Clicked += Boton_punto_Clicked;

            boton_borra_ultimo.Clicked += Boton_borra_ultimo_Clicked;
            boton_limpiar.Clicked += Boton_limpiar_Clicked;

            boton_suma.Clicked += Boton_suma_Clicked;
            boton_igual.Clicked += Boton_igual_Clicked;
            boton_resta.Clicked += Boton_resta_Clicked;
        }

        private void UpdateSuma()
        {
            switch (last_operation)
            {
                case "suma":
                    suma += float.Parse(display_resultado.Text);
                    break;
                case "resta":
                    suma -= float.Parse(display_resultado.Text);
                    break;
            }
            Console.WriteLine(suma);
        }

        private void Boton_resta_Clicked(object sender, EventArgs e)
        {
            UpdateSuma();
            display_resultado.Text = "0";
            already_decimal = false;

            last_operation = "resta";
        }

        private void Boton_igual_Clicked(object sender, EventArgs e)
        {
            UpdateSuma();

            display_resultado.Text = "0";
            already_decimal = false;

            display_resultado.Text = suma.ToString();
            suma = 0;

            last_operation = "";
        }

        private void Boton_suma_Clicked(object sender, EventArgs e)
        {
            UpdateSuma();
            display_resultado.Text = "0";
            already_decimal = false;

            last_operation = "suma";
        }

        private void Boton_limpiar_Clicked(object sender, EventArgs e)
        {
            already_decimal = false;
            display_resultado.Text = "0";

            last_operation = "suma";
        }

        private void Boton_borra_ultimo_Clicked(object sender, EventArgs e)
        {
            if (display_resultado.Text == "0") return;
            if (display_resultado.Text.EndsWith(".")) already_decimal = false;

            display_resultado.Text = display_resultado.Text.Remove(display_resultado.Text.Length-1,1);
            if (display_resultado.Text.Length == 0) display_resultado.Text = "0";
        }

        private void Boton_punto_Clicked(object sender, EventArgs e)
        {
            if (!already_decimal)
            {
                display_resultado.Text += ".";
                already_decimal = true;
            }
        }

        private void Boton_0_Clicked(object sender, EventArgs e)
        {
            if (display_resultado.Text != "0")
                display_resultado.Text = display_resultado.Text + "0";
        }

        private void Boton_1_Clicked(object sender, EventArgs e)
        {
            if (display_resultado.Text == "0")
            {
                display_resultado.Text = "1";
            }
            else
            {
                display_resultado.Text = display_resultado.Text + "1";
            }
        }

        private void Boton_2_Clicked(object sender, EventArgs e)
        {
            if (display_resultado.Text == "0")
            {
                display_resultado.Text = "2";
            }
            else
            {
                display_resultado.Text = display_resultado.Text + "2";
            }
        }

        private void Boton_3_Clicked(object sender, EventArgs e)
        {
            if (display_resultado.Text == "0")
            {
                display_resultado.Text = "3";
            }
            else
            {
                display_resultado.Text = display_resultado.Text + "3";
            }
        }

        private void Boton_4_Clicked(object sender, EventArgs e)
        {
            if (display_resultado.Text == "0")
            {
                display_resultado.Text = "4";
            }
            else
            {
                display_resultado.Text = display_resultado.Text + "4";
            }
        }

        private void Boton_5_Clicked(object sender, EventArgs e)
        {
            if (display_resultado.Text == "0")
            {
                display_resultado.Text = "5";
            }
            else
            {
                display_resultado.Text = display_resultado.Text + "5";
            }
        }

        private void Boton_6_Clicked(object sender, EventArgs e)
        {
            if (display_resultado.Text == "0")
            {
                display_resultado.Text = "6";
            }
            else
            {
                display_resultado.Text = display_resultado.Text + "6";
            }
        }

        private void Boton_7_Clicked(object sender, EventArgs e)
        {
            if(display_resultado.Text == "0")
            {
                display_resultado.Text = "7";
            }
            else
            {
                display_resultado.Text = display_resultado.Text + "7";
            }
        }

        private void Boton_8_Clicked(object sender, EventArgs e)
        {
            if (display_resultado.Text == "0")
            {
                display_resultado.Text = "8";
            }
            else
            {
                display_resultado.Text = display_resultado.Text + "8";
            }
        }

        private void Boton_9_Clicked(object sender, EventArgs e)
        {
            if (display_resultado.Text == "0")
            {
                display_resultado.Text = "9";
            }
            else
            {
                display_resultado.Text = display_resultado.Text + "9";
            }
        }
    }
}
