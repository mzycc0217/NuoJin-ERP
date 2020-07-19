namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addmzy_3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chmistry_User",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        user_Id = c.Long(nullable: false),
                        ChemistryId = c.Long(nullable: false),
                        ContentDes = c.String(maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChemistryMonad", t => t.ChemistryId)
                .ForeignKey("dbo.UserDetails", t => t.user_Id)
                .Index(t => t.user_Id)
                .Index(t => t.ChemistryId);
            
            CreateTable(
                "dbo.FinshedProduct_User",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        user_Id = c.Long(nullable: false),
                        FnishedProductId = c.Long(nullable: false),
                        ContentDes = c.String(maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FnishedProductMonad", t => t.FnishedProductId)
                .ForeignKey("dbo.UserDetails", t => t.user_Id)
                .Index(t => t.user_Id)
                .Index(t => t.FnishedProductId);
            
            CreateTable(
                "dbo.Office_User",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        user_Id = c.Long(nullable: false),
                        OfficeId = c.Long(nullable: false),
                        ContentDes = c.String(maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OfficeMonad", t => t.OfficeId)
                .ForeignKey("dbo.UserDetails", t => t.user_Id)
                .Index(t => t.user_Id)
                .Index(t => t.OfficeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Office_User", "user_Id", "dbo.UserDetails");
            DropForeignKey("dbo.Office_User", "OfficeId", "dbo.OfficeMonad");
            DropForeignKey("dbo.FinshedProduct_User", "user_Id", "dbo.UserDetails");
            DropForeignKey("dbo.FinshedProduct_User", "FnishedProductId", "dbo.FnishedProductMonad");
            DropForeignKey("dbo.Chmistry_User", "user_Id", "dbo.UserDetails");
            DropForeignKey("dbo.Chmistry_User", "ChemistryId", "dbo.ChemistryMonad");
            DropIndex("dbo.Office_User", new[] { "OfficeId" });
            DropIndex("dbo.Office_User", new[] { "user_Id" });
            DropIndex("dbo.FinshedProduct_User", new[] { "FnishedProductId" });
            DropIndex("dbo.FinshedProduct_User", new[] { "user_Id" });
            DropIndex("dbo.Chmistry_User", new[] { "ChemistryId" });
            DropIndex("dbo.Chmistry_User", new[] { "user_Id" });
            DropTable("dbo.Office_User");
            DropTable("dbo.FinshedProduct_User");
            DropTable("dbo.Chmistry_User");
        }
    }
}
