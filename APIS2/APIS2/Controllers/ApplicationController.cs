using Core.Entites;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using System.Security.Claims;


[ApiController]
[Route("api/[controller]")]
public class ApplicationController : ControllerBase
{
	private readonly IApplicationService _applicationService;
	private readonly JobService _jobService;
	private readonly ILoggerFactory _logger;

	public ApplicationController(IApplicationService applicationService,JobService jobService, ILoggerFactory logger)
	{
		_applicationService = applicationService;
		_jobService = jobService;
		_logger = logger;
	}

	[HttpGet("jobs/{jobId}")]
	[Authorize]
	public async Task<IActionResult> GetJobDetails(int jobId)
	{
		var job = await _jobService.GetJobByIdAsync(jobId);
		if (job == null)
		{
			return NotFound("Job not found");
		}
		return Ok(job); // دي هترجع تفاصيل الوظيفة
	}

	[HttpPost("apply/{jobId}")]
	[Authorize]
	public async Task<IActionResult> ApplyForJob(int jobId)
	{
		// استخدم ClaimTypes.NameIdentifier بدلاً من "nameid"
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
		if (userIdClaim == null)
		{
			return Unauthorized("User ID is missing from the token.");
		}

		var userId = int.Parse(userIdClaim.Value);
		var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;  // يمكنك أيضًا الحصول على الـ email و الـ role أو أي Claim آخر

		// تسجيل المعلومات:
		var logger = _logger.CreateLogger("Application");
		logger.LogInformation($"User ID: {userId}, Email: {userEmail}");

		if (userId == 0)
		{
			return Unauthorized("User is not authenticated");
		}

		// جلب تفاصيل الوظيفة
		var job = await _jobService.GetJobByIdAsync(jobId);
		if (job == null)
		{
			return NotFound("Job not found");
		}

		// التحقق إذا كانت الوظيفة مفتوحة أو مغلقة
		if (job.Status == "Closed")
		{
			return BadRequest("This job is already closed.");
		}

		// تحديث حالة الوظيفة إلى "Closed" بعد التقديم
		job.Status = "Closed";
		await _jobService.UpdateJobAsync(job); // التحديث في الـ DB

		// تسجيل التقديم في جدول Applications
		var application = new Application
		{
			JobId = jobId,
			UserId = userId,
			CreatedAt = DateTime.UtcNow
		};

		await _applicationService.ApplyForJobAsync(application);

		return Ok("Application submitted successfully and job status is now closed.");
	}


}
