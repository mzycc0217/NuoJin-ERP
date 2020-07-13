namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M_basicinformation2AddZ_Chemistry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Z_Chemistry",
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
                        Z_ChemistryTypeid = c.Long(nullable: false),
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
                .ForeignKey("dbo.Z_ChemistryType", t => t.Z_ChemistryTypeid)
                .Index(t => t.EntryPersonId)
                .Index(t => t.WarehousingTypeId)
                .Index(t => t.CompanyId)
                .Index(t => t.Z_ChemistryTypeid);
            
            CreateTable(
                "dbo.Z_ChemistryType",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Z_Chemistry", "Z_ChemistryTypeid", "dbo.Z_ChemistryType");
            DropForeignKey("dbo.Z_Chemistry", "WarehousingTypeId", "dbo.WarehousingType");
            DropForeignKey("dbo.Z_Chemistry", "EntryPersonId", "dbo.UserDetails");
            DropForeignKey("dbo.Z_Chemistry", "CompanyId", "dbo.Company");
            DropIndex("dbo.Z_Chemistry", new[] { "Z_ChemistryTypeid" });
            DropIndex("dbo.Z_Chemistry", new[] { "CompanyId" });
            DropIndex("dbo.Z_Chemistry", new[] { "WarehousingTypeId" });
            DropIndex("dbo.Z_Chemistry", new[] { "EntryPersonId" });
            DropTable("dbo.Z_ChemistryType");
            DropTable("dbo.Z_Chemistry");
        }
    }
}
