namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AssetExpenditure", new[] { "PurchaseId" });
            CreateTable(
                "dbo.AssetIncome",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        SaleId = c.Long(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Desc = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sale", t => t.SaleId)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SaleId);
            
            AddColumn("dbo.AssetExpenditure", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Sale", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AssetExpenditure", "PurchaseId", c => c.Long());
            CreateIndex("dbo.AssetExpenditure", "PurchaseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssetIncome", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.AssetIncome", "SaleId", "dbo.Sale");
            DropIndex("dbo.AssetIncome", new[] { "SaleId" });
            DropIndex("dbo.AssetIncome", new[] { "UserId" });
            DropIndex("dbo.AssetExpenditure", new[] { "PurchaseId" });
            AlterColumn("dbo.AssetExpenditure", "PurchaseId", c => c.Long(nullable: false));
            DropColumn("dbo.Sale", "Amount");
            DropColumn("dbo.AssetExpenditure", "Amount");
            DropTable("dbo.AssetIncome");
            CreateIndex("dbo.AssetExpenditure", "PurchaseId");
        }
    }
}
