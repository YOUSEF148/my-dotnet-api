
using APIS2.DTOS;
using AutoMapper;
using core.DTOS;
using core.DTOS.Core.DTOS;
using Core.Entites;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

using System.Security.Claims;

namespace Apis2.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class JobController : ControllerBase
{
	private readonly JobService _jobService;
	private readonly IMapper _mapper;
	private readonly IUserService _userService;

	public JobController(JobService jobService, IMapper mapper, IUserService userService)
	{
		_jobService = jobService;
		_mapper = mapper;
		_userService = userService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllJobs()
	{
		var jobs = await _jobService.GetAllJobsAsync();
		return Ok(jobs);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetJobById(int jobId)
    {
        var job = await _jobService.GetJobByIdAsync(jobId);
        if (job == null) return NotFound(new { message = "Job not found." });

        return Ok(job);
    }

	[HttpPost]
	[Authorize(Roles = "admin")]
	public async Task<IActionResult> AddJob([FromBody] JobDto jobDto)
	{
		var job = _mapper.Map<Job>(jobDto);
		await _jobService.AddJobAsync(job);
		return CreatedAtAction(nameof(GetJobById), new { id = job.Id }, job);
	}
	

	[HttpPut("{id}")]
	[Authorize(Roles = "admin")]
	public async Task<IActionResult> UpdateJob(int id, [FromBody] JobDto jobDto)
	{
		var job = await _jobService.GetJobByIdAsync(id);
		if (job == null) return NotFound();

		_mapper.Map(jobDto, job);
		await _jobService.UpdateJobAsync(job);
		return NoContent();
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "admin")]
	public async Task<IActionResult> DeleteJob(int id)
	{
		await _jobService.DeleteJobAsync(id);
		return NoContent();
	}

	[Authorize(Roles = "admin")] // ✅ Only Admin can add users
	[HttpPost("add-user")]
	public async Task<IActionResult> AddUser([FromBody] AddUserDto addUserDto)
	{
		if (addUserDto == null)
		{
			return BadRequest(new { message = "Invalid request. Please provide valid user data." });
		}

		// ✅ Ensure only "admin" or "user" roles are allowed
		if (addUserDto.Role.ToLower() != "admin" && addUserDto.Role.ToLower() != "user")
		{
			return BadRequest(new { message = "Invalid role. Only 'admin' or 'user' roles are allowed." });
		}

		var user = await _userService.AddUserAsync(addUserDto.Name, addUserDto.Email, addUserDto.Password, addUserDto.Role);

		if (user == null)
		{
			return BadRequest(new { message = "User already exists." });
		}

		var userDto = _mapper.Map<UserDto>(user);
		return Ok(new { message = "User added successfully", user = userDto });
	}

	[HttpDelete("delete-user/{userId}")]
	public async Task<IActionResult> DeleteUser(int userId)
	{
		if (userId <= 0)
		{
			return BadRequest(new { message = "Invalid user ID." });
		}

		// Call the service method to delete the user
		var result = await _userService.DeleteUserAsync(userId);

		if (!result)
		{
			return NotFound(new { message = "User not found." });
		}

		return Ok(new { message = "User deleted successfully." });
	}





	[HttpGet("search")]
	public async Task<IActionResult> SearchJobsByName([FromQuery] string jobTitle)
	{
		if (string.IsNullOrEmpty(jobTitle))
		{
			return BadRequest(new { message = "Please provide a job title to search." });
		}

		var jobs = await _jobService.SearchJobsByNameAsync(jobTitle);

		if (!jobs.Any())
		{
			return NotFound(new { message = "No jobs found with the given title." });
		}

		return Ok(jobs);
	}

}
