namespace Main_Object.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAnnot : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MessageDTOes", "SenderName", c => c.String(nullable: false));
            AlterColumn("dbo.MessageDTOes", "ReceiverName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.Users", "Salt", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Salt", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String(maxLength: 15));
            AlterColumn("dbo.MessageDTOes", "ReceiverName", c => c.String());
            AlterColumn("dbo.MessageDTOes", "SenderName", c => c.String());
        }
    }
}
