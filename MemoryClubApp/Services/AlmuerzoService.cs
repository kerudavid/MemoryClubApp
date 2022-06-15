using MemoryClubApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MemoryClubApp.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MemoryClubApp.Services
{
    public class AlmuerzoService: IAlmuerzoService
    {
        public async Task<CateringResponseModel> Almuerzo(CateringModel almuerzoModel)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();
            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(almuerzoModel);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/Catering", content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<CateringResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }
    }
}
