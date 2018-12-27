namespace FoodieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Email", c => c.String());
            AlterColumn("dbo.Orders", "Phone", c => c.String());
            AlterColumn("dbo.Orders", "PostalCode", c => c.String());
            AlterColumn("dbo.Orders", "City", c => c.String());
            AlterColumn("dbo.Orders", "Address", c => c.String());
            AlterColumn("dbo.Orders", "LastName", c => c.String());
            AlterColumn("dbo.Orders", "FirstName", c => c.String());
            AlterColumn("dbo.Orders", "Username", c => c.String());
        }
    }
}
