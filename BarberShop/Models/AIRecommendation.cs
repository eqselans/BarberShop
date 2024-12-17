namespace BarberShop.Models
{
	public class AIRecommendation
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string UploadedImage { get; set; }
		public string Recommendation { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
