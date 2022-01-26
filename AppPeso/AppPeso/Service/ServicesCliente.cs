using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppPeso.Service
{
    public class ServicesCliente
    {

        public async Task<string> registrarUser(string nombre, string apellido, string altura, string peso)
        {

            var url = "https://cooperativa-pugacho.herokuapp.com/user/store";


            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("nombre",nombre),
                new KeyValuePair<string, string>("apellido",apellido),
                new KeyValuePair<string, string>("altura",altura),
                new KeyValuePair<string, string>("peso",peso),

            };
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new FormUrlEncodedContent(keyValues);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

    }
}
