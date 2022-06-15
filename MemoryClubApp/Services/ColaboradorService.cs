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
    public class ColaboradorService: IColaboradorService
    {
        public async Task<ColaboradorResponseModel> GetColaborador()
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();
            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("api/Colaborador");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<ColaboradorResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }
    }
}
