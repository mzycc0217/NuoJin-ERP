namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RawMaterial", "Desc", c => c.String(maxLength: 255));
            AddColumn("dbo.RawMaterial", "ServiceLife", c => c.Double(nullable: false));
            AddColumn("dbo.RawMaterial", "TechnicalDescription", c => c.String(maxLength: 255));
            AddColumn("dbo.Device", "RawMaterialId", c => c.Long());
            CreateIndex("dbo.Device", "RawMaterialId");
            AddForeignKey("dbo.Device", "RawMaterialId", "dbo.RawMaterial", "Id");
            DropColumn("dbo.Device", "ServiceLife");
            DropColumn("dbo.Device", "TechnicalDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Device", "TechnicalDescription", c => c.String(maxLength: 255));
            AddColumn("dbo.Device", "ServiceLife", c => c.Double(nullable: false));
            DropForeignKey("dbo.Device", "RawMaterialId", "dbo.RawMaterial");
            DropIndex("dbo.Device", new[] { "RawMaterialId" });
            DropColumn("dbo.Device", "RawMaterialId");
            DropColumn("dbo.RawMaterial", "TechnicalDescription");
            DropColumn("dbo.RawMaterial", "ServiceLife");
            DropColumn("dbo.RawMaterial", "Desc");
        }
    }
}
