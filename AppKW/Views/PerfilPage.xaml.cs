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

        /* public async void Editar(string id)
         {
             var usuario = await _usuarioRepositorio.GetById(id);
             if (usuario == null)
             {
                 await DisplayAlert("Warning", "Data not found.", "Ok");
             }
             usuario.Id = id;
             usuario.nombre = TxtNombrePerfil.Text;

         } */

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            FirebaseAuthLink userData = await _usuarioRepositorio.getDataUser();

            Dictionary<string, Models.User> user = await getUserData(userData.User.LocalId);

            if (user != null)
            {
                foreach (var entry in user)
                {
                    string clave = entry.Key;
                    Models.User usuario = entry.Value;

                    string uid = usuario.Uid;
                    string apellido = usuario.Apellido;
                    string correo = usuario.Correo;
                    string nombre = usuario.Nombre;

                    Console.WriteLine("Clave: " + clave);
                    Console.WriteLine("UID: " + uid);
                    Console.WriteLine("Apellido: " + apellido);
                    Console.WriteLine("Correo: " + correo);
                    Console.WriteLine("Nombre: " + nombre);

                    TxtNombrePerfil.Text = nombre;
                    TxtcorreoPerfil.Text = correo;
                    TxtApellidoPerfil.Text = apellido;
                }
            }
            /*
            string key = await SecureStorage.GetAsync("key");

            var user = await _usuarioRepositorio.getUserById(key);
            Console.WriteLine("DATA: " + user);

            FirebaseAuthLink dataUser = await _usuarioRepositorio.getDataUser();

            if (dataUser != null)
            {
                string name = dataUser.User.DisplayName;
                string email = dataUser.User.Email;
                string Uid = dataUser.User.LocalId;

                TxtNombrePerfil.Text = name;
                TxtcorreoPerfil.Text = email;
                TxtApellidoPerfil.Text = user.apellido;
            }
            */
        }

        public async void cerrarSesion(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();
            await Navigation.PushAsync(new StartPage());
        }

        private async Task<Dictionary<string, Models.User>> getUserData(string uid)
        {
            try
            {
                string url = $"https://appkw-67b39-default-rtdb.firebaseio.com/users.json?orderBy=\"Uid\"&equalTo=\"{uid}\"";

                HttpClient httpClient = new HttpClient();
                string response = await httpClient.GetStringAsync(url);

                Dictionary<string, Models.User> respuesta = JsonConvert.DeserializeObject<Dictionary<string, Models.User>>(response);

                return respuesta;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return null;
        }
    }
}