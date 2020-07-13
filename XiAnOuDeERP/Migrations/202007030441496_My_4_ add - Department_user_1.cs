namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class My_4_addDepartment_user_1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Departent_User", "Departrement_ID");
            CreateIndex("dbo.Departent_User", "U_ID");
            AddForeignKey("dbo.Departent_User", "Departrement_ID", "dbo.Department", "Id");
            AddForeignKey("dbo.Departent_User", "U_ID", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departent_User", "U_ID", "dbo.User");
            DropForeignKey("dbo.Departent_User", "Departrement_ID", "dbo.Department");
            DropIndex("dbo.Departent_User", new[] { "U_ID" });
            DropIndex("dbo.Departent_User", new[] { "Departrement_ID" });
        }
    }
}
