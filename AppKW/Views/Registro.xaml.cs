﻿using Acr.UserDialogs;
using AppKW.Services;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        AuthenticationService authenticationService = new AuthenticationService();
        public Registro()
        {
            InitializeComponent();
        }
        public async void Register(object sender, EventArgs e)
        {
            bool isValid = await validateRegister(TxtName.Text, TxtLastname.Text, TxtEmail.Text, TxtPassword.Text, TxtConfirmPassword.Text);

            if (isValid)
            {
                string name = TxtName.Text.Trim();
                string lastname = TxtLastname.Text.Trim();
                string email = TxtEmail.Text.Trim();
                string password = TxtPassword.Text.Trim();

                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando...");
                    FirebaseAuthLink newUser = await authenticationService.Register(email, password, name, lastname);
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert(Title, "Usuario registrado correctamente, revisa tu email para validarlo", "Aceptar");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                } catch (Exception ex)
                {
                    if (ex.Message.Contains("EMAIL_EXISTS"))
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Error", "Ya existe una cuenta con ese correo electrónico", "Aceptar");
                    }
                    else if (ex.Message.Contains("INVALID_EMAIL"))
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Error", "Correo electrónico invalido", "Aceptar");
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        Console.WriteLine("Error register" + ex.Message);
                        await DisplayAlert("Error", "Algo salió mal, inténtalo más tarde", "Aceptar");
                    }
                }
            }
        }
        private void imageButtonFacebook(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://www.facebook.com/kenworthdeleste"));
        }
        private void imageButtonTwitter(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://twitter.com/KenworthdelEste"));
        }        
        private async Task<bool> validateRegister(string name, string lastname, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Error", "Ingresa tu nombre", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(lastname))
            {
                await DisplayAlert("Error", "Ingresa tu apellido", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Error", "Ingresa una dirección de correo electrónico", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Ingresa una contraseña", "Aceptar");
                return false;
            }

            if (password.Length < 8)
            {
                await DisplayAlert("Error", "La contraseña debe ser igual o mayor a 8 caracteres", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(confirmPassword))
            {
                await DisplayAlert("Error", "Confirma tu contraseña", "Aceptar");
                return false;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden. Vuelve a intentarlo", "Aceptar");
                return false;
            }

            return true;
        }
    }
}
