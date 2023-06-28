using AppKW.Services;
using Xamarin.Forms;

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
    }
}