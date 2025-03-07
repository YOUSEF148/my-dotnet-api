using Core.Entites;
using Core.Interface;
using Repository.Data;
namespace Service
{
	public class ApplicationService : IApplicationService
	{
		private readonly AppDbContext _context;

		public ApplicationService(AppDbContext context)
		{
			_context = context;
		}

		// تسجيل التقديم في جدول Applications
		public async Task ApplyForJobAsync(Application application)
		{
			_context.Applications.Add(application);
			await _context.SaveChangesAsync();
		}
	}
}