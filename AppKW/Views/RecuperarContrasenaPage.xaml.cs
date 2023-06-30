using Acr.UserDialogs;
using AppKW.Services;
using AppKW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Provider.Telephony;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecuperarContrasenaPage : ContentPage
    {
        AuthenticationService authenticationService = new AuthenticationService();
        public RecuperarContrasenaPage()
        {
            InitializeComponent();
        }

        public async void BtnSendLink(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtEmail.Text))
            {
                await DisplayAlert("Error", "Escribe un correo electrónico valido", "Aceptar");
                return;
            }

            try
            {
                UserDialogs.Instance.ShowLoading("Cargando...");
                await authenticationService.ResetPassword(TxtEmail.Text.Trim());
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Correcto", "Se envió un correo para poder restablecer tu contraseña, revisa tu bandeja de entrada o spam", "Aceptar");
                ClearForm();
            } catch (Exception ex)
            {
                if (ex.Message.Contains("INVALID_EMAIL"))
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Ingresa un correo electrónico válido", "Aceptar");
                } else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Algo salió mal, inténtalo más tarde", "Aceptar");
                }
            }
        }

        private void ClearForm()
        {
            TxtEmail.Text = string.Empty;
        }
    }
}