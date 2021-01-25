/*********************************************************************
 * 
 * JeopardyLibrary - JeopardyContextFactory class
 * 
 * This is used to set the database onnection string when performing 
 * migrations using Entity Framework.
 * 
 *********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace JeopardyLibrary.EF
{
    public class JeopardyContextFactory : IDesignTimeDbContextFactory<JeopardyContext>
    {
        /*********************************************************************
         * This CreateDbContext sets the connection string to use for
         * Entity Framework database migrations
         * ******************************************************************/
        public JeopardyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JeopardyContext>();
            var connectionString = @"server=(local)\SQLEXPRESS;database=Jeopardy;
                          integrated security=True; MultipleActiveResultSets=True;
                          App=EntityFramework;";
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
                .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.
                QueryPossibleUnintendedUseOfEqualsWarning));
            return new JeopardyContext(optionsBuilder.Options);
        }
    }
}
