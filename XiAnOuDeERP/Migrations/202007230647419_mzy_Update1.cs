namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product_Sale", "del_ort", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product_Sale", "del_ort");
        }
    }
}
