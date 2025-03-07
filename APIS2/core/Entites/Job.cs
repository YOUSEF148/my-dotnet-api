using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{

	public class Job
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Company { get; set; }
		public string Location { get; set; }
		public decimal Salary { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public int EmployerId { get; set; } // Foreign key to User (Employer)
		public string? Status { get; set; } // Open or Closed, nullable for existing data
	}
}

