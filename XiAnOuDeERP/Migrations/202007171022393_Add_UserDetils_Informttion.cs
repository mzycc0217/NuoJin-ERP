namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserDetils_Informttion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FnishedProductMonad",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Purpose = c.String(maxLength: 255),
                        ExpectArrivalTime = c.DateTime(),
                        ApplyNumber = c.Double(),
                        QuasiPurchaseNumber = c.Double(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Enclosure = c.String(maxLength: 255),
                        ApplicantRemarks = c.String(maxLength: 255),
                        ApplyTime = c.DateTime(),
                        is_or = c.Int(nullable: false),
                        PurchaseTime = c.DateTime(),
                        WaybillNumber = c.String(maxLength: 255),
                        PurchaseContract = c.String(maxLength: 255),
                        ArrivalTime = c.DateTime(),
                        IsDelete = c.Boolean(nullable: false),
                        ApplicantId = c.Long(),
                        SupplierId = c.Long(),
                        FnishedProductId = c.Long(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.ApplicantId)
                .ForeignKey("dbo.Supplier", t => t.SupplierId)
                .ForeignKey("dbo.Z_FnishedProduct", t => t.FnishedProductId)
                .Index(t => t.ApplicantId)
                .Index(t => t.SupplierId)
                .Index(t => t.FnishedProductId);
            
            CreateTable(
                "dbo.OfficeMonad",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Purpose = c.String(maxLength: 255),
                        ExpectArrivalTime = c.DateTime(),
                        ApplyNumber = c.Double(),
                        QuasiPurchaseNumber = c.Double(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Enclosure = c.String(maxLength: 255),
                        ApplicantRemarks = c.String(maxLength: 255),
                        ApplyTime = c.DateTime(),
                        is_or = c.Int(nullable: false),
                        PurchaseTime = c.DateTime(),
                        WaybillNumber = c.String(maxLength: 255),
                        PurchaseContract = c.String(maxLength: 255),
                        ArrivalTime = c.DateTime(),
                        IsDelete = c.Boolean(nullable: false),
                        ApplicantId = c.Long(),
                        SupplierId = c.Long(),
                        OfficeId = c.Long(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.ApplicantId)
                .ForeignKey("dbo.Supplier", t => t.SupplierId)
                .ForeignKey("dbo.Z_Office", t => t.OfficeId)
                .Index(t => t.ApplicantId)
                .Index(t => t.SupplierId)
                .Index(t => t.OfficeId);
            
            CreateTable(
                "dbo.Chemistry_UserDetils",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ChemistryId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        ChemistryNumber = c.Double(nullable: false),
                        del_or = c.Boolean(nullable: false),
                        GetTime = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_Chemistry", t => t.ChemistryId)
                .Index(t => t.ChemistryId)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.FnishedProduct_UserDetils",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        FnishedProductId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        FnishedProductNumber = c.Double(nullable: false),
                        del_or = c.Boolean(nullable: false),
                        GetTime = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_FnishedProduct", t => t.FnishedProductId)
                .Index(t => t.FnishedProductId)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Office_UsrDetils",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        OfficeId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        OfficeNumber = c.Double(nullable: false),
                        del_or = c.Boolean(nullable: false),
                        GetTime = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_Office", t => t.OfficeId)
                .Index(t => t.OfficeId)
                .Index(t => t.User_id);
            
            AddColumn("dbo.Raw_UserDetils", "del_or", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Office_UsrDetils", "OfficeId", "dbo.Z_Office");
            DropForeignKey("dbo.Office_UsrDetils", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.FnishedProduct_UserDetils", "FnishedProductId", "dbo.Z_FnishedProduct");
            DropForeignKey("dbo.FnishedProduct_UserDetils", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.Chemistry_UserDetils", "ChemistryId", "dbo.Z_Chemistry");
            DropForeignKey("dbo.Chemistry_UserDetils", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.OfficeMonad", "OfficeId", "dbo.Z_Office");
            DropForeignKey("dbo.OfficeMonad", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.OfficeMonad", "ApplicantId", "dbo.UserDetails");
            DropForeignKey("dbo.FnishedProductMonad", "FnishedProductId", "dbo.Z_FnishedProduct");
            DropForeignKey("dbo.FnishedProductMonad", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.FnishedProductMonad", "ApplicantId", "dbo.UserDetails");
            DropIndex("dbo.Office_UsrDetils", new[] { "User_id" });
            DropIndex("dbo.Office_UsrDetils", new[] { "OfficeId" });
            DropIndex("dbo.FnishedProduct_UserDetils", new[] { "User_id" });
            DropIndex("dbo.FnishedProduct_UserDetils", new[] { "FnishedProductId" });
            DropIndex("dbo.Chemistry_UserDetils", new[] { "User_id" });
            DropIndex("dbo.Chemistry_UserDetils", new[] { "ChemistryId" });
            DropIndex("dbo.OfficeMonad", new[] { "OfficeId" });
            DropIndex("dbo.OfficeMonad", new[] { "SupplierId" });
            DropIndex("dbo.OfficeMonad", new[] { "ApplicantId" });
            DropIndex("dbo.FnishedProductMonad", new[] { "FnishedProductId" });
            DropIndex("dbo.FnishedProductMonad", new[] { "SupplierId" });
            DropIndex("dbo.FnishedProductMonad", new[] { "ApplicantId" });
            DropColumn("dbo.Raw_UserDetils", "del_or");
            DropTable("dbo.Office_UsrDetils");
            DropTable("dbo.FnishedProduct_UserDetils");
            DropTable("dbo.Chemistry_UserDetils");
            DropTable("dbo.OfficeMonad");
            DropTable("dbo.FnishedProductMonad");
        }
    }
}
