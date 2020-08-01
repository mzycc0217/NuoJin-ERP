namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_AddCom2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FnishedProductMonad", "Finshed_Sign", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FnishedProductMonad", "Finshed_Sign");
        }
    }
}
