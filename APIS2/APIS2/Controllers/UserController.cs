using AutoMapper;
using core.DTOS;
using core.DTOS.Core.DTOS;
using core.Interface;
using Core.Entites;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;
	private readonly IAuthService _authService; // ✅ إضافة IAuthService
	private readonly IMapper _mapper;

	public UserController(IUserService userService, IAuthService authService, IMapper mapper)
	{
		_userService = userService;
		_authService = authService;
		_mapper = mapper;
	}

	[HttpPost("register")]
	public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
	{
		if (registerDto == null)
		{
			return BadRequest(new { message = "Invalid request. Please provide valid user data." });
		}

		var user = await _userService.RegisterUserAsync(registerDto.Name, registerDto.Email, registerDto.Password);

		if (user == null)
		{
			return BadRequest(new { message = "User registration failed." });
		}

		var userDto = _mapper.Map<UserDto>(user);
		var token = _authService.GenerateJwtToken(userDto); // ✅ استخدم _authService

		return CreatedAtAction(nameof(GetUserByEmail), new { email = user.Email }, new { user = userDto, token });
	}
	
	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] UserLoginDto model)
	{
		var user = await _userService.GetUserByEmailAsync(model.Email);

		if (user == null || user.Password != model.Password)
			return Unauthorized(new { message = "Invalid email or password" });

		var userDto = _mapper.Map<UserDto>(user);
		var token = _authService.GenerateJwtToken(userDto); // ✅ استخدم _authService

		return Ok(new { user = userDto, token });
	}

	[HttpGet("by-email")]
	public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
	{
		var user = await _userService.GetUserByEmailAsync(email);
		if (user == null)
		{
			return NotFound(new { message = "User not found" });
		}

		return Ok(user);
	}
	[HttpPost("logout")]
	public IActionResult Logout()
	{
		// هنا العميل هو المسؤول عن حذف التوكن من التخزين المحلي (Local Storage أو الـ Session)
		// لا حاجة لإجراء تغييرات على الخادم إذا كان التوكن مجرد JWT
		return Ok(new { message = "Logged out successfully" });
	}
}
