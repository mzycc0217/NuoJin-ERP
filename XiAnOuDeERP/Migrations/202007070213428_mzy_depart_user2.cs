namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_depart_user2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departent_User", "Department_Id", "dbo.Department");
            DropForeignKey("dbo.Departent_User", "UserDetails_Id", "dbo.UserDetails");
            DropIndex("dbo.Departent_User", new[] { "Department_Id" });
            DropIndex("dbo.Departent_User", new[] { "UserDetails_Id" });
            AddColumn("dbo.Departent_User", "Department_Id1", c => c.Long());
            AddColumn("dbo.Departent_User", "UserDetails_Id1", c => c.Long());
            CreateIndex("dbo.Departent_User", "Department_Id1");
            CreateIndex("dbo.Departent_User", "UserDetails_Id1");
            AddForeignKey("dbo.Departent_User", "Department_Id1", "dbo.Department", "Id");
            AddForeignKey("dbo.Departent_User", "UserDetails_Id1", "dbo.UserDetails", "Id");
            DropColumn("dbo.Departent_User", "Departrement_ID");
          //  DropColumn("dbo.Departent_User", "U_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departent_User", "U_ID", c => c.Long());
            AddColumn("dbo.Departent_User", "Departrement_ID", c => c.Long());
            DropForeignKey("dbo.Departent_User", "UserDetails_Id1", "dbo.UserDetails");
            DropForeignKey("dbo.Departent_User", "Department_Id1", "dbo.Department");
            DropIndex("dbo.Departent_User", new[] { "UserDetails_Id1" });
            DropIndex("dbo.Departent_User", new[] { "Department_Id1" });
            DropColumn("dbo.Departent_User", "UserDetails_Id1");
            DropColumn("dbo.Departent_User", "Department_Id1");
            CreateIndex("dbo.Departent_User", "UserDetails_Id");
            CreateIndex("dbo.Departent_User", "Department_Id");
            AddForeignKey("dbo.Departent_User", "UserDetails_Id", "dbo.UserDetails", "Id");
            AddForeignKey("dbo.Departent_User", "Department_Id", "dbo.Department", "Id");
        }
    }
}
