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
    public partial class EmployeesPage : ContentPage
    {
        public EmployeesPage()
        {
            InitializeComponent();
            /*var browser = new WebView();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body>
                                <h1>Xamarin.Forms</h1>
                                <iframe src=""https://docs.google.com/forms/d/e/1FAIpQLSf-0iaGrIM1Vbf39OjKd-CESiN3qmX99WU_mPYyCvi6w1bg5g/viewform?embedded=true"" width=""350"" height=""540"" frameborder=""0"" marginheight=""0"" marginwidth=""0"">Cargando…</iframe>
                                </body>
                                </html>";
            browser.Source = htmlSource;
            Content = browser; */
        }

          private async void Clicked_Encuestas(object sender, EventArgs e)
          {
              await Navigation.PushAsync(new EncuestasPage());
          }

         private async void Clicked_Check(object sender, EventArgs e)
          {
              await Navigation.PushAsync(new CheckPage());
          } 

    }
}