using VRFEngine.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace VRFEngine.Data
{
    public class DataContext : DbContext
    {
        #region DbSets

        public DbSet<Field> Fields { get; set; }
        public DbSet<FieldVersion> FieldVersions { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormVersion> FormVersions { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<RuleVersion> RuleVersions { get; set; }

        #endregion

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            try
            {
                context.Database.Migrate();
            }
            catch (InvalidOperationException)
            {
                // catch exception for test in memory
                // TODO fix UnitTests on a local db
            }
            catch (Exception)
            {
                throw;
            }
            context.EnsureSeedDataForContext();
        }
    }
}
