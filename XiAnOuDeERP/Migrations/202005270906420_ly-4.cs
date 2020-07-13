namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetExpenditure",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        PurchaseId = c.Long(nullable: false),
                        Desc = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchase", t => t.PurchaseId)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PurchaseId);
            
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        DepartmentId = c.Long(nullable: false),
                        AssetsType = c.String(nullable: false),
                        TotalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Desc = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assets", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.AssetExpenditure", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.AssetExpenditure", "PurchaseId", "dbo.Purchase");
            DropIndex("dbo.Assets", new[] { "DepartmentId" });
            DropIndex("dbo.AssetExpenditure", new[] { "PurchaseId" });
            DropIndex("dbo.AssetExpenditure", new[] { "UserId" });
            DropTable("dbo.Assets");
            DropTable("dbo.AssetExpenditure");
        }
    }
}
