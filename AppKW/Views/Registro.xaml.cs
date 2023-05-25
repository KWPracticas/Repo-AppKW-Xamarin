
using AppKW.Models;
using AppKW.ViewModels;
using Firebase.Auth;
using Java.Net;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        UsuarioRepositorio _userRepository = new UsuarioRepositorio();
        public Registro()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
        }

        //Registrar un nuevo usuario
        public async void btnRegister(object sender, EventArgs e)
        {
            try
            {
                string nombre = TxtName.Text;
                string apellido = TxtLastName.Text;
                string correo = TxtEmail.Text;
                string contrasena = TxtPassword.Text;
                string confirmarcontrasena = TxtConfirmPass.Text;

                // Validaciones
                if (String.IsNullOrEmpty(nombre))
                {
                    await DisplayAlert("Error", "Escribe tu nombre", "Aceptar");
                    return;
                }
                if (String.IsNullOrEmpty(apellido))
                {
                    await DisplayAlert("Error", "Escribe tu apellido", "Aceptar");
                    return;
                }
                if (String.IsNullOrEmpty(correo))
                {
                    await DisplayAlert("Error", "Escribe tu correo electrónico", "Aceptar");
                    return;
                }
                if (String.IsNullOrEmpty(contrasena))
                {
                    await DisplayAlert("Error", "Escribe una contraseña", "Aceptar");
                    return;
                }
                if (contrasena.Length < 8)
                {
                    await DisplayAlert("Error", "La contraseña debe ser de al menos 8 caracteres", "Aceptar");
                    return;
                }
                if (String.IsNullOrEmpty(confirmarcontrasena))
                {
                    await DisplayAlert("Error", "Confirma tu contraseña", "Aceptar");
                    return;
                }
                if (contrasena != confirmarcontrasena)
                {
                    await DisplayAlert("Error", "Las contraseñas no coinciden", "Aceptar");
                    return;
                }

                await _userRepository.Register(correo.Trim(), contrasena.Trim(), nombre.Trim());

                await DisplayAlert("Registro de usuario", "Se envió un enlace de verificación a su correo electrónico", "Aceptar");
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Error", "Ya existe una cuenta creada con esa dirección de correo electrónico", "Aceptar");
                } else
                {
                    await DisplayAlert("Error", "Algo salió mal, inténtalo más tarde", "Aceptar");
                }
            }
        } 

        public void ImageButtonFacebook(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://www.facebook.com/kenworthdeleste"));
        }

        private void ImageButtonTwitter(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://twitter.com/KenworthdelEste"));
        }
    }
}