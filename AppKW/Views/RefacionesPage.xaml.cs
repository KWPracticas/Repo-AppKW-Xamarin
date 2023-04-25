using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RefacionesPage : ContentPage
    {
        public RefacionesPage()
        {
            InitializeComponent();
        }

        private void SobreTRP_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://mex.trpparts.com/"));
        }
        private void VeracruzTRP_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("http://trpdeleste.com.mx/"));
        }
    }
}