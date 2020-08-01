namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Tbale_Sale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hostitry_Product_Price",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        FinshProductId = c.Long(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinshProductDes = c.String(),
                        User_Id = c.Long(nullable: false),
                        Price_Time = c.DateTime(),
                        del_Or = c.Double(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.User_Id)
                .ForeignKey("dbo.Z_FnishedProduct", t => t.FinshProductId)
                .Index(t => t.FinshProductId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.LeaderShip",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        FinshedProductId = c.Long(nullable: false),
                        User_DId = c.Long(nullable: false),
                        Des = c.String(),
                        Finsh_Start = c.Int(nullable: false),
                        Price_Time = c.DateTime(),
                        del_Or = c.Double(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.User_DId)
                .ForeignKey("dbo.Z_FnishedProduct", t => t.FinshedProductId)
                .Index(t => t.FinshedProductId)
                .Index(t => t.User_DId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeaderShip", "FinshedProductId", "dbo.Z_FnishedProduct");
            DropForeignKey("dbo.LeaderShip", "User_DId", "dbo.UserDetails");
            DropForeignKey("dbo.Hostitry_Product_Price", "FinshProductId", "dbo.Z_FnishedProduct");
            DropForeignKey("dbo.Hostitry_Product_Price", "User_Id", "dbo.UserDetails");
            DropIndex("dbo.LeaderShip", new[] { "User_DId" });
            DropIndex("dbo.LeaderShip", new[] { "FinshedProductId" });
            DropIndex("dbo.Hostitry_Product_Price", new[] { "User_Id" });
            DropIndex("dbo.Hostitry_Product_Price", new[] { "FinshProductId" });
            DropTable("dbo.LeaderShip");
            DropTable("dbo.Hostitry_Product_Price");
        }
    }
}
