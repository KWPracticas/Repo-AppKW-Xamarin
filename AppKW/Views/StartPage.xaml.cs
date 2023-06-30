using AppKW.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        AuthenticationService authenticationService = new AuthenticationService();
        public StartPage()
        {
            InitializeComponent();
        }

        private async void Login(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        private async void Register(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Registro());
        }

        private async void Guest(object sender, EventArgs e)
        {
            try
            {
                await authenticationService.LoginAsAGuest();

                string role = await authenticationService.GetUserRoleInStorage();

                if (role != null && role == "Guest")
                {
                    MessagingCenter.Send(this, "isGuest");
                    await Shell.Current.GoToAsync($"//{nameof(Inicio)}");
                } else
                {
                    await DisplayAlert("Error", "Algo salió mal, inténtalo más tarde", "Aceptar");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Algo salió mal, inténtalo más tarde", "Aceptar");
            }
        }
    }
}
