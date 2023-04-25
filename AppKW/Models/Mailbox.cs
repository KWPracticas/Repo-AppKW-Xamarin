using Java.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppKW.Models
{
    internal class Mailbox
    {
        public string name { get; set; }
        public string company_name { get; set; }
        public string reason { get; set; }
        public string date_of_incident { get; set; }
        public string message { get; set; }
    }
}
