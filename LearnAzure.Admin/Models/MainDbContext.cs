using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LearnAzure.Admin
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MainDbContext : DbContext
    {        
#if DEBUG 
        static string connectionStringName = "localhost";
#else
        static string connectionStringName = "prod";
#endif

        public DbSet<Exam> Exams { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<LearningContent> LearningContents { get; set; }
        public DbSet<LearningArticle> LearningArticles { get; set; }
        public DbSet<LearningVideo> LearningVideos { get; set; }
        public DbSet<LearningPracticeLab> LearningPracticeLabs { get; set; }
        public DbSet<Question> Questions { get; set; }


        public MainDbContext() : base(connectionStringName)
        {                        
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainDbContext, MigrateDBConfiguration>());
        }
    }


    public class MigrateDBConfiguration : System.Data.Entity.Migrations.DbMigrationsConfiguration<MainDbContext>
    {
        public MigrateDBConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}