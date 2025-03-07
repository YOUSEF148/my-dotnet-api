using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Core.Interface;

using core.DTOS.Core.DTOS;
using core.Interface;

public class AuthService : IAuthService
{
	private readonly string _jwtSecret;

	public AuthService()
	{
		_jwtSecret = "lfkjdflqevn3434nvejrekjtrjevnhjrehvntljwehtvjqhtljqhtjhqlthqlthek"; // استخدم Key أقوى من بيئة `appsettings`
	}

	public string GenerateJwtToken(UserDto user)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.UTF8.GetBytes(_jwtSecret);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),  // تأكد من أن هذا موجود
            new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role)
		}),
			Expires = DateTime.UtcNow.AddHours(1),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}

}
