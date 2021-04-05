using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaVarasULP.Models
{
    public class Inquilino
    {
        public int Id{ get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DomicilioLaboral { get; set; }
        public string Email { get; set; }
        public string TelefonoInquilino { get; set; }
        public string NombreGarante{ get; set; }
        public string DniGarante{ get; set; }
        public string TelefonoGarante { get; set; }


    }
}

   
   