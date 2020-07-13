namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.User", new[] { "DepartmentId" });
            CreateTable(
                "dbo.DeviceRepair",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        DeviceId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        Desc = c.String(maxLength: 255),
                        ApprovalType = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .Index(t => t.DeviceId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        Usage = c.String(maxLength: 255),
                        ServiceLife = c.Double(nullable: false),
                        TechnicalDescription = c.String(maxLength: 255),
                        IsScrap = c.Boolean(nullable: false),
                        DepartmentId = c.Long(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .ForeignKey("dbo.UserDetails", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.DepartmentId);
            
            AlterColumn("dbo.User", "DepartmentId", c => c.Long(nullable: false));
            CreateIndex("dbo.User", "DepartmentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceRepair", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.DeviceRepair", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.Device", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.Device", "DepartmentId", "dbo.Department");
            DropIndex("dbo.User", new[] { "DepartmentId" });
            DropIndex("dbo.Device", new[] { "DepartmentId" });
            DropIndex("dbo.Device", new[] { "UserId" });
            DropIndex("dbo.DeviceRepair", new[] { "UserId" });
            DropIndex("dbo.DeviceRepair", new[] { "DeviceId" });
            AlterColumn("dbo.User", "DepartmentId", c => c.Long());
            DropTable("dbo.Device");
            DropTable("dbo.DeviceRepair");
            CreateIndex("dbo.User", "DepartmentId");
        }
    }
}
