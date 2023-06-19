using AppKW.ViewModels;
using AppKW.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppKW
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("user1", typeof(LoginPage));
            Routing.RegisterRoute("user2", typeof(LoginPage));
            RolInvitado();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void RolInvitado()
        {
            //Validar tipo de usuario 
            string role = await SecureStorage.GetAsync("role");

            if (string.IsNullOrEmpty(role))
            {
                MessagingCenter.Send<AppShell>(this,
                    role = "Invitado"
                );
            }
        }
    }
}
