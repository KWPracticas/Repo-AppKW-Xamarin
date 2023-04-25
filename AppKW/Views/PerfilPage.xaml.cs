using AppKW.Models;
using AppKW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        public async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Warning", "Data not found.", "Ok");
        }
            

    }
}