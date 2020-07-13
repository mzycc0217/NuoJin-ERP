namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OutOfStockApproval",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        OutOfStockId = c.Long(nullable: false),
                        UserId = c.Long(),
                        IsApproval = c.Boolean(nullable: false),
                        UserTypeKey = c.String(),
                        ApprovalIndex = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OutOfStock", t => t.OutOfStockId)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .Index(t => t.OutOfStockId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.OutOfStock", "ApprovalKey", c => c.String());
            AddColumn("dbo.OutOfStock", "ApprovalIndex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OutOfStockApproval", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.OutOfStockApproval", "OutOfStockId", "dbo.OutOfStock");
            DropIndex("dbo.OutOfStockApproval", new[] { "UserId" });
            DropIndex("dbo.OutOfStockApproval", new[] { "OutOfStockId" });
            DropColumn("dbo.OutOfStock", "ApprovalIndex");
            DropColumn("dbo.OutOfStock", "ApprovalKey");
            DropTable("dbo.OutOfStockApproval");
        }
    }
}
