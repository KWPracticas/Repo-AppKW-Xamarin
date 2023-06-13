using System;
using System.Collections.Generic;
using System.Text;

namespace AppKW.Models
{
    public class User
    {
        public string Uid { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
    }

    public class RespuestaHttp
    {
        public Dictionary<string, User> Datos { get; set; }

    }
}
