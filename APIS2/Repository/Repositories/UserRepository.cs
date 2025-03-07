using core.Interface;
using Core.Entites;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}
		

		public async Task AddUserAsync(User user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
		}
		public async Task<bool> DeleteUserAsync(User user)
		{
			// تحقق إذا كان المستخدم موجودًا
			var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

			if (existingUser == null)
			{
				return false; // المستخدم غير موجود
			}

			// حذف المستخدم من قاعدة البيانات
			_context.Users.Remove(existingUser);
			await _context.SaveChangesAsync();

			return true; // تم الحذف بنجاح
		}
		public async Task<User> GetUserByIdAsync(int userId)
		{
			// Query the database for a user by their ID
			var user = await _context.Users
				.FirstOrDefaultAsync(u => u.Id == userId);

			return user; // This can be null if the user is not found
		}
		public async Task<bool> UserExistsAsync(string email)
		{
			return await _context.Users.AnyAsync(u => u.Email == email);
		}
	}

}
