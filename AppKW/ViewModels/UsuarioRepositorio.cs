using AppKW.Models;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Http;
using Firebase.Database.Extensions;
using Firebase.Database.Query;
using Firebase.Database.Offline;
using Firebase.Database.Streaming;
using FirebaseAdmin;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.BackgroundVideoView;

namespace AppKW.ViewModels
{
    class UsuarioRepositorio
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://appkw-67b39-default-rtdb.firebaseio.com/");
        static string webAPIKey = "AIzaSyA9YNZpoGoOmy18G8aUA84VmIcmcmXOFAE";
        public FirebaseAuthProvider authProvider;

        public UsuarioRepositorio()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }
        public async Task<bool> Resgister(string nombre, string apellido, string correo, string contrasena)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(correo, contrasena, nombre, true);
            
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {

                RegistroModel newUser = new RegistroModel();
                newUser.Uid = token.User.LocalId;
                newUser.nombre = nombre;
                newUser.apellido = apellido;
                newUser.correo = correo;
                //GUARDAR DATOS EN FIRESTORE
                await saveData(newUser);






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

            // Guardar instancia del usuario (email, token, name)
            if (userCredential.User.IsEmailVerified)
            {
                if(!string.IsNullOrEmpty(userCredential.FirebaseToken))
                {
                    string userObj = JsonConvert.SerializeObject(userCredential);

                    await SecureStorage.SetAsync("userObj", userObj);

                    return userCredential.FirebaseToken;
                }
            }
            
            return "";
        }

        public async Task<RegistroModel> getUserById(string id)
        {
            //string id = "-NWDuv4XNlZRDeaHLAVY";
            return (await firebaseClient.Child("users" + "/" + id).OnceSingleAsync<RegistroModel>());
        }

        //Guardar datos del usuario
        public async Task<bool> saveData(RegistroModel user)
        {

            var result = await firebaseClient.Child("users").PostAsync(JsonConvert.SerializeObject(user));
            Console.WriteLine($"RESULTADO: {result.Key}");

            await SecureStorage.SetAsync("key", result.Key);

            return true;
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

        public async Task<bool> validateToken()
        {

            string token = await SecureStorage.GetAsync("token");

            // HACER LO DEL REFRESH
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var userInfo = await authProvider.GetUserAsync(token);
                    
                    if (userInfo != null)
                    {
                        return true;
                    }
                } catch(Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            return false;
        }

        public async Task<FirebaseAuthLink> sendRefreshToken(string auth)
        {
            FirebaseAuth authObj = JsonConvert.DeserializeObject<FirebaseAuth>(auth);

            FirebaseAuthLink result = await authProvider.RefreshAuthAsync(authObj);
            return result;
        }

        public async Task<FirebaseAuthLink> getDataUser()
        {
            string user = await SecureStorage.GetAsync("userObj");
            if (!string.IsNullOrEmpty(user))
            {
                FirebaseAuthLink userData = JsonConvert.DeserializeObject<FirebaseAuthLink>(user);
                return userData;
            }

            return null;
        }


    }
}
