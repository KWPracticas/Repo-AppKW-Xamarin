using AppKW.Models;
using AppKW.ViewModels;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Media.Session.MediaSession;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilPage : ContentPage
    {
        UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        public PerfilPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Models.User user = await getDataUserWithFirebase();
            TxtNombrePerfil.Text = user.name;
            TxtApellidoPerfil.Text = user.lastname;
            TxtcorreoPerfil.Text = user.email;
            Console.WriteLine("User: " + user);
        }

        public async void btnSignOut(object sender, EventArgs e)
        {
            bool result = _usuarioRepositorio.signOut();
            if (result)
            {
                await Navigation.PushAsync(new StartPage());
            }
        }

        // Obtener datos del usuario desde firebase
        private async Task<Models.User> getDataUserWithFirebase()
        {
            string result = await _usuarioRepositorio.getDataUserWithFirebase();

            if(string.IsNullOrEmpty(result))
            {
                Console.WriteLine("Error");
            } else
            {
                Console.WriteLine("User: " + result);
                Dictionary<string, Models.User> jsonObject = JsonConvert.DeserializeObject<Dictionary<string, Models.User>>(result);
                Models.User user = jsonObject.Values.FirstOrDefault();

                if (user != null)
                {
                    Console.WriteLine($"Email: {user.email}");
                    Console.WriteLine($"Lastname: {user.lastname}");
                    Console.WriteLine($"Name: {user.name}");
                    Console.WriteLine($"Uid: {user.uid}");
                    return user;
                }

                return null;
            }

            return null;
        }
    }
}
