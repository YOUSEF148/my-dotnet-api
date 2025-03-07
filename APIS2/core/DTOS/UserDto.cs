using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.DTOS
{
	namespace Core.DTOS
	{
		public class UserDto
		{
			public int Id { get; set; }
			public string Name { get; set; } = string.Empty;
			public string Email { get; set; } = string.Empty;
			public string Role { get; set; } = "user"; // Default role is 'user'
		}
	}

}
