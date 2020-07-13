namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly11 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Device", new[] { "RawMaterialId" });
            AlterColumn("dbo.Device", "RawMaterialId", c => c.Long(nullable: false));
            CreateIndex("dbo.Device", "RawMaterialId");
            DropColumn("dbo.Device", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Device", "Name", c => c.String());
            DropIndex("dbo.Device", new[] { "RawMaterialId" });
            AlterColumn("dbo.Device", "RawMaterialId", c => c.Long());
            CreateIndex("dbo.Device", "RawMaterialId");
        }
    }
}
