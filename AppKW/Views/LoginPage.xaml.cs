using AppKW.ViewModels;
using Firebase.Auth;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Icu.Text.AlphabeticIndex;

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
            try
            {
                string correo = TxtEmail.Text;
                string contrasena = TxtPassword.Text;

                if (String.IsNullOrEmpty(correo))
                {
                    await DisplayAlert("Error", "Escribe tu correo electrónico", "Aceptar");
                    return;
                }
                
                if (String.IsNullOrEmpty(contrasena))
                {
                    await DisplayAlert("Error", "Escribe tu contraseña", "Aceptar");
                    return;
                }

                FirebaseAuthLink auth = await _usuarioRepositorio.SignIn(correo.Trim(), contrasena.Trim());
                
                if (auth == null)
                {
                    await DisplayAlert("Error", "Su cuenta no ha sido verificada, revise su correo electrónico", "Aceptar");
                    return;
                }

                string role = await SecureStorage.GetAsync("role");
                MessagingCenter.Send<LoginPage>(this,
                    (role == "User") ? "User" : "Invitado"
                );
                MessagingCenter.Send<LoginPage>(this,
                    (role == "Employee") ? "Employee" : "User"
                );
                Console.WriteLine("role: " + role);

                if (role == "User")
                {
                    await Shell.Current.GoToAsync("//inicio");
                }
                else
                {
                    await Shell.Current.GoToAsync("//empleado");
                }
            }
            catch(Exception exception) 
            {
                if (exception.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("Error", "Verifique su usuario o contraseña", "Aceptar");

                }
                else if(exception.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("No autorizado", "Verifique su usuario o contraseña", "Aceptar");
                }
                else if (exception.Message.Contains("INVALID_EMAIL"))
                {
                    await DisplayAlert("No autorizado", "Escribe una dirección de correo valida", "Aceptar");
                }
                else 
                {
                    await DisplayAlert("Error", exception.Message, "Aceptar");
                }
            }
            
        }

        //Metodo que direcciona a el registro desde el login
        public async void RegisterTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Registro());
        }

        //Metodo de recuperar contraseña 
        public async void ForgotTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RecuperarContrasenaPage());
        }
    }
}