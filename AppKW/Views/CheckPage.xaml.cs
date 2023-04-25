using AppKW.Models;
using AppKW.Services;
using AppKW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckPage : ContentPage
	{
        Encuestas _encuestasRepository = new Encuestas();
        public CheckPage ()
		{
			InitializeComponent ();
		}

        private async void DataEncuestas_Clicked(object sender, EventArgs e)
        {
            try
            {
                string nombre = TxtNombre.Text;
                string departamento = TxtDepartamento.Text;
                string pregunta1 = TxtP1.SelectedItem.ToString();
                string pregunta2 = TxtP2.SelectedItem.ToString();
                string pregunta3 = TxtP3.SelectedItem.ToString();
                string pregunta4 = TxtP4.SelectedItem.ToString();
                string pregunta5 = TxtP5.SelectedItem.ToString();
                string pregunta6 = TxtP6.SelectedItem.ToString();
                string pregunta7 = TxtP7.SelectedItem.ToString();

                //Validaciones
                if (string.IsNullOrEmpty(nombre))
                {
                    await DisplayAlert("Advertencia", "El campo de nombre esta vacio y es requerido.", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(departamento))
                {
                    await DisplayAlert("Advertencia", "El campo de departamento esta vacio y es requerido.", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(pregunta1))
                {
                    await DisplayAlert("Advertencia", "La pregunta 1 no esta contestada", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(pregunta2))
                {
                    await DisplayAlert("Advertencia", "La pregunta 2 no esta contestada", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(pregunta3))
                {
                    await DisplayAlert("Advertencia", "La pregunta 3 no esta contestada", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(pregunta4))
                {
                    await DisplayAlert("Advertencia", "La pregunta 4 no esta contestada", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(pregunta5))
                {
                    await DisplayAlert("Advertencia", "La pregunta 5 no esta contestada", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(pregunta6))
                {
                    await DisplayAlert("Advertencia", "La pregunta 6 no esta contestada", "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(pregunta7))
                {
                    await DisplayAlert("Advertencia", "La pregunta 7 no esta contestada", "Ok");
                    return;
                }

                //Validar si se agregaron los datos
                EncuestaModel encuestas = new EncuestaModel();
                encuestas.nombre = nombre;
                encuestas.departamento = departamento;
                encuestas.pregunta1 = pregunta1;
                encuestas.pregunta2 = pregunta2;
                encuestas.pregunta3 = pregunta3;
                encuestas.pregunta4 = pregunta4;
                encuestas.pregunta5 = pregunta5;
                encuestas.pregunta6 = pregunta6;
                encuestas.pregunta7 = pregunta7;

                var isSaved = await _encuestasRepository.Save(encuestas);
                if (isSaved)
                {
                    await DisplayAlert("Informacion", "Registro exito de información", "Ok");
                    Clear();
                    //await DisplayAlert("Registro exitoso", "Se envió un enlace de verificación a tu correo.", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "No funciono el registro", "Ok");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error Catch", ex.Message, "Ok");
            }
        }

        public void Clear()
        {
            TxtNombre.Text = string.Empty;
            TxtDepartamento.Text = string.Empty;
            TxtP1.SelectedItem = null;
            TxtP2.SelectedItem = null;
            TxtP3.SelectedItem = null;
            TxtP4.SelectedItem = null;
            TxtP5.SelectedItem = null;
            TxtP6.SelectedItem = null;
            TxtP7.SelectedItem = null;
        }
    }
}