using AppKW.Views;
using Xamarin.Forms;

namespace AppKW.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        public bool isRegularUser;
        public bool isEmployee;
        public bool isGuest;

        public string isTitle;
        public string icon;

        public bool IsRegularUser { get => isRegularUser; set => SetProperty(ref isRegularUser, value); }
        public bool IsEmployee { get => isEmployee; set => SetProperty(ref isEmployee, value); }
        public bool IsGuest { get => isGuest; set => SetProperty(ref isGuest, value); }

        public string IsTitle { get => isTitle; set => SetProperty(ref isTitle, value); }
        public string Icon { get => icon; set => SetProperty(ref icon, value); }

        public ShellViewModel() 
        {
            MessagingCenter.Subscribe<LoginPage>(this, "isRegularUser", (sender) =>
            {
                IsRegularUser = true;
                IsEmployee = false;
                IsGuest = false;

                IsTitle = "Cerrar sesión";
                Icon = "logout.png";
            });

            MessagingCenter.Subscribe<LoginPage>(this, "isEmployee", (sender) =>
            {
                IsEmployee = true;
                IsRegularUser = false;
                IsGuest = false;

                IsTitle = "Cerrar sesión";
                Icon = "logout.png";
            });

            MessagingCenter.Subscribe<StartPage>(this, "isGuest", (sender) =>
            {
                IsGuest = true;
                IsRegularUser = false;
                IsEmployee = false;

                IsTitle = "Iniciar sesión";
                Icon = "login.png";
            });

            //App
            MessagingCenter.Subscribe<App>(this, "isRegularUser", (sender) =>
            {
                IsRegularUser = true;
                IsEmployee = false;
                IsGuest = false;

                IsTitle = "Cerrar sesión";
                Icon = "logout.png";
            });

            MessagingCenter.Subscribe<App>(this, "isEmployee", (sender) =>
            {
                IsEmployee = true;
                IsRegularUser = false;
                IsGuest = false;

                IsTitle = "Cerrar sesión";
                Icon = "logout.png";
            });

            MessagingCenter.Subscribe<App>(this, "isGuest", (sender) =>
            {
                IsGuest = true;
                IsRegularUser = false;
                IsEmployee = false;

                IsTitle = "Iniciar sesión";
                Icon = "login.png";
            });
        }
    }
}