namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class information : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hostitry_Product_Price", "del_Or", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hostitry_Product_Price", "del_Or", c => c.Double(nullable: false));
        }
    }
}
