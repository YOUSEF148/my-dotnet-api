using core.DTOS.Core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Interface
{
	public interface IAuthService
	{
		string GenerateJwtToken(UserDto user);

	}
}
