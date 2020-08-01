namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Tbale_Sale_zy1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product_Custorm",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Encoding = c.String(),
                        CustomName = c.String(maxLength: 40),
                        CustomCompany = c.String(maxLength: 40),
                        CustomAddress = c.String(maxLength: 40),
                        CustommePhnes = c.String(maxLength: 20),
                        CoustomPhone = c.String(maxLength: 20),
                        Business = c.String(maxLength: 200),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product_Sale",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        FishProductId = c.Long(nullable: false),
                        ProductNumber = c.Double(nullable: false),
                        Userid = c.Long(nullable: false),
                        Des = c.String(maxLength: 255),
                        QuasiPurchaseNumber = c.Double(nullable: false),
                        Behoof = c.String(maxLength: 255),
                        Sale_Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SupplierId = c.Long(),
                        Sale_Time = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Supplier", t => t.SupplierId)
                .ForeignKey("dbo.UserDetails", t => t.Userid)
                .ForeignKey("dbo.Z_FnishedProduct", t => t.FishProductId)
                .Index(t => t.FishProductId)
                .Index(t => t.Userid)
                .Index(t => t.SupplierId);
            
            AddColumn("dbo.Hostitry_Product_Price", "CustomNameId", c => c.Long(nullable: false));
            CreateIndex("dbo.Hostitry_Product_Price", "CustomNameId");
            AddForeignKey("dbo.Hostitry_Product_Price", "CustomNameId", "dbo.Product_Custorm", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product_Sale", "FishProductId", "dbo.Z_FnishedProduct");
            DropForeignKey("dbo.Product_Sale", "Userid", "dbo.UserDetails");
            DropForeignKey("dbo.Product_Sale", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.Hostitry_Product_Price", "CustomNameId", "dbo.Product_Custorm");
            DropIndex("dbo.Product_Sale", new[] { "SupplierId" });
            DropIndex("dbo.Product_Sale", new[] { "Userid" });
            DropIndex("dbo.Product_Sale", new[] { "FishProductId" });
            DropIndex("dbo.Hostitry_Product_Price", new[] { "CustomNameId" });
            DropColumn("dbo.Hostitry_Product_Price", "CustomNameId");
            DropTable("dbo.Product_Sale");
            DropTable("dbo.Product_Custorm");
        }
    }
}
