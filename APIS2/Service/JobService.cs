using Core.Entites;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
namespace Services
{
	public class JobService
	{
		private readonly AppDbContext _context;

		public JobService(AppDbContext context)
		{
			_context = context;
		}

		// جلب الوظائف
		public async Task<List<Job>> GetAllJobsAsync()
		{
			return await _context.Jobs.ToListAsync();
		}

		// جلب الوظيفة عن طريق الـ jobId
		public async Task<Job> GetJobByIdAsync(int jobId)
		{
			return await _context.Jobs.FirstOrDefaultAsync(j => j.Id == jobId);
		}


		// تحديث حالة الوظيفة
		public async Task UpdateJobAsync(Job job)
		{
			_context.Jobs.Update(job);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteJobAsync(int jobId)
		{
			var job = await _context.Jobs.FindAsync(jobId);
			if (job == null)
			{
				throw new Exception("Job not found");
			}

			_context.Jobs.Remove(job);
			await _context.SaveChangesAsync();
		}
		public async Task AddJobAsync(Job job)
		{
			_context.Jobs.Add(job);
			await _context.SaveChangesAsync();
		}
		public async Task<List<Job>> SearchJobsByNameAsync(string jobName)
		{
			// التحقق إذا كان اسم الوظيفة المدخل فارغ
			if (string.IsNullOrEmpty(jobName))
			{
				throw new ArgumentException("Job name cannot be empty");
			}

			// استخدام Linq للبحث عن الوظائف التي تحتوي على الاسم المدخل
			return await _context.Jobs
				.Where(job => job.Title.Contains(jobName))  // بحث باستخدام Contains للبحث الجزئي
				.ToListAsync();  // إرجاع قائمة بالوظائف المطابقة
		}
	} }



