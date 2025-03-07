using Core.Entites;

using Microsoft.EntityFrameworkCore;

namespace Repository.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<User> Users { get; set; }  // Represents the 'Users' table in the database
	public DbSet<Job> Jobs { get; set; }    // Represents the 'Jobs' table in the database
	public DbSet<Application> Applications { get; set; } // Represents the 'Applications' table
}
