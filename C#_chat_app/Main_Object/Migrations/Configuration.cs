namespace Main_Object.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Main_Object.App_Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;


        }

        protected override void Seed(Main_Object.App_Context context)
        {

            AdminUser user = new AdminUser();

            user.Register("admin", "admin", "admin@admin.com");

            //  This method will be called after migrating to the latest version.
            context.Database.ExecuteSqlCommand("UPDATE [dbo].[Users] SET AccessId=5 WHERE Username='admin'");
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            
        }
    }
}
