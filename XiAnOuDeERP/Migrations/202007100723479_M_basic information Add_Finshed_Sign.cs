namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M_basicinformationAdd_Finshed_Sign : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Z_FnishedProduct", "Finshed_Sign", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Z_FnishedProduct", "Finshed_Sign");
        }
    }
}
