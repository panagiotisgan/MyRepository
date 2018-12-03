namespace Main_Object.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Arxiko : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageDTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderName = c.String(),
                        ReceiverName = c.String(),
                        Message = c.String(nullable: false, maxLength: 250),
                        DateTime = c.DateTime(nullable: false),
                        Receiver_Id = c.Int(),
                        Sender_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Receiver_Id)
                .ForeignKey("dbo.Users", t => t.Sender_Id)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessId = c.Int(nullable: false),
                        Username = c.String(maxLength: 15),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageDTOes", "Sender_Id", "dbo.Users");
            DropForeignKey("dbo.MessageDTOes", "Receiver_Id", "dbo.Users");
            DropIndex("dbo.MessageDTOes", new[] { "Sender_Id" });
            DropIndex("dbo.MessageDTOes", new[] { "Receiver_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.MessageDTOes");
        }
    }
}
