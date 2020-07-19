namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_three_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entrepot",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        EntrepotName = c.String(maxLength: 20),
                        EntrepotDes = c.String(maxLength: 225),
                        EntrepotAddress = c.String(maxLength: 225),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Raw_UserDetils",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        RawId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        RawNumber = c.Single(nullable: false),
                        GetRawTime = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_Raw", t => t.RawId)
                .Index(t => t.RawId)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.RawRoom",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        RawId = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        RawNumber = c.Single(nullable: false),
                        RawOutNumber = c.Single(nullable: false),
                        Warning_RawNumber = c.Single(nullable: false),
                        RoomDes = c.String(),
                        EntrepotId = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entrepot", t => t.EntrepotId)
                .ForeignKey("dbo.UserDetails", t => t.User_id)
                .ForeignKey("dbo.Z_Raw", t => t.RawId)
                .Index(t => t.RawId)
                .Index(t => t.User_id)
                .Index(t => t.EntrepotId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RawRoom", "RawId", "dbo.Z_Raw");
            DropForeignKey("dbo.RawRoom", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.RawRoom", "EntrepotId", "dbo.Entrepot");
            DropForeignKey("dbo.Raw_UserDetils", "RawId", "dbo.Z_Raw");
            DropForeignKey("dbo.Raw_UserDetils", "User_id", "dbo.UserDetails");
            DropForeignKey("dbo.Entrepot", "User_id", "dbo.UserDetails");
            DropIndex("dbo.RawRoom", new[] { "EntrepotId" });
            DropIndex("dbo.RawRoom", new[] { "User_id" });
            DropIndex("dbo.RawRoom", new[] { "RawId" });
            DropIndex("dbo.Raw_UserDetils", new[] { "User_id" });
            DropIndex("dbo.Raw_UserDetils", new[] { "RawId" });
            DropIndex("dbo.Entrepot", new[] { "User_id" });
            DropTable("dbo.RawRoom");
            DropTable("dbo.Raw_UserDetils");
            DropTable("dbo.Entrepot");
        }
    }
}
