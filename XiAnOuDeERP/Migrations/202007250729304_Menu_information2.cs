namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu_information2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Menu_Ju", "userDetails_Id", "dbo.UserDetails");
            DropIndex("dbo.Menu_Ju", new[] { "userDetails_Id" });
            DropIndex("dbo.Menu_Ju", new[] { "z_Menu_Id" });
            DropColumn("dbo.Menu_Ju", "Menu_Id");
            RenameColumn(table: "dbo.Menu_Ju", name: "z_Menu_Id", newName: "Menu_Id");
            AddColumn("dbo.Menu_Ju", "userType_Id", c => c.Long(nullable: false));
            AddColumn("dbo.Z_Menu", "Del_or", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Menu_Ju", "Menu_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.Menu_Ju", "userType_Id");
            CreateIndex("dbo.Menu_Ju", "Menu_Id");
            AddForeignKey("dbo.Menu_Ju", "userType_Id", "dbo.UserType", "Id");
            DropColumn("dbo.Menu_Ju", "user_Id");
            DropColumn("dbo.Menu_Ju", "userDetails_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menu_Ju", "userDetails_Id", c => c.Long());
            AddColumn("dbo.Menu_Ju", "user_Id", c => c.Long(nullable: false));
            DropForeignKey("dbo.Menu_Ju", "userType_Id", "dbo.UserType");
            DropIndex("dbo.Menu_Ju", new[] { "Menu_Id" });
            DropIndex("dbo.Menu_Ju", new[] { "userType_Id" });
            AlterColumn("dbo.Menu_Ju", "Menu_Id", c => c.Long());
            DropColumn("dbo.Z_Menu", "Del_or");
            DropColumn("dbo.Menu_Ju", "userType_Id");
            RenameColumn(table: "dbo.Menu_Ju", name: "Menu_Id", newName: "z_Menu_Id");
            AddColumn("dbo.Menu_Ju", "Menu_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.Menu_Ju", "z_Menu_Id");
            CreateIndex("dbo.Menu_Ju", "userDetails_Id");
            AddForeignKey("dbo.Menu_Ju", "userDetails_Id", "dbo.UserDetails", "Id");
        }
    }
}
