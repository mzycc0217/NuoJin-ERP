namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LeaderShip", "FinshedProductId", "dbo.Z_FnishedProduct");
            DropIndex("dbo.LeaderShip", new[] { "FinshedProductId" });
            AddColumn("dbo.LeaderShip", "Sale_Id", c => c.Long(nullable: false));
            AddColumn("dbo.Product_Sale", "Del_Or", c => c.Int(nullable: false));
            CreateIndex("dbo.LeaderShip", "Sale_Id");
            AddForeignKey("dbo.LeaderShip", "Sale_Id", "dbo.Product_Sale", "Id");
            DropColumn("dbo.LeaderShip", "FinshedProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LeaderShip", "FinshedProductId", c => c.Long(nullable: false));
            DropForeignKey("dbo.LeaderShip", "Sale_Id", "dbo.Product_Sale");
            DropIndex("dbo.LeaderShip", new[] { "Sale_Id" });
            DropColumn("dbo.Product_Sale", "Del_Or");
            DropColumn("dbo.LeaderShip", "Sale_Id");
            CreateIndex("dbo.LeaderShip", "FinshedProductId");
            AddForeignKey("dbo.LeaderShip", "FinshedProductId", "dbo.Z_FnishedProduct", "Id");
        }
    }
}
