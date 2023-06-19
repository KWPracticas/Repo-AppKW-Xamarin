using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using User = AppKW.Models.User;

namespace AppKW.ViewModels
{
    class UsuarioRepositorio
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://appkw-67b39-default-rtdb.firebaseio.com/");
        private const string webAPIKey = "AIzaSyA9YNZpoGoOmy18G8aUA84VmIcmcmXOFAE";
        private FirebaseAuthProvider authProvider;

        public UsuarioRepositorio()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }

        public async Task register(string name, string lastname, string email, string password)
        {
            FirebaseAuthLink result = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password, name, true);

            string role = assignRole(result.User.Email);

            User user = new User();
            user.uid = result.User.LocalId;
            user.role = role;
            user.name = name;
            user.lastname = lastname;
            user.email = email;

            await saveUserInRealtimeDatabase(user);
        }

        private async Task saveUserInRealtimeDatabase(User user)
        {
            await firebaseClient.Child("users").PostAsync(JsonConvert.SerializeObject(user));
        }

        public async Task resetPassword(string email)
        {
            await authProvider.SendPasswordResetEmailAsync(email);
        }

        public async Task<FirebaseAuthLink> signIn(string email, string password)
        {
            FirebaseAuthLink result = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            
            if (result.User.IsEmailVerified)
            {
                //await assignRole(result.User.Email);
                string userSession = JsonConvert.SerializeObject(result); // Convertir el result (.json) en un string
                await SecureStorage.SetAsync("userSession", userSession);
                return result;
            }

            return null;
        }

        public async Task<bool> validateToken()
        {
            string userSession = await SecureStorage.GetAsync("userSession");
            
            if (!string.IsNullOrEmpty(userSession))
            {
                FirebaseAuthLink user = JsonConvert.DeserializeObject<FirebaseAuthLink>(userSession); // Convertir el string guardado en el storage a un object
                Firebase.Auth.User result = await authProvider.GetUserAsync(user.FirebaseToken); // Obtener el usuario de firebase con el token dentro del object anterior

                if (result != null)
                {
                    return true;
                }
            }

            return false;

        }

        private string assignRole(string email)
        {
            char delimitador = '@';

            string[] domain = email.Split(delimitador);

            if (!string.IsNullOrEmpty(domain[1]) && domain[1] == "kenworthdeleste.com.mx")
            {
                return "Employee";
            }

            return "User";
        }

        public async Task<bool> signInWithRefreshToken()
        {
            string userSession = await SecureStorage.GetAsync("userSession");

            if (!string.IsNullOrEmpty(userSession))
            {
                FirebaseAuthLink user = JsonConvert.DeserializeObject<FirebaseAuthLink>(userSession); // Convertir el string guardado en el storage a un object
                FirebaseAuthLink result = await authProvider.RefreshAuthAsync(user);
                if (result != null) 
                {
                    string newUserSession = JsonConvert.SerializeObject(result);
                    await SecureStorage.SetAsync("userSession", newUserSession);
                    return true;
                } else 
                { 
                    return false; 
                }
            }

            return false;
        }

        public async Task<FirebaseAuthLink> getDataUserInStorage()
        {
            string userSession = await SecureStorage.GetAsync("userSession");
            if (!string.IsNullOrEmpty(userSession))
            {
                FirebaseAuthLink user = JsonConvert.DeserializeObject<FirebaseAuthLink>(userSession);
                return user;
            }
            return null;
        }

        public async Task<string> getDataUserWithFirebase()
        {
            try
            {
                string userSession = await SecureStorage.GetAsync("userSession");
                if (!string.IsNullOrEmpty(userSession))
                {
                    FirebaseAuthLink user = JsonConvert.DeserializeObject<FirebaseAuthLink>(userSession);
                    //string uid = "EA74ElRh0YUUOnPfZdeEcVje4hp2";
                    string uid = user.User.LocalId;
                    string url = $"https://appkw-67b39-default-rtdb.firebaseio.com/users.json?orderBy=\"uid\"&equalTo=\"{uid}\"";
                    HttpClient httpClient = new HttpClient();
                    return await httpClient.GetStringAsync(url);
                }

                return null;

            } catch (Exception ex)
            { 
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public bool signOut()
        {
            try
            {
                SecureStorage.RemoveAll();
                return true;
            } catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<string> getRole()
        {
            try
            {
                string result = await getDataUserWithFirebase();
                Dictionary<string, Models.User> jsonObject = JsonConvert.DeserializeObject<Dictionary<string, Models.User>>(result);
                Models.User user = jsonObject.Values.FirstOrDefault();
                return user.role;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
