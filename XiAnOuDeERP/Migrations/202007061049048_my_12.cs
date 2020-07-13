namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class my_12 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Content_User", "user_Id", "dbo.User");
            //DropForeignKey("dbo.Departent_User", "U_ID", "dbo.User");
            //DropIndex("dbo.Content_User", new[] { "user_Id" });
            //AddColumn("dbo.Content_User", "UserDetails_Id", c => c.Long());
            //CreateIndex("dbo.Content_User", "UserDetails_Id");
            //AddForeignKey("dbo.Content_User", "UserDetails_Id", "dbo.UserDetails", "Id");
            //AddForeignKey("dbo.Departent_User", "U_ID", "dbo.UserDetails", "Id");
            //DropColumn("dbo.Content_User", "user_Id1");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Content_User", "user_Id", c => c.Long());
            //DropForeignKey("dbo.Departent_User", "U_ID", "dbo.UserDetails");
            //DropForeignKey("dbo.Content_User", "UserDetails_Id", "dbo.UserDetails");
            //DropIndex("dbo.Content_User", new[] { "UserDetails_Id" });
            //DropColumn("dbo.Content_User", "UserDetails_Id");
            //CreateIndex("dbo.Content_User", "user_Id1");
            //AddForeignKey("dbo.Departent_User", "U_ID", "dbo.User", "Id");
            //AddForeignKey("dbo.Content_User", "user_Id1", "dbo.User", "Id");
        }
    }
}
