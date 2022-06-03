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
    public class ClienteService : IClienteService
    {
        public async Task<ClienteResponseModel> Cliente()
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();
            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("api/Cliente");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<ClienteResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }
    }
}
