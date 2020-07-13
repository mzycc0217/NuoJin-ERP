namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetsType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Assets", "AssetsTypeId", c => c.Long(nullable: false));
            AddColumn("dbo.Assets", "Number", c => c.Double(nullable: false));
            AddColumn("dbo.Assets", "CompanyId", c => c.Long(nullable: false));
            CreateIndex("dbo.Assets", "AssetsTypeId");
            CreateIndex("dbo.Assets", "CompanyId");
            AddForeignKey("dbo.Assets", "AssetsTypeId", "dbo.AssetsType", "Id");
            AddForeignKey("dbo.Assets", "CompanyId", "dbo.Company", "Id");
            DropColumn("dbo.Assets", "AssetsType");
            DropColumn("dbo.Assets", "TotalValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assets", "TotalValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Assets", "AssetsType", c => c.String(nullable: false));
            DropForeignKey("dbo.Assets", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Assets", "AssetsTypeId", "dbo.AssetsType");
            DropIndex("dbo.Assets", new[] { "CompanyId" });
            DropIndex("dbo.Assets", new[] { "AssetsTypeId" });
            DropColumn("dbo.Assets", "CompanyId");
            DropColumn("dbo.Assets", "Number");
            DropColumn("dbo.Assets", "AssetsTypeId");
            DropTable("dbo.AssetsType");
        }
    }
}
