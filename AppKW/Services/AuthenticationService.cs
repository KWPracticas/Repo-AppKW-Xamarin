using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

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

        public async Task<FirebaseAuthLink> Login(string email, string password)
        {
            FirebaseAuthLink user = await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, password);

            if (!user.User.IsEmailVerified)
            {
                return null;
            }

            return user;
        }

        // iniciar como invitado
        public async Task LoginAsAGuest()
        {
            Models.User user = new Models.User
            {
                uid = "1",
                name = "guest",
                lastname = "guest",
                email = "guest@mail.com",
                role = "Guest"
            };

            await SaveUserDataInStorage(user);
        }

        // guardar el objeto que devuelve firebase auth
        public async Task SaveUserInStorage(FirebaseAuthLink user)
        {
            string userSession = JsonConvert.SerializeObject(user); // guardar usuario en storage
            await SecureStorage.SetAsync("user", userSession);
        }

        // guardar el objeto que devuelve realtime database en el storage como un string
        public async Task SaveUserDataInStorage(Models.User user)
        {
            string userData = JsonConvert.SerializeObject(user); // guardar usuario en storage
            await SecureStorage.SetAsync("userData", userData);
        }

        public async Task<FirebaseAuthLink> GetUserFromStorage()
        {
            string user = await SecureStorage.GetAsync("user");
            FirebaseAuthLink userObj = JsonConvert.DeserializeObject<FirebaseAuthLink>(user);
            return userObj;
        }

        public async Task<Models.User> GetUserDataFromStorage()
        {
            string userData = await SecureStorage.GetAsync("userData");
            Models.User userObj = JsonConvert.DeserializeObject<Models.User>(userData);
            return userObj;
        }

        // obtener datos del usuario por medio de su uid en realtime database
        public async Task<Models.User> GetUserFromRealTimeDatabase(string userUid)
        {
            try
            {
                string url = $"https://kenworthapp-cc0fa-default-rtdb.firebaseio.com/users.json?orderBy=\"uid\"&equalTo=\"{userUid}\"";
                HttpClient client = new HttpClient();
                string result = await client.GetStringAsync(url);
                Dictionary<string, Models.User> jsonObject = JsonConvert.DeserializeObject<Dictionary<string, Models.User>>(result);
                Models.User user = jsonObject.Values.FirstOrDefault();
                return user;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<string> GetUserRoleInStorage()
        {
            string userData = await SecureStorage.GetAsync("userData");

            if (!string.IsNullOrEmpty(userData))
            {
                Models.User userObj = JsonConvert.DeserializeObject<Models.User>(userData);
                return userObj.role;
            }
            else
            {
                return null;
            }
        }
    }
}