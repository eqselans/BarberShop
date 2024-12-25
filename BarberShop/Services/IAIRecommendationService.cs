using BarberShop.Models;

public interface IAIRecommendationService
{
    Task<string> GetHairstyleRecommendationAsync(IFormFile photo,string hairOption);
}

