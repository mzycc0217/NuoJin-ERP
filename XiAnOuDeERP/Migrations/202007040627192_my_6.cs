namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class my_6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_User_Type", "user_Id", c => c.Long());
            AddColumn("dbo.User_User_Type", "userType_Id", c => c.Long());
            CreateIndex("dbo.User_User_Type", "user_Id");
            CreateIndex("dbo.User_User_Type", "userType_Id");
            AddForeignKey("dbo.User_User_Type", "user_Id", "dbo.User", "Id");
            AddForeignKey("dbo.User_User_Type", "userType_Id", "dbo.UserType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_User_Type", "userType_Id", "dbo.UserType");
            DropForeignKey("dbo.User_User_Type", "user_Id", "dbo.User");
            DropIndex("dbo.User_User_Type", new[] { "userType_Id" });
            DropIndex("dbo.User_User_Type", new[] { "user_Id" });
            DropColumn("dbo.User_User_Type", "userType_Id");
            DropColumn("dbo.User_User_Type", "user_Id");
        }
    }
}
