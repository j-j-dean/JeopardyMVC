/*********************************************************************
 * 
 * JeopardyLibrary - JeopardyContext class
 * 
 * OnConfiguring method - makes a connection to the SQL database.
 * 
 * Getters and Setters using the Entity Framework are created to reference 
 * the corresponding tables in the database (Categories, Questions, Answers)
 * 
 *********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JeopardyMVC")]

namespace JeopardyLibrary.EF
{
    /*********************************************************
     * The Entity Framework DbContext
     * ******************************************************/
    public class JeopardyContext: DbContext
    {
        public JeopardyContext(DbContextOptions options): base(options)
        {
        }

        internal JeopardyContext()
        {

        }

         /********************************************************************
         * Configure the connection string for connecting to the database
         * *****************************************************************/
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = @"server=(local)\SQLEXPRESS;database=Jeopardy;
                          integrated security=True; MultipleActiveResultSets=True;
                          App=EntityFramework;";
                optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.
                    QueryPossibleUnintendedUseOfEqualsWarning));
            }
        }
         

        /********************************************************************
         * Create Entity Framework table references to the corresponding
         * tables in the database (Categories, Questions, Answers)
         * *****************************************************************/
        public DbSet<JeopardyLibrary.Models.Categories> Categories { get; set; }
        public DbSet<JeopardyLibrary.Models.Questions> Questions { get; set; }
        public DbSet<JeopardyLibrary.Models.Answers> Answers { get; set; }

    }
}
