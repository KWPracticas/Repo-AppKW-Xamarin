using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppKW.ViewModels
{
    internal class ContactoViewModel : BaseViewModel
    {
        public static string FirebaseClient = "https://xamarinapptest-6e415-default-rtdb.firebaseio.com";
        public static string FrebaseSecret = "GqyWsXoIlQUmNI4r0xBy78T4mXosCTOm6CkxmjCq";

        public FirebaseClient fc = new FirebaseClient(FirebaseClient,
                                   new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FrebaseSecret) });
        public ContactoViewModel()
        {
            Title = "Contacto";
        }
    }
}
