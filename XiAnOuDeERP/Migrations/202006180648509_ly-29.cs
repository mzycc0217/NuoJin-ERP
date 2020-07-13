namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly29 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersonnelRts", "ReviewedById", "dbo.UserDetails");
            DropIndex("dbo.PersonnelRts", new[] { "ReviewedById" });
            CreateTable(
                "dbo.PersonnelRtsApproval",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        PersonnelRtsId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        IsApproval = c.Boolean(nullable: false),
                        UserTypeKey = c.String(),
                        ApprovalIndex = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonnelRts", t => t.PersonnelRtsId)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .Index(t => t.PersonnelRtsId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.PersonnelRts", "ApprovalKey", c => c.String());
            AddColumn("dbo.PersonnelRts", "ApprovalIndex", c => c.Int(nullable: false));
            DropColumn("dbo.PersonnelRts", "ReviewedById");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonnelRts", "ReviewedById", c => c.Long());
            DropForeignKey("dbo.PersonnelRtsApproval", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.PersonnelRtsApproval", "PersonnelRtsId", "dbo.PersonnelRts");
            DropIndex("dbo.PersonnelRtsApproval", new[] { "UserId" });
            DropIndex("dbo.PersonnelRtsApproval", new[] { "PersonnelRtsId" });
            DropColumn("dbo.PersonnelRts", "ApprovalIndex");
            DropColumn("dbo.PersonnelRts", "ApprovalKey");
            DropTable("dbo.PersonnelRtsApproval");
            CreateIndex("dbo.PersonnelRts", "ReviewedById");
            AddForeignKey("dbo.PersonnelRts", "ReviewedById", "dbo.UserDetails", "Id");
        }
    }
}
