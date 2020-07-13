namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class My_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Content_User",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ContentID = c.Long(nullable: false),
                        ContentDes = c.String(maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        Purchase_Id = c.Long(),
                        user_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchase", t => t.Purchase_Id)
                .ForeignKey("dbo.User", t => t.user_Id)
                .Index(t => t.Purchase_Id)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.User_User_Type",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        User_Type_ID = c.Long(nullable: false),
                        Type = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        user_Id = c.Long(),
                        userType_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.user_Id)
                .ForeignKey("dbo.UserType", t => t.userType_Id)
                .Index(t => t.user_Id)
                .Index(t => t.userType_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_User_Type", "userType_Id", "dbo.UserType");
            DropForeignKey("dbo.User_User_Type", "user_Id", "dbo.User");
            DropForeignKey("dbo.Content_User", "user_Id", "dbo.User");
            DropForeignKey("dbo.Content_User", "Purchase_Id", "dbo.Purchase");
            DropIndex("dbo.User_User_Type", new[] { "userType_Id" });
            DropIndex("dbo.User_User_Type", new[] { "user_Id" });
            DropIndex("dbo.Content_User", new[] { "user_Id" });
            DropIndex("dbo.Content_User", new[] { "Purchase_Id" });
            DropTable("dbo.User_User_Type");
            DropTable("dbo.Content_User");
        }
    }
}
