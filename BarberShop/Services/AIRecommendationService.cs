using System.IO;
using System.Threading.Tasks;

namespace BarberShop.Services
{
	public class AIRecommendationService : IAIRecommendationService
	{
		public async Task<string> GenerateRecommendationAsync(string uploadedImagePath)
		{
			// Yapay Zeka iş mantığı burada uygulanır.
			// Bu örnekte basit bir simülasyon sağlanmıştır.

			if (!File.Exists(uploadedImagePath))
				throw new FileNotFoundException("Yüklenen resim bulunamadı.");

			// Simüle edilmiş bir öneri
			await Task.Delay(1000); // İşlem simülasyonu için gecikme
			return "Kısa ve modern bir saç modeli öneriyoruz!";
		}
	}
}
