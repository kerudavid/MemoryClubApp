using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace MemoryClubApp.Models
{
    public class ClienteModel
    {
        public int IdCliente { get; set; }
        public int IdTransportista { get; set; }
        public string Nombre { get; set; }
        public string CI { get; set; }
    }
}
