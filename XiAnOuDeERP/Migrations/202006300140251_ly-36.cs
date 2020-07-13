namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly36 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RawMaterial", "DepartmentId", "dbo.Department");
            DropIndex("dbo.RawMaterial", new[] { "DepartmentId" });
            DropColumn("dbo.RawMaterial", "DepartmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RawMaterial", "DepartmentId", c => c.Long());
            CreateIndex("dbo.RawMaterial", "DepartmentId");
            AddForeignKey("dbo.RawMaterial", "DepartmentId", "dbo.Department", "Id");
        }
    }
}
