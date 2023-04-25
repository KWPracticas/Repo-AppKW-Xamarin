using AppKW.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppKW.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}