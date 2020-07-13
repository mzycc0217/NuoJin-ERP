namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly27 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "Desc", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "Desc");
        }
    }
}
