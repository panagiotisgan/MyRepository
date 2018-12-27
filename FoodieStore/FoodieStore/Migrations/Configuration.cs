namespace FoodieStore.Migrations
{
    using FoodieStore.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FoodieStore.App_Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FoodieStore.App_Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Roles.AddOrUpdate(
                new Role() { RoleId=1, Rolename = "Admin"},
                new Role() { RoleId=2, Rolename = "User"},
                new Role() { RoleId=3, Rolename = "Banned" });

            context.Categories.AddOrUpdate(
                new Category() { CategoryId=1, CategoryName = "Drinks"},
                new Category() { CategoryId=2, CategoryName = "Dishes"},
                new Category() { CategoryId=3, CategoryName = "Starters"});
        }
    }
}
