using System.ComponentModel.DataAnnotations;

namespace APIS2.DTOS
{
	public class JobDto
	{
		[Required]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public string Company { get; set; }

		[Required]
		public string Location { get; set; }

		[Required]
		[Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
		public decimal Salary { get; set; }

		[Required]
		public int EmployerId { get; set; }  // Ensure it's an int, not a string

	}


}
