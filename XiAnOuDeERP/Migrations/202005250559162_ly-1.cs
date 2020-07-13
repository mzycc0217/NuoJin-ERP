namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "UserTypeId", c => c.Long());
            AddColumn("dbo.UserElement", "UserTypeId", c => c.Long(nullable: false));
            AddColumn("dbo.UserMenu", "UserTypeId", c => c.Long(nullable: false));
            AddColumn("dbo.UserModule", "UserTypeId", c => c.Long(nullable: false));
            CreateIndex("dbo.User", "UserTypeId");
            CreateIndex("dbo.UserElement", "UserTypeId");
            CreateIndex("dbo.UserMenu", "UserTypeId");
            CreateIndex("dbo.UserModule", "UserTypeId");
            AddForeignKey("dbo.User", "UserTypeId", "dbo.UserType", "Id");
            AddForeignKey("dbo.UserElement", "UserTypeId", "dbo.UserType", "Id");
            AddForeignKey("dbo.UserMenu", "UserTypeId", "dbo.UserType", "Id");
            AddForeignKey("dbo.UserModule", "UserTypeId", "dbo.UserType", "Id");
            DropColumn("dbo.User", "UserType");
            DropColumn("dbo.UserElement", "UserType");
            DropColumn("dbo.UserMenu", "UserType");
            DropColumn("dbo.UserModule", "UserType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserModule", "UserType", c => c.Int(nullable: false));
            AddColumn("dbo.UserMenu", "UserType", c => c.Int(nullable: false));
            AddColumn("dbo.UserElement", "UserType", c => c.Int(nullable: false));
            AddColumn("dbo.User", "UserType", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserModule", "UserTypeId", "dbo.UserType");
            DropForeignKey("dbo.UserMenu", "UserTypeId", "dbo.UserType");
            DropForeignKey("dbo.UserElement", "UserTypeId", "dbo.UserType");
            DropForeignKey("dbo.User", "UserTypeId", "dbo.UserType");
            DropIndex("dbo.UserModule", new[] { "UserTypeId" });
            DropIndex("dbo.UserMenu", new[] { "UserTypeId" });
            DropIndex("dbo.UserElement", new[] { "UserTypeId" });
            DropIndex("dbo.User", new[] { "UserTypeId" });
            DropColumn("dbo.UserModule", "UserTypeId");
            DropColumn("dbo.UserMenu", "UserTypeId");
            DropColumn("dbo.UserElement", "UserTypeId");
            DropColumn("dbo.User", "UserTypeId");
            DropTable("dbo.UserType");
        }
    }
}
