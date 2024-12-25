using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BarberShop.Services
{
    public class HairstyleApiService
    {
        private readonly HttpClient _httpClient;

        public HairstyleApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", "0a7538773amsh856aaf83cc81155p142157jsnfa4d2e0f7682");
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "hairstyle-changer.p.rapidapi.com");
        }

        public async Task<string> GetHairstyleSuggestionsAsync(string imageUrl)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://hairstyle-changer.p.rapidapi.com/huoshan/facebody/hairstyle"),
                Content = new StringContent($"{{\"imageUrl\": \"{imageUrl}\"}}")
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
