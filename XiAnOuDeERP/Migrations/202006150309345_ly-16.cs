namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly16 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchase", "DepartmentLeaderId", "dbo.UserDetails");
            DropForeignKey("dbo.Purchase", "GeneralManagerId", "dbo.UserDetails");
            DropForeignKey("dbo.Purchase", "PurchasingSpecialistId", "dbo.UserDetails");
            DropIndex("dbo.Purchase", new[] { "ProjectId" });
            DropIndex("dbo.Purchase", new[] { "DepartmentLeaderId" });
            DropIndex("dbo.Purchase", new[] { "GeneralManagerId" });
            DropIndex("dbo.Purchase", new[] { "PurchasingSpecialistId" });
            DropIndex("dbo.RawMaterial", new[] { "EntryPersonId" });
            DropIndex("dbo.Warehousing", new[] { "PurchaseId" });
            CreateTable(
                "dbo.Approval",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Key = c.String(),
                        UserTypeKey = c.String(),
                        Deis = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RelatedApproval",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        RelatedKey = c.String(nullable: false),
                        ApprovalKey = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WarehousingType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Desc = c.String(maxLength: 255),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Purchase", "ApprovalKey", c => c.String());
            AddColumn("dbo.Purchase", "ApprovalIndex", c => c.Int(nullable: false));
            AddColumn("dbo.Purchase", "IsReject", c => c.Boolean(nullable: false));
            AddColumn("dbo.Warehousing", "RawMaterialId", c => c.Long());
            AlterColumn("dbo.Purchase", "ProjectId", c => c.Long(nullable: false));
            AlterColumn("dbo.RawMaterial", "EntryPersonId", c => c.Long(nullable: false));
            AlterColumn("dbo.Warehousing", "PurchaseId", c => c.Long());
            CreateIndex("dbo.Purchase", "ProjectId");
            CreateIndex("dbo.RawMaterial", "EntryPersonId");
            CreateIndex("dbo.Warehousing", "PurchaseId");
            CreateIndex("dbo.Warehousing", "RawMaterialId");
            AddForeignKey("dbo.Warehousing", "RawMaterialId", "dbo.RawMaterial", "Id");
            DropColumn("dbo.Purchase", "DepartmentLeaderId");
            DropColumn("dbo.Purchase", "GeneralManagerId");
            DropColumn("dbo.Purchase", "PurchasingSpecialistId");
            DropColumn("dbo.RawMaterial", "WarehousingType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RawMaterial", "WarehousingType", c => c.Int());
            AddColumn("dbo.Purchase", "PurchasingSpecialistId", c => c.Long());
            AddColumn("dbo.Purchase", "GeneralManagerId", c => c.Long());
            AddColumn("dbo.Purchase", "DepartmentLeaderId", c => c.Long());
            DropForeignKey("dbo.Warehousing", "RawMaterialId", "dbo.RawMaterial");
            DropIndex("dbo.Warehousing", new[] { "RawMaterialId" });
            DropIndex("dbo.Warehousing", new[] { "PurchaseId" });
            DropIndex("dbo.RawMaterial", new[] { "EntryPersonId" });
            DropIndex("dbo.Purchase", new[] { "ProjectId" });
            AlterColumn("dbo.Warehousing", "PurchaseId", c => c.Long(nullable: false));
            AlterColumn("dbo.RawMaterial", "EntryPersonId", c => c.Long());
            AlterColumn("dbo.Purchase", "ProjectId", c => c.Long());
            DropColumn("dbo.Warehousing", "RawMaterialId");
            DropColumn("dbo.Purchase", "IsReject");
            DropColumn("dbo.Purchase", "ApprovalIndex");
            DropColumn("dbo.Purchase", "ApprovalKey");
            DropTable("dbo.WarehousingType");
            DropTable("dbo.RelatedApproval");
            DropTable("dbo.Approval");
            CreateIndex("dbo.Warehousing", "PurchaseId");
            CreateIndex("dbo.RawMaterial", "EntryPersonId");
            CreateIndex("dbo.Purchase", "PurchasingSpecialistId");
            CreateIndex("dbo.Purchase", "GeneralManagerId");
            CreateIndex("dbo.Purchase", "DepartmentLeaderId");
            CreateIndex("dbo.Purchase", "ProjectId");
            AddForeignKey("dbo.Purchase", "PurchasingSpecialistId", "dbo.UserDetails", "Id");
            AddForeignKey("dbo.Purchase", "GeneralManagerId", "dbo.UserDetails", "Id");
            AddForeignKey("dbo.Purchase", "DepartmentLeaderId", "dbo.UserDetails", "Id");
        }
    }
}
