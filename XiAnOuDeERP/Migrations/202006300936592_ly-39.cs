namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly39 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Supplier", new[] { "RawMaterialId" });
            AlterColumn("dbo.Supplier", "RawMaterialId", c => c.Long(nullable: false));
            CreateIndex("dbo.Supplier", "RawMaterialId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Supplier", new[] { "RawMaterialId" });
            AlterColumn("dbo.Supplier", "RawMaterialId", c => c.Long());
            CreateIndex("dbo.Supplier", "RawMaterialId");
        }
    }
}
