using AppKW.Models;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppKW.Services
{
    class Encuestas
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://appkw-67b39-default-rtdb.firebaseio.com/");

        //Guardar datos del usuario
        public async Task<bool> Save(EncuestaModel encuestas)
        {
            var data = await firebaseClient.Child(nameof(EncuestaModel)).PostAsync(JsonConvert.SerializeObject(encuestas));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<EncuestaModel>>GetAll()
        {
            return (await firebaseClient.Child(nameof(EncuestaModel)).OnceAsync<EncuestaModel>()).Select(item => new EncuestaModel
            {
                nombre = item.Object.nombre,
                departamento = item.Object.departamento,
                pregunta1 = item.Object.pregunta1,
                pregunta2 = item.Object.pregunta2,
                pregunta3 = item.Object.pregunta3,
                pregunta4 = item.Object.pregunta4,
                pregunta5 = item.Object.pregunta5,
                pregunta6 = item.Object.pregunta6,
                pregunta7 = item.Object.pregunta7,
                Id = item.Key
            }).ToList();
        }
    }
}
