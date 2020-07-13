namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M_basicinformation2AddAllZ_ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Z_FinshedProductType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        Dec = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Z_FnishedProduct",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 255),
                        Encoding = c.String(maxLength: 20),
                        EntryPersonId = c.Long(nullable: false),
                        WarehousingTypeId = c.Long(),
                        RawMaterialType = c.String(maxLength: 255),
                        CompanyId = c.Long(),
                        Desc = c.String(maxLength: 255),
                        IsDelete = c.Boolean(nullable: false),
                        Z_FinshedProductTypeid = c.Long(nullable: false),
                        EnglishName = c.String(maxLength: 255),
                        Abbreviation = c.String(maxLength: 255),
                        BeCommonlyCalled1 = c.String(maxLength: 255),
                        BeCommonlyCalled2 = c.String(),
                        CASNumber = c.String(maxLength: 255),
                        MolecularWeight = c.String(maxLength: 255),
                        MolecularFormula = c.String(maxLength: 255),
                        StructuralFormula = c.String(maxLength: 255),
                        AppearanceState = c.String(maxLength: 255),
                        ServiceLife = c.Double(),
                        TechnicalDescription = c.String(maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.UserDetails", t => t.EntryPersonId)
                .ForeignKey("dbo.WarehousingType", t => t.WarehousingTypeId)
                .ForeignKey("dbo.Z_FinshedProductType", t => t.Z_FinshedProductTypeid)
                .Index(t => t.EntryPersonId)
                .Index(t => t.WarehousingTypeId)
                .Index(t => t.CompanyId)
                .Index(t => t.Z_FinshedProductTypeid);
            
            CreateTable(
                "dbo.Z_MaterialCode",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 255),
                        Encoding = c.String(maxLength: 20),
                        EntryPersonId = c.Long(nullable: false),
                        WarehousingTypeId = c.Long(),
                        RawMaterialType = c.String(maxLength: 255),
                        CompanyId = c.Long(),
                        Desc = c.String(maxLength: 255),
                        IsDelete = c.Boolean(nullable: false),
                        Z_FinshedProductType = c.Long(nullable: false),
                        Z_RowTypeid = c.Long(nullable: false),
                        EnglishName = c.String(maxLength: 255),
                        Abbreviation = c.String(maxLength: 255),
                        BeCommonlyCalled1 = c.String(maxLength: 255),
                        BeCommonlyCalled2 = c.String(),
                        CASNumber = c.String(maxLength: 255),
                        MolecularWeight = c.String(maxLength: 255),
                        MolecularFormula = c.String(maxLength: 255),
                        StructuralFormula = c.String(maxLength: 255),
                        AppearanceState = c.String(maxLength: 255),
                        ServiceLife = c.Double(),
                        TechnicalDescription = c.String(maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.UserDetails", t => t.EntryPersonId)
                .ForeignKey("dbo.WarehousingType", t => t.WarehousingTypeId)
                .ForeignKey("dbo.Z_RowType", t => t.Z_RowTypeid)
                .Index(t => t.EntryPersonId)
                .Index(t => t.WarehousingTypeId)
                .Index(t => t.CompanyId)
                .Index(t => t.Z_RowTypeid);
            
            CreateTable(
                "dbo.Z_RowType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        Dec = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Z_Office",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 255),
                        Encoding = c.String(maxLength: 20),
                        EntryPersonId = c.Long(nullable: false),
                        WarehousingTypeId = c.Long(),
                        RawMaterialType = c.String(maxLength: 255),
                        CompanyId = c.Long(),
                        Desc = c.String(maxLength: 255),
                        IsDelete = c.Boolean(nullable: false),
                        Z_OfficeTypeid = c.Long(nullable: false),
                        EnglishName = c.String(maxLength: 255),
                        Abbreviation = c.String(maxLength: 255),
                        BeCommonlyCalled1 = c.String(maxLength: 255),
                        BeCommonlyCalled2 = c.String(),
                        CASNumber = c.String(maxLength: 255),
                        MolecularWeight = c.String(maxLength: 255),
                        MolecularFormula = c.String(maxLength: 255),
                        StructuralFormula = c.String(maxLength: 255),
                        AppearanceState = c.String(maxLength: 255),
                        ServiceLife = c.Double(),
                        TechnicalDescription = c.String(maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.UserDetails", t => t.EntryPersonId)
                .ForeignKey("dbo.WarehousingType", t => t.WarehousingTypeId)
                .ForeignKey("dbo.Z_OfficeType", t => t.Z_OfficeTypeid)
                .Index(t => t.EntryPersonId)
                .Index(t => t.WarehousingTypeId)
                .Index(t => t.CompanyId)
                .Index(t => t.Z_OfficeTypeid);
            
            CreateTable(
                "dbo.Z_OfficeType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        Dec = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Z_Raw",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 255),
                        Encoding = c.String(maxLength: 20),
                        EntryPersonId = c.Long(nullable: false),
                        WarehousingTypeId = c.Long(),
                        RawMaterialType = c.String(maxLength: 255),
                        CompanyId = c.Long(),
                        Desc = c.String(maxLength: 255),
                        IsDelete = c.Boolean(nullable: false),
                        Z_RowTypeid = c.Long(nullable: false),
                        EnglishName = c.String(maxLength: 255),
                        Abbreviation = c.String(maxLength: 255),
                        BeCommonlyCalled1 = c.String(maxLength: 255),
                        BeCommonlyCalled2 = c.String(),
                        CASNumber = c.String(maxLength: 255),
                        MolecularWeight = c.String(maxLength: 255),
                        MolecularFormula = c.String(maxLength: 255),
                        StructuralFormula = c.String(maxLength: 255),
                        AppearanceState = c.String(maxLength: 255),
                        ServiceLife = c.Double(),
                        TechnicalDescription = c.String(maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.UserDetails", t => t.EntryPersonId)
                .ForeignKey("dbo.WarehousingType", t => t.WarehousingTypeId)
                .ForeignKey("dbo.Z_RowType", t => t.Z_RowTypeid)
                .Index(t => t.EntryPersonId)
                .Index(t => t.WarehousingTypeId)
                .Index(t => t.CompanyId)
                .Index(t => t.Z_RowTypeid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Z_Raw", "Z_RowTypeid", "dbo.Z_RowType");
            DropForeignKey("dbo.Z_Raw", "WarehousingTypeId", "dbo.WarehousingType");
            DropForeignKey("dbo.Z_Raw", "EntryPersonId", "dbo.UserDetails");
            DropForeignKey("dbo.Z_Raw", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Z_Office", "Z_OfficeTypeid", "dbo.Z_OfficeType");
            DropForeignKey("dbo.Z_Office", "WarehousingTypeId", "dbo.WarehousingType");
            DropForeignKey("dbo.Z_Office", "EntryPersonId", "dbo.UserDetails");
            DropForeignKey("dbo.Z_Office", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Z_MaterialCode", "Z_RowTypeid", "dbo.Z_RowType");
            DropForeignKey("dbo.Z_MaterialCode", "WarehousingTypeId", "dbo.WarehousingType");
            DropForeignKey("dbo.Z_MaterialCode", "EntryPersonId", "dbo.UserDetails");
            DropForeignKey("dbo.Z_MaterialCode", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Z_FnishedProduct", "Z_FinshedProductTypeid", "dbo.Z_FinshedProductType");
            DropForeignKey("dbo.Z_FnishedProduct", "WarehousingTypeId", "dbo.WarehousingType");
            DropForeignKey("dbo.Z_FnishedProduct", "EntryPersonId", "dbo.UserDetails");
            DropForeignKey("dbo.Z_FnishedProduct", "CompanyId", "dbo.Company");
            DropIndex("dbo.Z_Raw", new[] { "Z_RowTypeid" });
            DropIndex("dbo.Z_Raw", new[] { "CompanyId" });
            DropIndex("dbo.Z_Raw", new[] { "WarehousingTypeId" });
            DropIndex("dbo.Z_Raw", new[] { "EntryPersonId" });
            DropIndex("dbo.Z_Office", new[] { "Z_OfficeTypeid" });
            DropIndex("dbo.Z_Office", new[] { "CompanyId" });
            DropIndex("dbo.Z_Office", new[] { "WarehousingTypeId" });
            DropIndex("dbo.Z_Office", new[] { "EntryPersonId" });
            DropIndex("dbo.Z_MaterialCode", new[] { "Z_RowTypeid" });
            DropIndex("dbo.Z_MaterialCode", new[] { "CompanyId" });
            DropIndex("dbo.Z_MaterialCode", new[] { "WarehousingTypeId" });
            DropIndex("dbo.Z_MaterialCode", new[] { "EntryPersonId" });
            DropIndex("dbo.Z_FnishedProduct", new[] { "Z_FinshedProductTypeid" });
            DropIndex("dbo.Z_FnishedProduct", new[] { "CompanyId" });
            DropIndex("dbo.Z_FnishedProduct", new[] { "WarehousingTypeId" });
            DropIndex("dbo.Z_FnishedProduct", new[] { "EntryPersonId" });
            DropTable("dbo.Z_Raw");
            DropTable("dbo.Z_OfficeType");
            DropTable("dbo.Z_Office");
            DropTable("dbo.Z_RowType");
            DropTable("dbo.Z_MaterialCode");
            DropTable("dbo.Z_FnishedProduct");
            DropTable("dbo.Z_FinshedProductType");
        }
    }
}
