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
    public class LoginService: ILoginService
    {
        public async Task<LoginResponseModel> Login(UsuarioLoginModel usuarioLoginModel)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();
            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(usuarioLoginModel);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/Login", content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<LoginResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }
    }
}
