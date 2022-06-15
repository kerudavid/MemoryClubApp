using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryClubApp.Models
{
    public class TransporteResponseModel
    {
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
        [JsonProperty("Success")]
        public bool Success { get; set; }
    }
}
