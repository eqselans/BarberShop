using Microsoft.AspNetCore.Http;

namespace BarberShop.ViewModels
{
	public class AIModelViewModel
	{
		public IFormFile UploadedImage { get; set; }
		public string Recommendation { get; set; }
		public string ErrorMessage { get; set; }
	}
}
