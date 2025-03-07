using Core.Entites;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
	public class JobRepository : IJobRepository
	{
		private readonly AppDbContext _context;

		public JobRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Job>> GetAllJobsAsync()
		{
			return await _context.Jobs.ToListAsync();
		}

		public async Task<Job?> GetJobByIdAsync(int id) // ✅ تأكد من وجودها
		{
			return await _context.Jobs.FindAsync(id);
		}
		public async Task<bool> DeleteUserAsync(User user)
		{
			// Remove the user from the database
			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
			return true; // Success
		}

		public async Task<IEnumerable<Job>> GetJobsByIdsAsync(IEnumerable<int> jobIds) // ✅ تأكد من وجودها
		{
			return await _context.Jobs
				.Where(j => jobIds.Contains(j.Id))
				.ToListAsync();
		}
	

		public async Task AddJobAsync(Job job)
		{
			_context.Jobs.Add(job);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateJobAsync(Job job) // ✅ تأكد من وجودها
		{
			_context.Jobs.Update(job);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteJobAsync(int id)
		{
			var job = await _context.Jobs.FindAsync(id);
			if (job != null)
			{
				_context.Jobs.Remove(job);
				await _context.SaveChangesAsync();
			}
		}

		


		public async Task<IEnumerable<Job>> SearchJobsByNameAsync(string jobTitle) // ✅ تنفيذ SearchJobsByNameAsync
		{
			return await _context.Jobs
				.Where(j => j.Title.ToLower().Contains(jobTitle.ToLower()))
				.ToListAsync();
		}

	}
	
}