using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Interface
{
	public interface IUserRepository
	{
		Task AddUserAsync(User user);  // Method for adding a user to the database
		Task<User> GetUserByEmailAsync(string email);  // Method to get a user by their email
		Task<bool> UserExistsAsync(string email);
		Task<bool> DeleteUserAsync(User user);
		Task<User> GetUserByIdAsync(int userId);


	}
}