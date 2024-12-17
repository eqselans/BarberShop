using BarberShop.Models;

namespace BarberShop.Data.Repository
{
	public class ServiceRepository : GenericRepository<Service>
	{
		public ServiceRepository(ApplicationDbContext context) : base(context) { }

		public async Task<IEnumerable<Service>> GetServicesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
		{
			return await FindAsync(s => s.Price >= minPrice && s.Price <= maxPrice);
		}
	}
}
