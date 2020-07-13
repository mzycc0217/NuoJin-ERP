namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly37 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RawMaterial", "WarehouseKeeperId", "dbo.UserDetails");
            DropIndex("dbo.RawMaterial", new[] { "WarehouseKeeperId" });
            AddColumn("dbo.StorageRoom", "WarehouseKeeperId", c => c.Long());
            CreateIndex("dbo.StorageRoom", "WarehouseKeeperId");
            AddForeignKey("dbo.StorageRoom", "WarehouseKeeperId", "dbo.UserDetails", "Id");
            DropColumn("dbo.RawMaterial", "WarehouseKeeperId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RawMaterial", "WarehouseKeeperId", c => c.Long());
            DropForeignKey("dbo.StorageRoom", "WarehouseKeeperId", "dbo.UserDetails");
            DropIndex("dbo.StorageRoom", new[] { "WarehouseKeeperId" });
            DropColumn("dbo.StorageRoom", "WarehouseKeeperId");
            CreateIndex("dbo.RawMaterial", "WarehouseKeeperId");
            AddForeignKey("dbo.RawMaterial", "WarehouseKeeperId", "dbo.UserDetails", "Id");
        }
    }
}
