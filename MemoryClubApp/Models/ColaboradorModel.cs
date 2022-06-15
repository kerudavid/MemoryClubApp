using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace MemoryClubApp.Models
{
    public class ColaboradorModel
    {
        public int IdColaborador { get; set; }
        public string NombreColaborador { get; set; }
        public int sucursal { get; set; }
        public string CIColaborador { get; set; }
    }
}
