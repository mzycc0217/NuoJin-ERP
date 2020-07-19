namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchase", "PurchaseAmount", c => c.Double());
            AddColumn("dbo.ChemistryMonad", "PurchaseAmount", c => c.Double());
            AddColumn("dbo.FnishedProductMonad", "PurchaseAmount", c => c.Double());
            AddColumn("dbo.OfficeMonad", "PurchaseAmount", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OfficeMonad", "PurchaseAmount");
            DropColumn("dbo.FnishedProductMonad", "PurchaseAmount");
            DropColumn("dbo.ChemistryMonad", "PurchaseAmount");
            DropColumn("dbo.Purchase", "PurchaseAmount");
        }
    }
}
