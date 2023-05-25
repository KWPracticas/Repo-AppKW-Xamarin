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
    public partial class RecuperarContrasenaPage : ContentPage
    {
        UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        public RecuperarContrasenaPage()
        {
            InitializeComponent();
        }

        public async void Button_SendLink(object sender, EventArgs e)
        {
            try
            {
                string correo = TxtEmail.Text;

                if(string.IsNullOrEmpty(correo))
                {
                    await DisplayAlert("Error", "Escribe una dirección de correo electrónico", "Aceptar");
                    return;
                }

                await _usuarioRepositorio.ResetPassword(correo.Trim());
                await DisplayAlert("Restaurar contraseña", "Enviamos un enlace a tu correo electrónico para restablecer tu cuenta", "Aceptar");
                Clear();
            }
            catch 
            {
                await DisplayAlert("Error", "Algo salió mal, inténtalo más tarde", "Aceptar");
            }
        }

        public void Clear()
        {
            TxtEmail.Text = string.Empty;
        }
    }
}