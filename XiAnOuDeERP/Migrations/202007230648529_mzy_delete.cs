namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_delete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Product_Sale", "Del_Or");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product_Sale", "Del_Or", c => c.Int(nullable: false));
        }
    }
}
