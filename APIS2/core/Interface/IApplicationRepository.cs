using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace core.Interface
{
	public interface IApplicationRepository
	{
		Task<IEnumerable<Application>> GetApplicationsByUserIdAsync(int userId);
		Task AddAsync(Application application);
		Task<bool> ExistsAsync(Expression<Func<Application, bool>> predicate);

	}
}
