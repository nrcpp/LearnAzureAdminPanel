using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.EntityFramework;

namespace LearnAzure.Admin
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }


    // Users
    [DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class UsersDbContext : IdentityDbContext<ApplicationUser>
    {
#if DEBUG 
        const string connectionString = "localhost_users";
#else
        const string connectionString = "prod_users";
#endif

        public UsersDbContext()
            : base(connectionString, throwIfV1Schema: false)
        {
        }

        public static UsersDbContext Create() => new UsersDbContext();

        static UsersDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UsersDbContext, UsersMigrationConfiguration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<IdentityRole>()
                .Property(p => p.Name)
                .HasMaxLength(255);

            modelBuilder
                .Entity<IdentityUser>()
                .Property(p => p.UserName)
                .HasMaxLength(256);

            modelBuilder
              .Properties()
              .Where(p => p.PropertyType == typeof(string) &&
                          !p.Name.Contains("Id") &&
                          !p.Name.Contains("Provider"))
              .Configure(p => p.HasMaxLength(255));

        }
    }

    public class FixedMySqlMigrationSqlGenerator : MySqlMigrationSqlGenerator
    {
        public FixedMySqlMigrationSqlGenerator()
        {
        }

        /// <summary>
        /// we want BTREE because HASH is not correct for normal Keys on MySQL 8
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        protected override MigrationStatement Generate(CreateIndexOperation op)
        {
            MigrationStatement migrationStatement = base.Generate(op);
            System.Diagnostics.Trace.WriteLine(migrationStatement.Sql);
            string fubarSql = migrationStatement.Sql.TrimEnd();

            if (fubarSql.EndsWith("using HASH", StringComparison.OrdinalIgnoreCase))
            {
                string modSql = fubarSql.Replace("using HASH", " using BTREE");
                migrationStatement.Sql = modSql;
            }
            return migrationStatement;
        }


    }

    internal class UsersMigrationConfiguration : DbMigrationsConfiguration<UsersDbContext>
    {
        public UsersMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            SetSqlGenerator("MySql.Data.MySqlClient", new FixedMySqlMigrationSqlGenerator());

            CodeGenerator = new MySql.Data.Entity.MySqlMigrationCodeGenerator();
        }
    }
}