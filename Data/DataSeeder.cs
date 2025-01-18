using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Models;
using System;

namespace SimpleAPI.Data
{
    public static class DataSeeder
    {

        public static void SeedDatabase(SQLiteContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // add tables and seed data
            InitializeDatabase(context);

            // Save changes to the database
            context.SaveChanges();
        }

        public static void ExecuteSqlFile(SQLiteContext context, string filePath)
        {
            // Read the SQL file
            var sqlStatements = File.ReadAllText(filePath);

            // Execute the SQL statements
            context.Database.ExecuteSqlRaw(sqlStatements);
        }

        public static void InitializeDatabase(SQLiteContext context)
        {
            ExecuteSqlFile(context, "Data/SQL/intialTables.sql");

            // Execute seed script
            ExecuteSqlFile(context, "Data/SQL/IntialData.sql");
        }
    }

}
