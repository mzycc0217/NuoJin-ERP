namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly12 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Purchase", new[] { "RawMaterialId" });
            DropIndex("dbo.StorageRoom", new[] { "RawMaterialId" });
            AlterColumn("dbo.Purchase", "RawMaterialId", c => c.Long(nullable: false));
            AlterColumn("dbo.StorageRoom", "RawMaterialId", c => c.Long(nullable: false));
            AlterColumn("dbo.StorageRoom", "Number", c => c.Double(nullable: false));
            CreateIndex("dbo.Purchase", "RawMaterialId");
            CreateIndex("dbo.StorageRoom", "RawMaterialId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StorageRoom", new[] { "RawMaterialId" });
            DropIndex("dbo.Purchase", new[] { "RawMaterialId" });
            AlterColumn("dbo.StorageRoom", "Number", c => c.Double());
            AlterColumn("dbo.StorageRoom", "RawMaterialId", c => c.Long());
            AlterColumn("dbo.Purchase", "RawMaterialId", c => c.Long());
            CreateIndex("dbo.StorageRoom", "RawMaterialId");
            CreateIndex("dbo.Purchase", "RawMaterialId");
        }
    }
}
