namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RawMaterial", "WarehousingTypeId", c => c.Long());
            CreateIndex("dbo.RawMaterial", "WarehousingTypeId");
            AddForeignKey("dbo.RawMaterial", "WarehousingTypeId", "dbo.WarehousingType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RawMaterial", "WarehousingTypeId", "dbo.WarehousingType");
            DropIndex("dbo.RawMaterial", new[] { "WarehousingTypeId" });
            DropColumn("dbo.RawMaterial", "WarehousingTypeId");
        }
    }
}
