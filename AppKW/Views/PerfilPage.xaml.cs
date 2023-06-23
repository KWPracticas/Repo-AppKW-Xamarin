using AppKW.ViewModels;
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
using User = AppKW.Models.User;

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
            User user = await getUser();
            TxtNombrePerfil.Text = user.name;
            TxtApellidoPerfil.Text = user.lastname;
            TxtcorreoPerfil.Text = user.email;
        }

        // Obtener datos del usuario desde firebase
        private async Task<User> getUser()
        {
            string result = await _usuarioRepositorio.getDataUserWithFirebase();

            if (!string.IsNullOrEmpty(result))
            {
                Dictionary<string,User> jsonObject = JsonConvert.DeserializeObject<Dictionary<string,User>>(result);
                User user = jsonObject.Values.FirstOrDefault();
                return user;
            }

            return null;
        }
    }
}
