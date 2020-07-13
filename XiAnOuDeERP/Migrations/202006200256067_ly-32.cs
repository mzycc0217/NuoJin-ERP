namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly32 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OutOfStockType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.OutOfStock", "OutOfStockTypeId", c => c.Long());
            CreateIndex("dbo.OutOfStock", "OutOfStockTypeId");
            AddForeignKey("dbo.OutOfStock", "OutOfStockTypeId", "dbo.OutOfStockType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OutOfStock", "OutOfStockTypeId", "dbo.OutOfStockType");
            DropIndex("dbo.OutOfStock", new[] { "OutOfStockTypeId" });
            DropColumn("dbo.OutOfStock", "OutOfStockTypeId");
            DropTable("dbo.OutOfStockType");
        }
    }
}
