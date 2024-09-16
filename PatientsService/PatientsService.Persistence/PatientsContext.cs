using Microsoft.EntityFrameworkCore;
using PatientsService.Domain;

namespace PatientsService.Persistence;

public class PatientsContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }

    public PatientsContext(DbContextOptions<PatientsContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}