using System.Data.Entity;
using University.Model;

namespace University
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("University")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Major>      Majors      { get; set; }
        public DbSet<Student>    Students    { get; set; }
        public DbSet<Professor>  Professors  { get; set; }
        public DbSet<Subject>    Subjects    { get; set; }
    }
}
