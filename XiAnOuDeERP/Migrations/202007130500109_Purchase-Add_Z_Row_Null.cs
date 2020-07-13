namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseAdd_Z_Row_Null : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Purchase", new[] { "RawMaterialId" });
            DropIndex("dbo.Purchase", new[] { "RawId" });
            AlterColumn("dbo.Purchase", "RawMaterialId", c => c.Long());
            AlterColumn("dbo.Purchase", "RawId", c => c.Long());
            CreateIndex("dbo.Purchase", "RawMaterialId");
            CreateIndex("dbo.Purchase", "RawId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Purchase", new[] { "RawId" });
            DropIndex("dbo.Purchase", new[] { "RawMaterialId" });
            AlterColumn("dbo.Purchase", "RawId", c => c.Long(nullable: false));
            AlterColumn("dbo.Purchase", "RawMaterialId", c => c.Long(nullable: false));
            CreateIndex("dbo.Purchase", "RawId");
            CreateIndex("dbo.Purchase", "RawMaterialId");
        }
    }
}
