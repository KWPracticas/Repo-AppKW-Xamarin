using AppKW.Services;
using AppKW.ViewModels;
using AppKW.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.BackgroundVideoView;
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
            
        }

        protected override void OnStart()
        {
            //await _userRepository.validateToken();

            base.OnStart();
        }

        protected override void OnSleep()
        {
            //SecureStorage.RemoveAll();
        }

        protected override void OnResume()
        {

        }
    }
}
