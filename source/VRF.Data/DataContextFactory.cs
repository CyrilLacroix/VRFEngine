using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VRFEngine.Data
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite("DataSource=VRFEngineDB")
                .Options;

            return new DataContext(options); ;
        }
    }
}
