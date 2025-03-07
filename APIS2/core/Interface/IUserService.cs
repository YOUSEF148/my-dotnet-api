using Core.Entites;

using System.Threading.Tasks;

namespace Core.Interface
{
	
		public interface IUserService
		{
			Task<User> RegisterUserAsync(string name, string email, string password); // ❌ إزالة `role`
			Task<User> GetUserByEmailAsync(string email);
		Task<User> AddUserAsync(string name, string email, string password, string role);
		Task<bool> DeleteUserAsync(int userId);


	}

}
