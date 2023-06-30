using AppKW.Views;
using Xamarin.Forms;

namespace AppKW.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        public bool isRegularUser;
        public bool isEmployee;
        public bool isGuest;

        public bool IsRegularUser { get => isRegularUser; set => SetProperty(ref isRegularUser, value); }
        public bool IsEmployee { get => isEmployee; set => SetProperty(ref isEmployee, value); }
        public bool IsGuest { get => isGuest; set => SetProperty(ref isGuest, value); }
        public ShellViewModel() 
        {
            MessagingCenter.Subscribe<LoginPage>(this, "isRegularUser", (sender) =>
            {
                IsRegularUser = true;
                IsEmployee = false;
            });

            MessagingCenter.Subscribe<LoginPage>(this, "isEmployee", (sender) =>
            {
                IsEmployee = true;
                IsRegularUser = false;
            });

            MessagingCenter.Subscribe<StartPage>(this, "isGuest", (sender) =>
            {
                IsGuest = true;
                IsRegularUser = false;
                IsEmployee = false;
            });
        }
    }
}