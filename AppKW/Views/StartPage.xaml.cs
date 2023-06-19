using AppKW.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        UsuarioRepositorio _userRepository = new UsuarioRepositorio();
        public StartPage()
        {
            InitializeComponent();
        }

        private async void Login_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void Register_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }

        private async void Guest_Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Inicio)}");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await validateSession();
        }

        private async Task validateSession()
        {
            try
            {
                bool isValid = await _userRepository.validateToken();

                if (isValid)
                {
                    string role = await _userRepository.getRole();
                    
                    await assignRole(role);
                    
                    Console.WriteLine("Token válido");
                }
                else
                {
                    await _userRepository.signInWithRefreshToken();

                    string role = await _userRepository.getRole();

                    await assignRole(role);

                    Console.WriteLine("Refresh token válido");
                }

            } catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                await DisplayAlert("Error", $"{ex.Message}", "Aceptar");
            }
        }

        private async Task assignRole(string role)
        {
            MessagingCenter.Send<StartPage>(this,
                    (role == "User") ? "User" : "Invitado"
                );

            MessagingCenter.Send<StartPage>(this,
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
        }
    }
}
