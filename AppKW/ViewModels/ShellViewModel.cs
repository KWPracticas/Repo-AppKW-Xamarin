using AppKW.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppKW.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        private bool isEmployee;
        private bool isUser;
        private bool isInvitado;

        public bool IsEmployee { get => isEmployee; set => SetProperty(ref isEmployee, value); }
        public bool IsInvitado { get => isInvitado; set => SetProperty(ref isInvitado, value); }
        public bool IsUser { get => isUser; set => SetProperty(ref isUser, value); }

        public ShellViewModel()
        {
            MessagingCenter.Subscribe<LoginPage>(this, "Employee", (sender) =>
            {
                IsEmployee = true;
                IsInvitado = false;
                IsUser = false;
            });
            MessagingCenter.Subscribe<LoginPage>(this, "User", (sender) =>
            { //Listo Logueo user
                IsUser = true;
                IsInvitado = true;
            });
            MessagingCenter.Subscribe<AppShell>(this, "invitado", (sender) =>
            {
                IsInvitado = true;
                IsUser = false;
                IsEmployee = false;
            });
            MessagingCenter.Subscribe<StartPage>(this, "Employee", (sender) =>
            {
                IsEmployee = true;
                IsInvitado = false;
                IsUser = false;
            });
            MessagingCenter.Subscribe<StartPage>(this, "User", (sender) =>
            { //Listo Logueo user
                IsUser = true;
                IsInvitado = true;
            });

        }
    }
}
