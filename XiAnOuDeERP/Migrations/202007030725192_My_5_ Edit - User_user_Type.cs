namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class My_5_EditUser_user_Type : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_User_Type", "user_Id", "dbo.User");
            DropForeignKey("dbo.User_User_Type", "userType_Id", "dbo.UserType");
            DropIndex("dbo.User_User_Type", new[] { "user_Id" });
            DropIndex("dbo.User_User_Type", new[] { "userType_Id" });
            AddColumn("dbo.User_User_Type", "u_Id", c => c.Long(nullable: false));
            AddColumn("dbo.User_User_Type", "Type_id", c => c.Int(nullable: false));
            DropColumn("dbo.User_User_Type", "Type");
            DropColumn("dbo.User_User_Type", "user_Id");
            DropColumn("dbo.User_User_Type", "userType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_User_Type", "userType_Id", c => c.Long());
            AddColumn("dbo.User_User_Type", "user_Id", c => c.Long());
            AddColumn("dbo.User_User_Type", "Type", c => c.Long(nullable: false));
            DropColumn("dbo.User_User_Type", "Type_id");
            DropColumn("dbo.User_User_Type", "u_Id");
            CreateIndex("dbo.User_User_Type", "userType_Id");
            CreateIndex("dbo.User_User_Type", "user_Id");
            AddForeignKey("dbo.User_User_Type", "userType_Id", "dbo.UserType", "Id");
            AddForeignKey("dbo.User_User_Type", "user_Id", "dbo.User", "Id");
        }
    }
}
