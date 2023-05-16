using AppKW.Models;
using AppKW.ViewModels;
using Firebase.Auth;
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
            string token = await SecureStorage.GetAsync("token");
            User userInfo = await _usuarioRepositorio.authProvider.GetUserAsync(token);

            String name = userInfo.DisplayName;
            String email = userInfo.Email;
            String lastName = userInfo.LastName;
        }

        public async void cerrarSesion(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();
            await Navigation.PushAsync(new StartPage());
        }
    }
}