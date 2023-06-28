using AppKW.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppKW.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        public bool isRegularUser;
        public bool isEmployee;

        public bool IsRegularUser { get => isRegularUser; set => SetProperty(ref isRegularUser, value); }
        public bool IsEmployee { get => isEmployee; set => SetProperty(ref isEmployee, value); }
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
        }
    }
}