using AppKW.Models;
using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppKW.ViewModels
{
    class UsuarioRepositorio
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://appkw-67b39-default-rtdb.firebaseio.com/");
        private static string webAPIKey = "AIzaSyA9YNZpoGoOmy18G8aUA84VmIcmcmXOFAE";
        private FirebaseAuthProvider authProvider; 

        public UsuarioRepositorio()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }
        public async Task Register(string correo, string contrasena, string nombre)
        {
            FirebaseAuthLink user = await authProvider.CreateUserWithEmailAndPasswordAsync(correo, contrasena, nombre, true);
        }
        public async Task<FirebaseAuthLink> SignIn(string correo, string contrasena)
        {
            FirebaseAuthLink auth = await authProvider.SignInWithEmailAndPasswordAsync(correo, contrasena);

            string email = auth.User.Email;

            assignRole(email);

            if(!auth.User.IsEmailVerified)
            {
                return null;
            }

            string userObj = JsonConvert.SerializeObject(auth);

            await SecureStorage.SetAsync("userObj", userObj); // auth
            await SecureStorage.SetAsync("token", auth.FirebaseToken); // token
            return auth;
        }
        public async Task ResetPassword(string correo)
        {
            await authProvider.SendPasswordResetEmailAsync(correo);
        }
        private async void assignRole(string email)
        {
            char delimitador = '@';

            string[] domain = email.Split(delimitador);

            // Validar si es empleado o cliente
            if (!string.IsNullOrEmpty(domain[1]) && domain[1] == "kenworthdeleste.com.mx")
            {
                await SecureStorage.SetAsync("role", "Employee");
            }
            else
            {
                await SecureStorage.SetAsync("role", "User");
            }
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
