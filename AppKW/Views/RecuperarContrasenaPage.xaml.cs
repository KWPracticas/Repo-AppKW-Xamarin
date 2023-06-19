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
            string correo = TxtEmail.Text;
            if (string.IsNullOrEmpty(correo))
            {
                await DisplayAlert("Mensaje", "Por favor introdusca correo electronico valido", "Ok");
                return;
            }

            try
            {
                await _usuarioRepositorio.resetPassword(correo.Trim());
                await DisplayAlert("Restaurar Contraseña", "Se envio elance a correo electronico para recuperar contraseña", "Listo");
                Clear();
            } catch (Exception ex)
            {
                await DisplayAlert("Restaurar coontraseña", "El link fallo", "Ok");
            }
        }

        public void Clear()
        {
            TxtEmail.Text = string.Empty;
        }
    }
}