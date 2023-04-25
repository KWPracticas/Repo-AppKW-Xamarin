using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SucursalesPage : ContentPage
    {
        public SucursalesPage()
        {
            InitializeComponent();

            //URL del mapa
            url_mapa.Source = "https://www.google.com/maps/d/u/0/edit?mid=1V82L3YVe3Tl0suWpLy4IZ-g2kE6w0V0&usp=sharing";
        }
    }
}