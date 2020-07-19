namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FK_purs_User : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Pursh_User", new[] { "Purchase_Id1" });
            DropIndex("dbo.Pursh_User", new[] { "UserDetails_Id" });
            DropColumn("dbo.Pursh_User", "Purchase_Id");
            DropColumn("dbo.Pursh_User", "user_Id");
            RenameColumn(table: "dbo.Pursh_User", name: "Purchase_Id1", newName: "Purchase_Id");
            RenameColumn(table: "dbo.Pursh_User", name: "UserDetails_Id", newName: "user_Id");
            AlterColumn("dbo.Pursh_User", "Purchase_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Pursh_User", "user_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.Pursh_User", "user_Id");
            CreateIndex("dbo.Pursh_User", "Purchase_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pursh_User", new[] { "Purchase_Id" });
            DropIndex("dbo.Pursh_User", new[] { "user_Id" });
            AlterColumn("dbo.Pursh_User", "user_Id", c => c.Long());
            AlterColumn("dbo.Pursh_User", "Purchase_Id", c => c.Long());
            RenameColumn(table: "dbo.Pursh_User", name: "user_Id", newName: "UserDetails_Id");
            RenameColumn(table: "dbo.Pursh_User", name: "Purchase_Id", newName: "Purchase_Id1");
            AddColumn("dbo.Pursh_User", "user_Id", c => c.Long(nullable: false));
            AddColumn("dbo.Pursh_User", "Purchase_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.Pursh_User", "UserDetails_Id");
            CreateIndex("dbo.Pursh_User", "Purchase_Id1");
        }
    }
}
