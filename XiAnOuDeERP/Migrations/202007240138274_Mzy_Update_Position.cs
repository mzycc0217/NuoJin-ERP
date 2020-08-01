namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mzy_Update_Position : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Position_Correspond", "DepartmentId", "dbo.Department");
            DropIndex("dbo.Position_Correspond", new[] { "DepartmentId" });
            AddColumn("dbo.Position_Correspond", "PositionId", c => c.Long(nullable: false));
            CreateIndex("dbo.Position_Correspond", "PositionId");
            AddForeignKey("dbo.Position_Correspond", "PositionId", "dbo.Position_User", "Id");
            DropColumn("dbo.Position_Correspond", "DepartmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Position_Correspond", "DepartmentId", c => c.Long(nullable: false));
            DropForeignKey("dbo.Position_Correspond", "PositionId", "dbo.Position_User");
            DropIndex("dbo.Position_Correspond", new[] { "PositionId" });
            DropColumn("dbo.Position_Correspond", "PositionId");
            CreateIndex("dbo.Position_Correspond", "DepartmentId");
            AddForeignKey("dbo.Position_Correspond", "DepartmentId", "dbo.Department", "Id");
        }
    }
}
