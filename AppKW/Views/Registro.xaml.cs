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



        public async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                string nombre = TxtName.Text;
                string apellido = TxtLastName.Text;
                string correo = TxtEmail.Text;
                string contrasena = TxtPassword.Text;
                string confirmarcontrasena = TxtConfirmPass.Text;
                //Validaciones de formulario de regstro de usuario
                if (String.IsNullOrEmpty(nombre))
                {
                    await DisplayAlert("Advertencia", "Tipo nombre", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(apellido))
                {
                    await DisplayAlert("Advertencia", "Type apellido", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(correo))
                {
                    await DisplayAlert("Advertencia", "Type correo", "Ok");
                    return;
                }
                if (contrasena.Length < 8)
                {
                    await DisplayAlert("Advertencia", "Contraseña mayor a 8 caraceres", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(contrasena))
                {
                    await DisplayAlert("Advertencia", "Type contraseña", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(confirmarcontrasena))
                {
                    await DisplayAlert("Advertencia", "Type confirmar contraseña", "Ok");
                    return;
                }
                if (contrasena != confirmarcontrasena)
                {
                    await DisplayAlert("Advertencia", "La contraseña no coincide", "Ok");
                    return;
                }



                //Validar si el registro se completo o fallo
                bool isSave = await _userRepository.Resgister(nombre.Trim(), apellido.Trim(), correo.Trim(), contrasena.Trim());
                if (isSave)
                {
                    await DisplayAlert("Registro exitoso", "Se envió un enlace de verificación a tu correo.", "Ok");
                    //dirigir al Login  
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                else
                {
                    await DisplayAlert("Resgistro de usuario", "Registro fallido", "Ok");
                    Clear();
                } 
            }
            //Validacion de si el correo exite en la base de datos
            catch (Exception exception)
            {
                if (exception.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Alerta", "Correo ya existente", "Ok");
                    Clear();
                }
                /*if (exception.Message.Contains("INVALID_EMAIL")) 
                {
                    await DisplayAlert("Warning", "Tiene espacios en blanco al escribir el correo", "Ok");
                }*/
                else
                {
                    await DisplayAlert("Error", exception.Message, "Ok");
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

        //Metodo para limpiar
        public void Clear()
        {
            TxtName.Text = string.Empty;
            TxtLastName.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtPassword.Text = string.Empty;
            TxtConfirmPass.Text = string.Empty;
        }
    }
}