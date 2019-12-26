using VRFEngine.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace VRFEngine.Test.Helper
{
    public static class DataContextHelper
    {
        public static DataContext GetDataContext()
        {
            // DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
            //     .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            //     .Options;

            // DataContext dataContext = new DataContext(options);

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite("DataSource=VRFEngineDB")
                .Options;

             DataContext dataContext = new DataContext(options);
             DbInitializer.Initialize(dataContext);
            return dataContext;
        }
    }
}
