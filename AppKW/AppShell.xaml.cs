using Acr.UserDialogs;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppKW
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando...");
            bool isRemoveUser = SecureStorage.Remove("user");
            bool isRemoveUserData = SecureStorage.Remove("userData");

            if (!isRemoveUser || !isRemoveUserData)
            {
                return;
            }

            await Shell.Current.GoToAsync("//LoginPage");
            UserDialogs.Instance.HideLoading();
        }
    }
}