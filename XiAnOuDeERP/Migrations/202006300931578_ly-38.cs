namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly38 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplier", "RawMaterialId", c => c.Long());
            AddColumn("dbo.Supplier", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Supplier", "RawMaterialId");
            AddForeignKey("dbo.Supplier", "RawMaterialId", "dbo.RawMaterial", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supplier", "RawMaterialId", "dbo.RawMaterial");
            DropIndex("dbo.Supplier", new[] { "RawMaterialId" });
            DropColumn("dbo.Supplier", "Price");
            DropColumn("dbo.Supplier", "RawMaterialId");
        }
    }
}
