using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace zilver.data
{

    namespace zilver.data
    {
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                // Load appsettings.json from the main project (zilver)
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
    }

}
