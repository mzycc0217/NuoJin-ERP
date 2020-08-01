namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chemistry_InRoom",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ChemistryId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        OutIutRoom = c.Double(nullable: false),
                        InenportNumber = c.Double(nullable: false),
                        is_or = c.Int(nullable: false),
                        del_or = c.Boolean(nullable: false),
                        GetTime = c.DateTime(),
                        entrepotid = c.Long(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entrepot", t => t.entrepotid)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_Chemistry", t => t.ChemistryId)
                .Index(t => t.ChemistryId)
                .Index(t => t.User_id)
                .Index(t => t.entrepotid);
            
            CreateTable(
                "dbo.FinishProduct_InRoom",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        FnishedProductId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        FnishedProductNumbers = c.Double(nullable: false),
                        del_or = c.Boolean(nullable: false),
                        GetTime = c.DateTime(),
                        is_or = c.Int(nullable: false),
                        entrepotid = c.Long(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entrepot", t => t.entrepotid)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_FnishedProduct", t => t.FnishedProductId)
                .Index(t => t.FnishedProductId)
                .Index(t => t.User_id)
                .Index(t => t.entrepotid);
            
            CreateTable(
                "dbo.Office_InRoom",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        OfficeId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        OfficeNumber = c.Double(nullable: false),
                        InenportNumber = c.Double(nullable: false),
                        OutIutRoom = c.Double(nullable: false),
                        del_or = c.Boolean(nullable: false),
                        is_or = c.Int(nullable: false),
                        GetTime = c.DateTime(),
                        entrepotid = c.Long(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entrepot", t => t.entrepotid)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_Office", t => t.OfficeId)
                .Index(t => t.OfficeId)
                .Index(t => t.User_id)
                .Index(t => t.entrepotid);
            
            CreateTable(
                "dbo.Raw_InRoom",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        RawId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        RawNumber = c.Double(nullable: false),
                        InenportNumber = c.Double(nullable: false),
                        OutIutRoom = c.Double(nullable: false),
                        del_or = c.Boolean(nullable: false),
                        is_or = c.Int(nullable: false),
                        GetRawTime = c.DateTime(),
                        entrepotid = c.Long(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entrepot", t => t.entrepotid)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_Raw", t => t.RawId)
                .Index(t => t.RawId)
                .Index(t => t.User_id)
                .Index(t => t.entrepotid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Raw_InRoom", "RawId", "dbo.Z_Raw");
            DropForeignKey("dbo.Raw_InRoom", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.Raw_InRoom", "entrepotid", "dbo.Entrepot");
            DropForeignKey("dbo.Office_InRoom", "OfficeId", "dbo.Z_Office");
            DropForeignKey("dbo.Office_InRoom", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.Office_InRoom", "entrepotid", "dbo.Entrepot");
            DropForeignKey("dbo.FinishProduct_InRoom", "FnishedProductId", "dbo.Z_FnishedProduct");
            DropForeignKey("dbo.FinishProduct_InRoom", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.FinishProduct_InRoom", "entrepotid", "dbo.Entrepot");
            DropForeignKey("dbo.Chemistry_InRoom", "ChemistryId", "dbo.Z_Chemistry");
            DropForeignKey("dbo.Chemistry_InRoom", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.Chemistry_InRoom", "entrepotid", "dbo.Entrepot");
            DropIndex("dbo.Raw_InRoom", new[] { "entrepotid" });
            DropIndex("dbo.Raw_InRoom", new[] { "User_id" });
            DropIndex("dbo.Raw_InRoom", new[] { "RawId" });
            DropIndex("dbo.Office_InRoom", new[] { "entrepotid" });
            DropIndex("dbo.Office_InRoom", new[] { "User_id" });
            DropIndex("dbo.Office_InRoom", new[] { "OfficeId" });
            DropIndex("dbo.FinishProduct_InRoom", new[] { "entrepotid" });
            DropIndex("dbo.FinishProduct_InRoom", new[] { "User_id" });
            DropIndex("dbo.FinishProduct_InRoom", new[] { "FnishedProductId" });
            DropIndex("dbo.Chemistry_InRoom", new[] { "entrepotid" });
            DropIndex("dbo.Chemistry_InRoom", new[] { "User_id" });
            DropIndex("dbo.Chemistry_InRoom", new[] { "ChemistryId" });
            DropTable("dbo.Raw_InRoom");
            DropTable("dbo.Office_InRoom");
            DropTable("dbo.FinishProduct_InRoom");
            DropTable("dbo.Chemistry_InRoom");
        }
    }
}
