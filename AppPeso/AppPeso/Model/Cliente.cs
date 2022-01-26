using System;
using System.Collections.Generic;
using System.Text;

namespace AppPeso.Model
{
     public class Cliente
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string altura { get; set; }
        public string peso { get; set; }
        public int estado { get; set; }
        public double IMC { get; set; }
    }
}
