using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppKW.Services
{
    class AuthenticationService
    {
        static string apiKey = "AIzaSyBaycg7yb-VDdrzTr_p7HSnETK_B3ac1Ts";
        static string databaseURL = "https://kenworthapp-cc0fa-default-rtdb.firebaseio.com";
        static string Domain = "kenworthdeleste.com.mx";
        FirebaseAuthProvider firebaseAuthProvider;
        FirebaseClient firebaseClient;

        public AuthenticationService()
        {
            firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            firebaseClient = new FirebaseClient(databaseURL);
        }

        public async Task<FirebaseAuthLink> Register(string email, string password, string name, string lastname)
        {
            FirebaseAuthLink newUser = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(email, password, name, true);

            string role = AssignRole(newUser.User.Email);

            Models.User user = new Models.User
            {
                uid = newUser.User.LocalId,
                name = name,
                lastname = lastname,
                email = email,
                role = role
            };

            await SaveUserInRealtimeDatabase(user);

            return newUser;
        }

        // guardar datos del usuario en realtime database
        public async Task SaveUserInRealtimeDatabase(Models.User user)
        {
            await firebaseClient.Child("users").PostAsync(JsonConvert.SerializeObject(user));
        }

        // asignar rol
        private string AssignRole(string email)
        {
            char limit = '@';
            string[] domain = email.Split(limit);

            if (!string.IsNullOrEmpty(domain[1]) && domain[1] == Domain)
            {
                return "Employee";
            }
            else if (!string.IsNullOrEmpty(domain[1]))
            {
                return "Regular User";
            }
            else
            {
                return null;
            }
        }
    }
}
