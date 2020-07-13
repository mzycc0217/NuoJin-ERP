namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly35 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.User", new[] { "UserTypeId" });
            CreateTable(
                "dbo.UserDetailsType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        UserTypeId = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .ForeignKey("dbo.UserType", t => t.UserTypeId)
                .Index(t => t.UserId)
                .Index(t => t.UserTypeId);
            
            AlterColumn("dbo.User", "UserTypeId", c => c.Long());
            CreateIndex("dbo.User", "UserTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetailsType", "UserTypeId", "dbo.UserType");
            DropForeignKey("dbo.UserDetailsType", "UserId", "dbo.UserDetails");
            DropIndex("dbo.UserDetailsType", new[] { "UserTypeId" });
            DropIndex("dbo.UserDetailsType", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "UserTypeId" });
            AlterColumn("dbo.User", "UserTypeId", c => c.Long(nullable: false));
            DropTable("dbo.UserDetailsType");
            CreateIndex("dbo.User", "UserTypeId");
        }
    }
}
