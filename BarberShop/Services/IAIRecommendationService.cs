namespace BarberShop.Services
{
	public interface IAIRecommendationService
	{
		Task<string> GenerateRecommendationAsync(string uploadedImagePath);
	}
}
