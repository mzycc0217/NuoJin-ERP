namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M_basicinformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Z_Supplies",
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
                        Z_SuppliesTypeid = c.Long(nullable: false),
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
                .ForeignKey("dbo.Z_SuppliesType", t => t.Z_SuppliesTypeid)
                .Index(t => t.EntryPersonId)
                .Index(t => t.WarehousingTypeId)
                .Index(t => t.CompanyId)
                .Index(t => t.Z_SuppliesTypeid);
            
            CreateTable(
                "dbo.Z_SuppliesType",
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
            DropForeignKey("dbo.Z_Supplies", "Z_SuppliesTypeid", "dbo.Z_SuppliesType");
            DropForeignKey("dbo.Z_Supplies", "WarehousingTypeId", "dbo.WarehousingType");
            DropForeignKey("dbo.Z_Supplies", "EntryPersonId", "dbo.UserDetails");
            DropForeignKey("dbo.Z_Supplies", "CompanyId", "dbo.Company");
            DropIndex("dbo.Z_Supplies", new[] { "Z_SuppliesTypeid" });
            DropIndex("dbo.Z_Supplies", new[] { "CompanyId" });
            DropIndex("dbo.Z_Supplies", new[] { "WarehousingTypeId" });
            DropIndex("dbo.Z_Supplies", new[] { "EntryPersonId" });
            DropTable("dbo.Z_SuppliesType");
            DropTable("dbo.Z_Supplies");
        }
    }
}
