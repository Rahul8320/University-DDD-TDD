using Microsoft.EntityFrameworkCore;
using University.Api.Rooms;
using University.Api.Students;

namespace University.Api.Data;

public class UniversityDbContext(DbContextOptions<UniversityDbContext> options) : DbContext(options)
{
    public required DbSet<Student> Students { get; set; }
    public required DbSet<Room> Rooms { get; set; }
}
