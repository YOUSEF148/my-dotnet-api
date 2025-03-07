using Core.Entites;
using core.Interface;

using Core.Interface;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<User> RegisterUserAsync(string name, string email, string password)
	{
		if (await _userRepository.UserExistsAsync(email))
		{
			return null; // ❌ User already exists
		}

		var user = new User
		{
			Name = name,
			Email = email,
			Password = password, // 🔒 Encrypt password
			Role = "user" // Default role
		};

		await _userRepository.AddUserAsync(user);
		return user;
	}
	public async Task<bool> DeleteUserAsync(int userId)
	{
		// Find the user by ID
		var user = await _userRepository.GetUserByIdAsync(userId);

		if (user == null)
		{
			return false; // User not found
		}

		// Delete the user
		await _userRepository.DeleteUserAsync(user);

		return true; // User deleted successfully
	}

	
	public async Task<User> AddUserAsync(string name, string email, string password, string role)
	{
		if (await _userRepository.UserExistsAsync(email))
		{
			return null; // ❌ User already exists
		}

		var user = new User
		{
			Name = name,
			Email = email,
			Password = password, // 🔒 Encrypt password
			Role = role // Admin can set role
		};

		await _userRepository.AddUserAsync(user);
		return user;
	}
	
	public async Task<User> GetUserByEmailAsync(string email)
	{
		return await _userRepository.GetUserByEmailAsync(email);
	}

	
}
