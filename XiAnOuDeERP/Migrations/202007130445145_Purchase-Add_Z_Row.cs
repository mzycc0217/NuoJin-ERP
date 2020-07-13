namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseAdd_Z_Row : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchase", "RawId", c => c.Long(nullable: true));
            CreateIndex("dbo.Purchase", "RawId");
            AddForeignKey("dbo.Purchase", "RawId", "dbo.Z_Raw", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchase", "RawId", "dbo.Z_Raw");
            DropIndex("dbo.Purchase", new[] { "RawId" });
            DropColumn("dbo.Purchase", "RawId");
        }
    }
}
