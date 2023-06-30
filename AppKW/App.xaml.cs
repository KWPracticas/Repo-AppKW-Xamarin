using AppKW.Models;
using AppKW.Services;
using AppKW.Views;
using Java.IO;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Console = System.Console;

namespace AppKW
{
    public partial class App : Application
    {
        AuthenticationService authenticationService = new AuthenticationService();
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();            
        }

        protected override async void OnStart()
        {
            base.OnStart();
            User user = await AuthenticateUser();

            if (user != null)
            {
                if (user.role == "Regular User")
                {
                    MessagingCenter.Send(this, "isRegularUser");
                } else if(user.role == "Employee")
                {
                    MessagingCenter.Send(this, "isEmployee");
                } else if(user.role == "Guest")
                {
                    MessagingCenter.Send(this, "isGuest");
                }

                await Shell.Current.GoToAsync($"//{nameof(Inicio)}");
            } else
            {
                await Shell.Current.GoToAsync($"//{nameof(StartPage)}");
            }
        }

        private async Task<User> AuthenticateUser()
        {
            try
            {
                User user = await authenticationService.GetUserDataFromStorage();
                return user;
            } catch (Exception)
            {
                return null;
            }
            
        }
    }
}