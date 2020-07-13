namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly18 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseApproval",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        PurchaseId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UserDetails_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchase", t => t.PurchaseId)
                .ForeignKey("dbo.UserDetails", t => t.UserDetails_Id)
                .Index(t => t.PurchaseId)
                .Index(t => t.UserDetails_Id);
            
            DropColumn("dbo.Purchase", "IsReject");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchase", "IsReject", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.PurchaseApproval", "UserDetails_Id", "dbo.UserDetails");
            DropForeignKey("dbo.PurchaseApproval", "PurchaseId", "dbo.Purchase");
            DropIndex("dbo.PurchaseApproval", new[] { "UserDetails_Id" });
            DropIndex("dbo.PurchaseApproval", new[] { "PurchaseId" });
            DropTable("dbo.PurchaseApproval");
        }
    }
}
