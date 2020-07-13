namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly28 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leave", "ApprovelId", "dbo.UserDetails");
            DropIndex("dbo.Leave", new[] { "ApprovelId" });
            CreateTable(
                "dbo.LeaveApproval",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        LeaveId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        IsApproval = c.Boolean(nullable: false),
                        UserTypeKey = c.String(),
                        ApprovalIndex = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leave", t => t.LeaveId)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .Index(t => t.LeaveId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Leave", "ApprovalKey", c => c.String());
            AddColumn("dbo.Leave", "ApprovalIndex", c => c.Int(nullable: false));
            DropColumn("dbo.Leave", "ApprovelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leave", "ApprovelId", c => c.Long());
            DropForeignKey("dbo.LeaveApproval", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.LeaveApproval", "LeaveId", "dbo.Leave");
            DropIndex("dbo.LeaveApproval", new[] { "UserId" });
            DropIndex("dbo.LeaveApproval", new[] { "LeaveId" });
            DropColumn("dbo.Leave", "ApprovalIndex");
            DropColumn("dbo.Leave", "ApprovalKey");
            DropTable("dbo.LeaveApproval");
            CreateIndex("dbo.Leave", "ApprovelId");
            AddForeignKey("dbo.Leave", "ApprovelId", "dbo.UserDetails", "Id");
        }
    }
}
