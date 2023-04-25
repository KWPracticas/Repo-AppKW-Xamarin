using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using AppKW.Models;
using Xamarin.Forms.Xaml;


namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();

            List<CarouselModel> carousels = new List<CarouselModel>()
            {
                new CarouselModel(){Title = "img 1", Url = "img1.jpg"},
                new CarouselModel(){Title = "img 2", Url = "img2.jpg"},
                new CarouselModel(){Title = "img 3", Url = "img4.jpg"},
                new CarouselModel(){Title = "img 3", Url = "img5.jpg"}
            };


            Carousel.ItemsSource = carousels;

            Device.StartTimer(TimeSpan.FromSeconds(2), (Func<bool>)(() =>
            {
                Carousel.Position = (Carousel.Position + 1) % carousels.Count;
                return true;
            }));
        }

        private void DisplayFullImage_TappedMision(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Mision());
        }
        private void DisplayFullImage_TappedVision(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Vision());
        }
        //Redireccionamento a redes sociales.
        private void RedSocial_Facebook(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://www.facebook.com/kenworthdeleste"));
        }
        private void RedSocial_twitter(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://twitter.com/KenworthdelEste"));
        }
        private void RedSocial_Instagram(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://www.instagram.com/kenworthdeleste/"));
        }
        private void RedSocial_youtube(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://www.youtube.com/channel/UCeZZCLFDX220ex1iyUote1g/videos"));
        }
        private void RedSocial_tiktok(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://www.tiktok.com/@kenworthdeleste?lang=es"));
        }
    }
}
