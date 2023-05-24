using AppKW.Models;
using AppKW.ViewModels;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Media.Session.MediaSession;

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

            FirebaseAuthLink dataUser = await _usuarioRepositorio.getDataUser();

            if (dataUser != null)
            {
                string name = dataUser.User.DisplayName;
                string email = dataUser.User.Email;
                string lastname = dataUser.User.LastName;
                string Uid = dataUser.User.LocalId;

                TxtNombrePerfil.Text = name;
                TxtcorreoPerfil.Text = email;
                TxtApellidoPerfil.Text = lastname;

            }

            string key = await SecureStorage.GetAsync("key");

            RegistroModel user = await _usuarioRepositorio.getUserById(dataUser.User.LocalId);



            await DisplayAlert("Alerta de id", JsonConvert.SerializeObject(user), "ok");
            
        }

        public async void cerrarSesion(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();
            await Navigation.PushAsync(new StartPage());
        }
    }
}