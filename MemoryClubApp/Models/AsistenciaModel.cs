using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryClubApp.Models
{
    public class AsistenciaModel
    {
        public int CodigoCliente { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime HoraEntrada { get; set; }
        public string Observaciones { get; set; }
        // La sucursal viene de la enfermera
        public int Sucursal { get; set; }
        public int Usuario { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
