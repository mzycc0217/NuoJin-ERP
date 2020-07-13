namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_depart_user : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departent_User", "Departrement_ID", "dbo.Department");
            DropForeignKey("dbo.Departent_User", "U_ID", "dbo.UserDetails");
            DropIndex("dbo.Departent_User", new[] { "Departrement_ID" });
            DropIndex("dbo.Departent_User", new[] { "U_ID" });
            AddColumn("dbo.Departent_User", "Department_Id", c => c.Long());
            AddColumn("dbo.Departent_User", "UserDetails_Id", c => c.Long());
            CreateIndex("dbo.Departent_User", "Department_Id");
            CreateIndex("dbo.Departent_User", "UserDetails_Id");
            AddForeignKey("dbo.Departent_User", "Department_Id", "dbo.Department", "Id");
            AddForeignKey("dbo.Departent_User", "UserDetails_Id", "dbo.UserDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departent_User", "UserDetails_Id", "dbo.UserDetails");
            DropForeignKey("dbo.Departent_User", "Department_Id", "dbo.Department");
            DropIndex("dbo.Departent_User", new[] { "UserDetails_Id" });
            DropIndex("dbo.Departent_User", new[] { "Department_Id" });
            DropColumn("dbo.Departent_User", "UserDetails_Id");
            DropColumn("dbo.Departent_User", "Department_Id");
            CreateIndex("dbo.Departent_User", "U_ID");
            CreateIndex("dbo.Departent_User", "Departrement_ID");
            AddForeignKey("dbo.Departent_User", "U_ID", "dbo.UserDetails", "Id");
            AddForeignKey("dbo.Departent_User", "Departrement_ID", "dbo.Department", "Id");
        }
    }
}
