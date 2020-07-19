namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Metnhd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChemistryMonad",
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
                        ChemistryId = c.Long(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.ApplicantId)
                .ForeignKey("dbo.Supplier", t => t.SupplierId)
                .ForeignKey("dbo.Z_Chemistry", t => t.ChemistryId)
                .Index(t => t.ApplicantId)
                .Index(t => t.SupplierId)
                .Index(t => t.ChemistryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChemistryMonad", "ChemistryId", "dbo.Z_Chemistry");
            DropForeignKey("dbo.ChemistryMonad", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.ChemistryMonad", "ApplicantId", "dbo.UserDetails");
            DropIndex("dbo.ChemistryMonad", new[] { "ChemistryId" });
            DropIndex("dbo.ChemistryMonad", new[] { "SupplierId" });
            DropIndex("dbo.ChemistryMonad", new[] { "ApplicantId" });
            DropTable("dbo.ChemistryMonad");
        }
    }
}
