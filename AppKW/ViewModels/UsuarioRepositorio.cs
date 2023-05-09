using AppKW.Models;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppKW.ViewModels
{
    class UsuarioRepositorio
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://appkw-67b39-default-rtdb.firebaseio.com/");
        static string webAPIKey = "AIzaSyA9YNZpoGoOmy18G8aUA84VmIcmcmXOFAE";
        FirebaseAuthProvider authProvider; 

        public UsuarioRepositorio()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }
        public async Task<bool> Resgister(string nombre, string correo, string contrasena)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(correo, contrasena, nombre, true);
            
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            
            return false;
        }

        public async Task<string> SignIn(string correo, string contrasena)
        {
            var userCredential = await authProvider.SignInWithEmailAndPasswordAsync(correo, contrasena);

            string userEmail = userCredential.User.Email;

            char delimitador = '@';

            string[] domain = userEmail.Split(delimitador);
            
            if (!string.IsNullOrEmpty(domain[1]) && domain[1] == "kenworthdeleste.com.mx") // cambiar dominio
            {
                await SecureStorage.SetAsync("role", "Employee"); //usuario Trabajador
            }
            //if (!string.IsNullOrEmpty(domain[1]) && domain[1] == "gmail.com") // cambiar dominio
            else
            {
                await SecureStorage.SetAsync("role", "User"); //Usuario registrado
            }


            //Console.WriteLine(domain[1]);

            if (userCredential.User.IsEmailVerified)
            {
                if(!string.IsNullOrEmpty(userCredential.FirebaseToken))
                {
                    return userCredential.FirebaseToken;
                }
            }
            
            return "";
        }

        //Guardar datos del usuario
        public async Task<bool> Save(RegistroModel registro)
        {
           var data = await firebaseClient.Child(nameof(RegistroModel)).PostAsync(JsonConvert.SerializeObject(registro));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        //Recuperar contraseña
        public async Task<bool>ResetPassword(string correo)
        {
            await authProvider.SendPasswordResetEmailAsync(correo);
            return true;
        }

        public async Task<bool>Guardar()
        {
            await authProvider.SignInWithGoogleIdTokenAsync("123");
            return true;

        }
        //Get ID
       /* public async Task<RegistroModel> GetById(string id)
        {
            var authToken = await authProvider
                GetFreshAuthAsync();
            var response = await firebaseClient
                .Child("usuarios")
                .Child(id)
                .OnceAsync<RegistroModel>(authToken.FirebaseToken);

            return response.FirstOrDefault()?.Object;
        } */




    }
}
