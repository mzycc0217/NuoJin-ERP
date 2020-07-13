namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly30 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OutOfStock", "WarehouseKeeperId", "dbo.UserDetails");
            DropForeignKey("dbo.Warehousing", "WarehouseKeeperId", "dbo.UserDetails");
            DropIndex("dbo.OutOfStock", new[] { "WarehouseKeeperId" });
            DropIndex("dbo.Warehousing", new[] { "WarehouseKeeperId" });
            DropColumn("dbo.Sale", "IsStatement");
            DropColumn("dbo.OutOfStock", "WarehouseKeeperId");
            DropColumn("dbo.OutOfStock", "IsCollarUse");
            DropColumn("dbo.Warehousing", "WarehouseKeeperId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Warehousing", "WarehouseKeeperId", c => c.Long());
            AddColumn("dbo.OutOfStock", "IsCollarUse", c => c.Boolean(nullable: false));
            AddColumn("dbo.OutOfStock", "WarehouseKeeperId", c => c.Long());
            AddColumn("dbo.Sale", "IsStatement", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Warehousing", "WarehouseKeeperId");
            CreateIndex("dbo.OutOfStock", "WarehouseKeeperId");
            AddForeignKey("dbo.Warehousing", "WarehouseKeeperId", "dbo.UserDetails", "Id");
            AddForeignKey("dbo.OutOfStock", "WarehouseKeeperId", "dbo.UserDetails", "Id");
        }
    }
}
