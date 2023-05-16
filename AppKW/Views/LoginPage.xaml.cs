using AppKW.ViewModels;
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
                string correo = TxtEmail.Text.Trim();
                string contrasena = TxtPassword.Text.Trim();
                if (String.IsNullOrEmpty(correo))
                {
                    await DisplayAlert("Advertencia", "introduce tu correo", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(contrasena))
                {
                    await DisplayAlert("Advertencia", "Introduce tu contraseña", "Ok");
                    return;
                }

                
                //Validación de logueo exitoso
                string token = await _usuarioRepositorio.SignIn(correo.Trim(), contrasena.Trim()); //exyZ ""
                                
                if (!string.IsNullOrEmpty(token))
                {
                    //Guardar token en storage
                    await SecureStorage.SetAsync("token", token);

                    //Validar tipo de usuario 
                    string role = await SecureStorage.GetAsync("role");
                    MessagingCenter.Send<LoginPage>(this,
                        (role == "User") ? "User" : "invitado"
                    );
                    MessagingCenter.Send<LoginPage>(this,
                        (role == "Employee") ? "Employee" : "User"
                    );
                    Console.WriteLine("role: " + role);

                    /* Checkbox
                    bool isChecked = Recordar.IsChecked;

                    if (isChecked)
                    {
                        // await DisplayAlert("info", "checkbox activado", "ok");     
                    }
                    else
                    {
                        await DisplayAlert("info", "checkbox desactivado", "ok");
                    }
                    */

                    //Redireccionar al Home
                    if (role == "User")
                    {
                        await Shell.Current.GoToAsync("//inicio");
                    }
                    else
                    {
                        await Shell.Current.GoToAsync("//empleado");
                    }
                    
                }
                else
                {
                    await DisplayAlert("Inicio de sesión", "Su cuenta no ha sido verificada, revise su correo electrónico", "Ok");
                }
            }
            catch(Exception exception) 
            {
                if (exception.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("No autorizado", "Correo no existente en la base de datos", "Ok");

                }
                else if(exception.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("No autorizado", "Contraseña incorrecta", "Ok");
                }
                else if (exception.Message.Contains("INVALID_EMAIL"))
                {
                    await DisplayAlert("No autorizado", "Este correo electrónico no es válido", "Ok");
                }
                else 
                {
                    await DisplayAlert("Error", exception.Message, "Ok");
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