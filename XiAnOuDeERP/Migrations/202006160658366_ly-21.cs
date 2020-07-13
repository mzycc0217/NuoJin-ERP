namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WarehousingApproval",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        WarehousingId = c.Long(nullable: false),
                        UserId = c.Long(),
                        IsApproval = c.Boolean(nullable: false),
                        UserTypeKey = c.String(),
                        ApprovalIndex = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .ForeignKey("dbo.Warehousing", t => t.WarehousingId)
                .Index(t => t.WarehousingId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Warehousing", "ApprovalKey", c => c.String());
            AddColumn("dbo.Warehousing", "ApprovalIndex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarehousingApproval", "WarehousingId", "dbo.Warehousing");
            DropForeignKey("dbo.WarehousingApproval", "UserId", "dbo.UserDetails");
            DropIndex("dbo.WarehousingApproval", new[] { "UserId" });
            DropIndex("dbo.WarehousingApproval", new[] { "WarehousingId" });
            DropColumn("dbo.Warehousing", "ApprovalIndex");
            DropColumn("dbo.Warehousing", "ApprovalKey");
            DropTable("dbo.WarehousingApproval");
        }
    }
}
