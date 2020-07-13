namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly19 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PurchaseApproval", new[] { "UserDetails_Id" });
            DropColumn("dbo.PurchaseApproval", "UserId");
            RenameColumn(table: "dbo.PurchaseApproval", name: "UserDetails_Id", newName: "UserId");
            AddColumn("dbo.PurchaseApproval", "IsApproval", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchaseApproval", "UserTypeKey", c => c.String());
            AddColumn("dbo.PurchaseApproval", "ApprovalIndex", c => c.Int(nullable: false));
            AlterColumn("dbo.PurchaseApproval", "UserId", c => c.Long());
            CreateIndex("dbo.PurchaseApproval", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PurchaseApproval", new[] { "UserId" });
            AlterColumn("dbo.PurchaseApproval", "UserId", c => c.Long(nullable: false));
            DropColumn("dbo.PurchaseApproval", "ApprovalIndex");
            DropColumn("dbo.PurchaseApproval", "UserTypeKey");
            DropColumn("dbo.PurchaseApproval", "IsApproval");
            RenameColumn(table: "dbo.PurchaseApproval", name: "UserId", newName: "UserDetails_Id");
            AddColumn("dbo.PurchaseApproval", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.PurchaseApproval", "UserDetails_Id");
        }
    }
}
