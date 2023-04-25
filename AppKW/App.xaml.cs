using AppKW.Services;
using AppKW.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            //Device.SetFlags(new string[] { "MediaElement_Experimental" });
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            SecureStorage.RemoveAll();
        }

        protected override void OnResume()
        {
        }


    }
}
