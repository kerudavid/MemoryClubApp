using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace MemoryClubApp.Models
{
    public class AsistenciaResponseModel
    {
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
        [JsonProperty("Success")]
        public bool Success { get; set; }

    }
}
