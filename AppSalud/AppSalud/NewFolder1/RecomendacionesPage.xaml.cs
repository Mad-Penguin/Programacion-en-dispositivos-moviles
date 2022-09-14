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
    public partial class RecomendacionesPage : ContentPage
    {
        public RecomendacionesPage(string resultado)
        {
            InitializeComponent();
            lbl_condicion.Text = resultado;

            if (resultado == "Peso bajo")
            {
                lbl_recomendacion.Text = "Si tienes bajo peso, puedes sentirte satisfecho más rápido. Consume de cinco a seis comidas más pequeñas durante el día en lugar de dos o tres comidas grandes. Como parte de una dieta saludable en general, elige panes, pastas y cereales integrales, frutas y vegetales, productos lácteos, fuentes de proteínas magra y nueces y semillas. El ejercicio, especialmente el fortalecimiento muscular, puede ayudarte a aumentar de peso al fortalecer los músculos. El ejercicio también puede estimular tu apetito.";
                lbl_mensaje.Text = "Aunque estar delgado a menudo puede ser saludable, tener bajo peso puede ser una preocupación si es el resultado de una mala nutrición o si estás embarazada o tienes otros problemas de salud. Por lo tanto, si tienes bajo peso, consulta a tu médico o dietista para hacerte una evaluación. Juntos, pueden planificar cómo llegar a tu peso ideal.";
            }
            if (resultado == "Peso normal")
            {
                lbl_recomendacion.Text = "Comparta la información del peso saludable con sus familiares. Promueva planes y actividades recreativas que permitan aumentar la actividad física   en familia: paseos, caminatas y ciclovías. Motive a sus hijos a mantenerse hidratados, promueva el consumo de agua. Evite los refrescos y las bebidas azucaradas. Participe en la compra y preparación de los alimentos.";
                lbl_mensaje.Text = "¡Sigue así!, recuerda que estar saludable es indispensable para una vida feliz";
            }
            if (resultado == "Sobre peso")
            {
                lbl_recomendacion.Text = "Evitar el consumo excesivo de frituras. Consumir agua(6 u 8 vasos de agua al día). Evitar el consumo de alimentos ultra procesados, altos en sodio, azúcar, grasas saturadas y que contengan grasas trans(pueden ayudarse del etiquetado octagonal). Realizar actividad física. Mejorar las rutinas y planificar las comidas. Evitar salir de casa si no es necesario y, de hacerlo, utilizar mascarilla y evitar contacto con otras personas. Consultar al especialista para revisar el caso particular si aún no se ha hecho; pueden ser consultas por videollamadas para comenzar.";
                lbl_mensaje.Text = "Es importante saber que el sobrepeso y la obesidad producen cambios negativos directamente en nuestro sistema inmune. Este problema significa tener grasa en exceso y provoca que la persona se encuentre en un estado inflamatorio constante, es decir, el sistema inmune lejos de actuar como “defensa” genera elementos (citoquinas) que favorecen la inflamación";
            }
            if (resultado == "Obesidad")
            {
                lbl_recomendacion.Text = "El tratamiento debe ser integral y garantizar a largo plazo y de forma progresiva una pérdida de peso, teniendo en cuenta todos los factores implicados: grado de sobrepeso, tipo de alimentación, nivel de actividad física, motivación para cumplir el tratamiento, etc. Mantenga un ritmo de ingesta a lo largo del día para llegar saciado a las comidas principales. Planifique las comidas y cenas con antelación para que siempre disponga de alimentos saludables listos para consumir. Beba agua diariamente y no la sustituya por refrescos aunque sean light o zero. Realice ejercicio físico con una frecuencia mínima de 3 veces a la semana.";
                lbl_mensaje.Text = "La obesidad es una enfermedad crónica de origen multifactorial causante, por sí misma, de numerosas complicaciones como la diabetes, la hipertensión o la dislipemia. No es un problema estético, sino de salud.";
            }
            if (resultado == "Obesidad extrema")
            {
                lbl_recomendacion.Text = "Cambie su dieta. Puede que se le remita a un dietista para que lo ayude o la ayude con un plan para bajar medio o un kilo por semana. Para bajar de peso, debe reducir el número de calorías que consume. Considere la posibilidad de añadir actividad física después de alcanzar un objetivo mínimo de bajar un 10% de su peso corporal. Medicación. Algunas personas se pueden beneficiar de la medicación para facilitar la pérdida de peso en caso de obesidad extrema. Tenga en cuenta que la medicación puede ser cara y tener efectos secundarios.";
                lbl_mensaje.Text = "Si tiene obesidad extrema, bajar de peso puede equivaler a “menos cardiopatías, menos diabetes y menos cáncer”, apunta Robert Eckel, M.D. y antiguo presidente de la American Heart Association. “Las mejoras metabólicas se comienzan a registrar cuando las personas con obesidad extrema pierden aproximadamente el 10% de su peso corporal”. ";
            }
        }
    }
}