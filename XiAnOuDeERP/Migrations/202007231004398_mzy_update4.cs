namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_update4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Position_User",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        PositionName = c.String(),
                        PositionDes = c.String(),
                        Order = c.String(),
                        del_Or = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Position_Correspond",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        DepartmentId = c.Long(nullable: false),
                        Sign = c.Int(),
                        del_Or = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .Index(t => t.User_id)
                .Index(t => t.DepartmentId);
            
            AddColumn("dbo.UserDetails", "PositionId", c => c.Long());
            CreateIndex("dbo.UserDetails", "PositionId");
            AddForeignKey("dbo.UserDetails", "PositionId", "dbo.Position_User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Position_Correspond", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.Position_Correspond", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.UserDetails", "PositionId", "dbo.Position_User");
            DropIndex("dbo.Position_Correspond", new[] { "DepartmentId" });
            DropIndex("dbo.Position_Correspond", new[] { "User_id" });
            DropIndex("dbo.UserDetails", new[] { "PositionId" });
            DropColumn("dbo.UserDetails", "PositionId");
            DropTable("dbo.Position_Correspond");
            DropTable("dbo.Position_User");
        }
    }
}
