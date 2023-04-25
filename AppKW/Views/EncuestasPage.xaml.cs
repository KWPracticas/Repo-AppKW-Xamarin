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
    public partial class EncuestasPage : ContentPage
    {
        public EncuestasPage()
        {
            InitializeComponent();
            EncuestaWeb();
        }

        public void EncuestaWeb()
        {
            var encuesta = new WebView();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body>
                            
                                <iframe id=""surveylegend-survey""
                                        src=""https://s.surveylegend.com/-NR5K8wLnjYf0QQyYnSF""
                                        width=""100%""
                                        height=""1000px""
                                        style=""frameborder: 0; border: 0; margin: 0 auto;"">
                                </iframe>
                                </body>
                                </html>";
            encuesta.Source = htmlSource;
            Content = encuesta;
        }
    }
}