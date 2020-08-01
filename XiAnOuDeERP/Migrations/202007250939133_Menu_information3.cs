namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu_information3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product_Sale", "Behoof", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product_Sale", "Behoof", c => c.String(maxLength: 255));
        }
    }
}
