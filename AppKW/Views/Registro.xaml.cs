using AppKW.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using String = System.String;

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
            bool isValidForm = await validateRegister(TxtName.Text, TxtLastName.Text, TxtEmail.Text, TxtPassword.Text, TxtConfirmPass.Text);

            if (isValidForm)
            {
                try
                {
                    await _userRepository.register(TxtName.Text.Trim(), TxtLastName.Text.Trim(), TxtEmail.Text.Trim(), TxtPassword.Text.Trim());
                    await DisplayAlert("Registro exitoso", "Se envió un enlace de verificación a tu correo", "Aceptar");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                catch (System.Exception ex)
                {
                    if (ex.Message.Contains("EMAIL_EXISTS"))
                    {
                        await DisplayAlert("Error", "Este nombre de usuario ya está en uso", "Aceptar");
                        clearForm();
                    }
                    else
                    {
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
        private void clearForm()
        {
            TxtName.Text = string.Empty;
            TxtLastName.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtPassword.Text = string.Empty;
            TxtConfirmPass.Text = string.Empty;
        }
        private async Task<bool> validateRegister(string name, string lastname, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(name))
            {
                await DisplayAlert("Error", "Ingresa tu nombre", "Aceptar");
                return false;
            }

            if (String.IsNullOrEmpty(lastname))
            {
                await DisplayAlert("Error", "Ingresa tu apellido", "Aceptar");
                return false;
            }

            if (String.IsNullOrEmpty(email))
            {
                await DisplayAlert("Error", "Ingresa una dirección de correo electrónico", "Aceptar");
                return false;
            }

            if (String.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Ingresa una contraseña", "Aceptar");
                return false;
            }

            if (password.Length < 8)
            {
                await DisplayAlert("Error", "La contraseña debe ser igual o mayor a 8 caracteres", "Aceptar");
                return false;
            }

            if (String.IsNullOrEmpty(confirmPassword))
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
