namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_startAnddel_or : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Raw_UserDetils", "is_or", c => c.Int(nullable: false));
            AddColumn("dbo.RawRoom", "del_or", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RawRoom", "del_or");
            DropColumn("dbo.Raw_UserDetils", "is_or");
        }
    }
}
