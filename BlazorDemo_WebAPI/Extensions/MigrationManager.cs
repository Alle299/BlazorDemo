using FluentMigrator;
using FluentMigrator.Runner;
using BlazorDemo_WebAPI.Migration;

namespace BlazorDemo_WebAPI.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    databaseService.CreateDatabase("BlazorDemo_WebAPI_Database");
                       //migrationService.RollbackToVersion(1);
                  migrationService.ListMigrations();
       
                   // migrationService.Down(new InitialTables_20220405001());

                    // migrationService.MigrateDown(20220405001);
                    migrationService.MigrateUp();
                   
                }
                catch (Exception ex) 
                {
                    //log errors or ...
                    var q = ex.InnerException;
                    throw;
                }
            }
            return host;
        }
    }
}
