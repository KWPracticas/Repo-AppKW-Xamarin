using AppKW.Models;
using AppKW.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contacto : ContentPage
    {
        public Contacto()
        {
            InitializeComponent();
        }

        public async void SendData_Clicked(object sender, EventArgs e)
        {
            Contact contactForm = new Contact
            {
                name = TxtNombre.Text.Trim(),
                lastname= TxtApellido.Text.Trim(),
                phone_number= TxtTelefono.Text.Trim(),
                email= TxtCorreo.Text.Trim(),
                interested_in = TxtInteresado.SelectedItem.ToString(),
                company = TxtCompania.Text.Trim(),
                description = TxtDescripcion.Text.Trim(),
                type = TxtPersona.SelectedItem.ToString(),
            };

            try
            {
                //Validaciones
                if (System.String.IsNullOrEmpty(TxtNombre.Text))
                {
                    await DisplayAlert("Advertencia", "Este dato es requerido, ingresa un nombre.", "Ok");
                    return;
                }
                if (System.String.IsNullOrEmpty(TxtApellido.Text))
                {
                    await DisplayAlert("Advertencia", "Este dato es requerido, ingresa un apellido.", "Ok");
                    return;
                }
                if (System.String.IsNullOrEmpty(TxtTelefono.Text))
                {
                    await DisplayAlert("Advertencia", "Este dato es requerido, ingresa un número de teléfono.", "Ok");
                    return;
                }
                if (System.String.IsNullOrEmpty(TxtCorreo.Text))
                {
                    await DisplayAlert("Advertencia", "Este dato es requerido, ingresa un correo electrónico válido.", "Ok");
                    return;
                }
                if (System.String.IsNullOrEmpty(TxtInteresado.SelectedItem.ToString()))
                {
                    await DisplayAlert("Advertencia", "Este dato es requerido, selecciona en que se encuentra interesado.", "Ok");
                    return;
                }
                if (System.String.IsNullOrEmpty(TxtCompania.Text))
                {
                    await DisplayAlert("Advertencia", "Este dato es requerido, ingresa una compañía.", "Ok");
                    return;
                }
                if (System.String.IsNullOrEmpty(TxtDescripcion.Text))
                {
                    await DisplayAlert("Advertencia", "Este dato es requerido, describe el mensaje.", "Ok");
                    return;
                }
                if (System.String.IsNullOrEmpty(TxtPersona.SelectedItem.ToString()))
                {
                    await DisplayAlert("Advertencia", "Este dato es requerido, selecciona el tipo de persona que eres.", "Ok");
                    return;
                }


                Uri uri = new Uri("http://192.168.1.64:8000/api/contact");
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(contactForm);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");


                var resp = await client.PostAsync(uri, contentJson);
                Console.WriteLine(resp);

                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("Correcto", "Mensaje enviado", "OK");
                    TxtNombre.Text = "";
                    TxtApellido.Text = "";
                    TxtTelefono.Text = "";
                    TxtCorreo.Text = "";
                    TxtCompania.Text = "";
                    TxtDescripcion.Text = "";
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrió un error Servicio", "OK");
                }
            } catch(Exception ex)
            {
                //Console.WriteLine(ex.ToString());
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
