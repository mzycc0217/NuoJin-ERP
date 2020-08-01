namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class information1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menu_Ju",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        user_Id = c.Long(nullable: false),
                        Menu_Id = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        userDetails_Id = c.Long(),
                        z_Menu_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.userDetails_Id)
                .ForeignKey("dbo.Z_Menu", t => t.z_Menu_Id)
                .Index(t => t.userDetails_Id)
                .Index(t => t.z_Menu_Id);
            
            CreateTable(
                "dbo.Z_Menu",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        name = c.String(),
                        icon = c.String(),
                        url = c.String(),
                        pid = c.Long(nullable: false),
                        Order = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menu_Ju", "z_Menu_Id", "dbo.Z_Menu");
            DropForeignKey("dbo.Menu_Ju", "userDetails_Id", "dbo.UserDetails");
            DropIndex("dbo.Menu_Ju", new[] { "z_Menu_Id" });
            DropIndex("dbo.Menu_Ju", new[] { "userDetails_Id" });
            DropTable("dbo.Z_Menu");
            DropTable("dbo.Menu_Ju");
        }
    }
}
