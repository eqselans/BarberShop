using BarberShop.Models;

namespace BarberShop.Data.Repository
{
	public class EmployeeRepository : GenericRepository<Employee>
	{
		public EmployeeRepository(ApplicationDbContext context) : base(context) { }

		public async Task<IEnumerable<Employee>> GetBySpecializationAsync(string specialization)
		{
			return await FindAsync(e => e.Specialization == specialization);
		}
	}
}
