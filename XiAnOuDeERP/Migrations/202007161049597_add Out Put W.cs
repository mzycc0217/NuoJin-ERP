namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOutPutW : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChemistryRoom",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ChemistryId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        RawNumber = c.Double(nullable: false),
                        RawOutNumber = c.Double(nullable: false),
                        Warning_RawNumber = c.Double(nullable: false),
                        RoomDes = c.String(),
                        EntrepotId = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entrepot", t => t.EntrepotId)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_Chemistry", t => t.ChemistryId)
                .Index(t => t.ChemistryId)
                .Index(t => t.User_id)
                .Index(t => t.EntrepotId);
            
            CreateTable(
                "dbo.FnishedProductRoom",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        FnishedProductId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        RawNumber = c.Double(nullable: false),
                        RawOutNumber = c.Double(nullable: false),
                        Warning_RawNumber = c.Double(nullable: false),
                        RoomDes = c.String(),
                        EntrepotId = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entrepot", t => t.EntrepotId)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_FnishedProduct", t => t.FnishedProductId)
                .Index(t => t.FnishedProductId)
                .Index(t => t.User_id)
                .Index(t => t.EntrepotId);
            
            CreateTable(
                "dbo.OfficeRoom",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        OfficeId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        RawNumber = c.Double(nullable: false),
                        RawOutNumber = c.Double(nullable: false),
                        Warning_RawNumber = c.Double(nullable: false),
                        RoomDes = c.String(),
                        EntrepotId = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entrepot", t => t.EntrepotId)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_Office", t => t.OfficeId)
                .Index(t => t.OfficeId)
                .Index(t => t.User_id)
                .Index(t => t.EntrepotId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OfficeRoom", "OfficeId", "dbo.Z_Office");
            DropForeignKey("dbo.OfficeRoom", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.OfficeRoom", "EntrepotId", "dbo.Entrepot");
            DropForeignKey("dbo.FnishedProductRoom", "FnishedProductId", "dbo.Z_FnishedProduct");
            DropForeignKey("dbo.FnishedProductRoom", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.FnishedProductRoom", "EntrepotId", "dbo.Entrepot");
            DropForeignKey("dbo.ChemistryRoom", "ChemistryId", "dbo.Z_Chemistry");
            DropForeignKey("dbo.ChemistryRoom", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.ChemistryRoom", "EntrepotId", "dbo.Entrepot");
            DropIndex("dbo.OfficeRoom", new[] { "EntrepotId" });
            DropIndex("dbo.OfficeRoom", new[] { "User_id" });
            DropIndex("dbo.OfficeRoom", new[] { "OfficeId" });
            DropIndex("dbo.FnishedProductRoom", new[] { "EntrepotId" });
            DropIndex("dbo.FnishedProductRoom", new[] { "User_id" });
            DropIndex("dbo.FnishedProductRoom", new[] { "FnishedProductId" });
            DropIndex("dbo.ChemistryRoom", new[] { "EntrepotId" });
            DropIndex("dbo.ChemistryRoom", new[] { "User_id" });
            DropIndex("dbo.ChemistryRoom", new[] { "ChemistryId" });
            DropTable("dbo.OfficeRoom");
            DropTable("dbo.FnishedProductRoom");
            DropTable("dbo.ChemistryRoom");
        }
    }
}
