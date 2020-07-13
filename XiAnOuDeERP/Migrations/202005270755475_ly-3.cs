namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        PreSale = c.Double(nullable: false),
                        ActualSale = c.Double(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OutOfStockId = c.Long(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsStatement = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OutOfStock", t => t.OutOfStockId)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.OutOfStockId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sale", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.Sale", "OutOfStockId", "dbo.OutOfStock");
            DropIndex("dbo.Sale", new[] { "OutOfStockId" });
            DropIndex("dbo.Sale", new[] { "UserId" });
            DropTable("dbo.Sale");
        }
    }
}
