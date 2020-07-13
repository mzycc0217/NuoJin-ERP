namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly31 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaleType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sale", "SaleTypeId", c => c.Long());
            CreateIndex("dbo.Sale", "SaleTypeId");
            AddForeignKey("dbo.Sale", "SaleTypeId", "dbo.SaleType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sale", "SaleTypeId", "dbo.SaleType");
            DropIndex("dbo.Sale", new[] { "SaleTypeId" });
            DropColumn("dbo.Sale", "SaleTypeId");
            DropTable("dbo.SaleType");
        }
    }
}
