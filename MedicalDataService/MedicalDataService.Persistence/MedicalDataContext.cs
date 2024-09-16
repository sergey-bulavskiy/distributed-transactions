using MedicalDataService.Domain;
using Microsoft.EntityFrameworkCore;

namespace MedicalDataService.Persistence;

public class MedicalDataContext : DbContext
{
    public DbSet<PatientsMedicalRecord> MedicalRecords { get; set; }

    public MedicalDataContext(DbContextOptions<MedicalDataContext> options)
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