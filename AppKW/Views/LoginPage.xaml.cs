using AppKW.ViewModels;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        public async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            bool isValidForm = await validateLogin(TxtEmail.Text, TxtPassword.Text);
            if (isValidForm)
            {
                try
                {
                    FirebaseAuthLink result = await _usuarioRepositorio.signIn(TxtEmail.Text.Trim(), TxtPassword.Text.Trim());

                    if (result != null)
                    {
                        string role = await _usuarioRepositorio.getRole();

                        MessagingCenter.Send<LoginPage>(this,
                            (role == "User") ? "User" : "Invitado"
                        );
                        MessagingCenter.Send<LoginPage>(this,
                            (role == "Employee") ? "Employee" : "User"
                        );

                        if (role == "User")
                        {
                            await Shell.Current.GoToAsync("//inicio");
                        }

                        if (role == "Employee")
                        {
                            await Shell.Current.GoToAsync("//empleado");
                        }

                    } else
                    {
                        await DisplayAlert("Error", "El correo electrónico no se ha verificado, revisa tu bandeja de entrada o la bandeja de spam para validarlo", "Aceptar");
                    }
                } catch(System.Exception ex)
                {
                    if (ex.Message.Contains("EMAIL_NOT_FOUND"))
                    {
                        await DisplayAlert("Error", "No pudimos encontrar tu cuenta", "Aceptar");
                    }
                    else if (ex.Message.Contains("INVALID_PASSWORD"))
                    {
                        await DisplayAlert("Error", "La contraseña es incorrecta. Vuelve a intentarlo o haz clic en \"¿Olvidaste la contraseña?\" para restablecerla.\r\n", "Aceptar");
                    }
                    else if (ex.Message.Contains("INVALID_EMAIL"))
                    {
                        await DisplayAlert("Error", "Ingresa un correo electrónico válido", "Aceptar");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Algo salió mal, inténtalo más tarde", "Aceptar");
                    }
                }
            }
            
            
        }
        public async void RegisterTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Registro());
        }
        public async void ForgotTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RecuperarContrasenaPage());
        }
        private async Task<bool> validateLogin(string email, string password)
        {
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

            return true;
        }
    }
}