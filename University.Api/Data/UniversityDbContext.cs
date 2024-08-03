using Microsoft.EntityFrameworkCore;
using University.Api.Entities;

namespace University.Api.Data;

public class UniversityDbContext(DbContextOptions<UniversityDbContext> options) : DbContext(options)
{
    public required DbSet<Student> Students { get; set; }
}
