using Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


		public interface IJobRepository
		{
			Task<IEnumerable<Job>> GetAllJobsAsync();
			Task<Job?> GetJobByIdAsync(int id); // ✅ أضف هذا التوقيع
			Task AddJobAsync(Job job);
			Task UpdateJobAsync(Job job); // ✅ أضف هذا التوقيع
			Task DeleteJobAsync(int id);
			Task<IEnumerable<Job>> SearchJobsByNameAsync(string jobTitle); // ✅ أضف هذا التوقيع
			
	
		}
		

	
