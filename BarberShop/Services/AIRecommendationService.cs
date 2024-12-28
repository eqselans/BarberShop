using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace BarberShop.Services
{
    public class AIRecommendationService : IAIRecommendationService
    {
        private readonly HttpClient _httpClient;

        public AIRecommendationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", "0a7538773amsh856aaf83cc81155p142157jsnfa4d2e0f7682");
            // Yedek api key ce1583ac75msh0f13584327500dcp1f7be5jsnb5b993fe1482
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "hairstyle-changer.p.rapidapi.com");
        }

        public async Task<string> GetHairstyleRecommendationAsync(IFormFile photo, string hairOption)
        {
            if (photo == null || photo.Length == 0)
            {
                throw new ArgumentException("Geçerli bir fotoğraf yüklenmedi.");
            }

            using var content = new MultipartFormDataContent();

            try
            {
                // Resim dosyasını form-data içeriğine ekle
                using var stream = photo.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                content.Add(fileContent, "image_target", photo.FileName);

                // Saç modelini form-data içeriğine ekle
                content.Add(new StringContent(hairOption)
                {
                    Headers =
                    {
                        ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "hair_type",
                        }
                    }
                });
                // API isteğini oluştur
                var response = await _httpClient.PostAsync("https://hairstyle-changer.p.rapidapi.com/huoshan/facebody/hairstyle", content);

                // API yanıtını kontrol et
                if (!response.IsSuccessStatusCode)
                {
                    var errorDetails = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API isteği başarısız oldu. Hata: {response.StatusCode}, Detay: {errorDetails}");
                }

                // Yanıtı ayrıştır
                var body = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(body);

                // "data.image" özelliğini al
                if (json.RootElement.TryGetProperty("data", out var dataElement) &&
                    dataElement.TryGetProperty("image", out var imageElement))
                {
                    return imageElement.GetString();
                }
                else
                {
                    throw new Exception("API yanıtında beklenen 'data.image' özelliği bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda bir loglama mekanizması eklenebilir
                throw new Exception($"AIRecommendationService işlem sırasında bir hata oluştu: {ex.Message}", ex);
            }
        }
    }
}
