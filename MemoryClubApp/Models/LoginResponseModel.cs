using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace MemoryClubApp.Models
{
    public class LoginResponseModel
    {
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }

        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("UserLevel")]
        public int UserLevel { get; set; }

        [JsonProperty("Sucursal")]
        public int Sucursal { get; set; }

        [JsonProperty("IdUsuario")]
        public int IdUsuario { get; set; }
    }
}
