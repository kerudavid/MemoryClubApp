using System;
using System.Collections.Generic;
using System.Text;
using MemoryClubApp.Interfaces;
using Newtonsoft.Json;
using MemoryClubApp.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MemoryClubApp.Services
{
    public class AsistenciaService : IAsistenciaService
    {
        public async Task<AsistenciaResponseModel> Asistencia(AsistenciaModel asistenciaModel)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();
            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(asistenciaModel);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/Asistencia", content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<AsistenciaResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }
    }
}
