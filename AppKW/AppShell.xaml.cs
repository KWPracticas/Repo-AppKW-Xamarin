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
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
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
            MessagingCenter.Send<AppShell>(this,
                (role == "User") ? "User" : "invitado"
            );
            Console.WriteLine("role: " + role);
        }




        /* public async void getRole()
         {
             string role = await SecureStorage.GetAsync("role");

             if(!string.IsNullOrEmpty(role) && role == "Employee")
             {
                 Empleados.IsVisible= true;
             }

             Console.WriteLine("role: " + role);
         } */

    }
}
