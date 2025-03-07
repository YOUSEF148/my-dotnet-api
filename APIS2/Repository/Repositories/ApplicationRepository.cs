//using core.Interface;
//using Core.Entites;

//using Core.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using Repository.Data;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace Repository.Repositories
//{
//	public class ApplicationRepository : IApplicationRepository
//	{
//		private readonly AppDbContext _context;

//		public ApplicationRepository(AppDbContext context)
//		{
//			_context = context;
//		}
//		public async Task<IEnumerable<Application>> GetApplicationsByUserIdAsync(int userId) // ✅ تأكد من وجودها
//		{
//			return await _context.Applications
//				.Where(a => a.UserId == userId)
//				.ToListAsync();
//		}

//		public async Task AddAsync(Application application)
//		{
//			await _context.Applications.AddAsync(application);
//			await _context.SaveChangesAsync();
//		}

//		public async Task<bool> HasUserAlreadyAppliedAsync(int userId, int jobId)
//		{
//			return await _context.Applications
//				.AnyAsync(a => a.UserId == userId && a.JobId == jobId);
//		}
//		public async Task<bool> ExistsAsync(Expression<Func<Application, bool>> predicate)
//		{
//			return await _context.Applications.AnyAsync(predicate);
//		}




//	}
//}
