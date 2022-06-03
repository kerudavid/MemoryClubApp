using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace MemoryClubApp.Models
{
    public class ClienteResponseModel
    {
        [JsonProperty("ListaCliente")]
        public List<ClienteModel> ListaCliente { get; set; }

        [JsonProperty("MessageError")]
        public string MessageError { get; set; }

        [JsonProperty("success")]
        public bool success { get; set; }
    }
}
