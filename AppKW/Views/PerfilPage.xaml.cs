using AppKW.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilPage : ContentPage
    {
        AuthenticationService authenticationService = new AuthenticationService();
        public PerfilPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Models.User userData = await authenticationService.GetUserDataFromStorage();

            if (userData != null)
            {
                Console.WriteLine($"Name: {userData.name}");
                Console.WriteLine($"Lastname: {userData.lastname}");
                Console.WriteLine($"Email: {userData.email}");
                Console.WriteLine($"Uid: {userData.uid}");
                Console.WriteLine($"Role: {userData.role}");

                TxtName.Text = userData.name;
                TxtLastname.Text = userData.lastname;
                TxtEmail.Text = userData.email;
                TxtUid.Text = userData.uid;
                TxtRol.Text = userData.role;
            }
        }
    }
}