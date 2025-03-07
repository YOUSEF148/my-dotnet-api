using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
	public class Application
	{
		public int Id { get; set; }
		public int JobId { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public Job Job { get; set; }
		public User User { get; set; }
	}

}
