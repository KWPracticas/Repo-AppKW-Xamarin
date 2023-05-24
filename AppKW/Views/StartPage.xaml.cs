using AppKW.ViewModels;
using Firebase.Auth;
using Java.Security;
using Newtonsoft.Json;
using Org.Json;
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
    public partial class StartPage : ContentPage
    {
        UsuarioRepositorio _userRepository = new UsuarioRepositorio();
        public StartPage()
        {
            InitializeComponent();
        }

        private async void Login_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void Register_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }

        private async void Guest_Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Inicio)}");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Lógica adicional al aparecer la página
            // Aquí puedes realizar operaciones asíncronas

            // Ejemplo de operación asíncrona con await
            DoAsyncTask();
        }

        private async void DoAsyncTask()
        {
            Console.WriteLine("DoAsyncTask");
            // Operación asíncrona
            bool flag = await _userRepository.validateToken();

            if (flag)
            {
                //Validar tipo de usuario 
                string role = await SecureStorage.GetAsync("role");
                MessagingCenter.Send<StartPage>(this,
                    (role == "User") ? "User" : "invitado"
                );
                MessagingCenter.Send<StartPage>(this,
                    (role == "Employee") ? "Employee" : "User"
                );
                Console.WriteLine("role: " + role);
                if (role == "User")
                {
                    await Shell.Current.GoToAsync("//inicio");
                }
                else
                {
                    await Shell.Current.GoToAsync("//empleado");
                }
                Console.WriteLine("Token valido");
            }
            else
            {
                //Refrescar la sesion
                string auth = await SecureStorage.GetAsync("userObj");

                if(!string.IsNullOrEmpty(auth))
                {
                    try
                    {
                        FirebaseAuthLink newUserSession = await _userRepository.sendRefreshToken(auth);


                        string newUserObj = JsonConvert.SerializeObject(newUserSession);

                        await SecureStorage.SetAsync("userObj", newUserObj);
                        await SecureStorage.SetAsync("token", newUserSession.FirebaseToken);



                        //Validar tipo de usuario 
                        string role = await SecureStorage.GetAsync("role");
                        MessagingCenter.Send<StartPage>(this,
                            (role == "User") ? "User" : "invitado"
                        );
                        MessagingCenter.Send<StartPage>(this,
                            (role == "Employee") ? "Employee" : "User"
                        );
                        Console.WriteLine("role: " + role);
                        if (role == "User")
                        {
                            await Shell.Current.GoToAsync("//inicio");
                        }
                        else
                        {
                            await Shell.Current.GoToAsync("//empleado");
                        }
                        Console.WriteLine("Token valido");


                    }
                    catch(Exception ex) 
                    { 
                        Console.WriteLine(ex.Message); 
                    }
                }

                await Shell.Current.GoToAsync($"//{nameof(StartPage)}");
                Console.WriteLine("Token invalido");
            }
            //await Navigation.PushAsync(new LoginPage());

            // Lógica después de la operación asíncrona
        }
    }
}
