namespace Exercise4_Ver2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWeekSalaryToFloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workers", "WeekSalary", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workers", "WeekSalary", c => c.Int(nullable: false));
        }
    }
}
