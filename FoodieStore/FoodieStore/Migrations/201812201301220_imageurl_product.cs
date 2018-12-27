namespace FoodieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageurl_product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "ImagePath", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ImagePath", c => c.String());
            DropColumn("dbo.Products", "Description");
        }
    }
}
