namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcoumn_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product_Sale", "Is_Or", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product_Sale", "Is_Or");
        }
    }
}
