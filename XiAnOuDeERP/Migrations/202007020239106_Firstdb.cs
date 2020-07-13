namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Firstdb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchase", "User_Id", c => c.Long());
            AddColumn("dbo.Purchase", "User_ID_Id", c => c.Long());
            CreateIndex("dbo.Purchase", "User_ID_Id");
            AddForeignKey("dbo.Purchase", "User_ID_Id", "dbo.UserDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchase", "User_ID_Id", "dbo.UserDetails");
            DropIndex("dbo.Purchase", new[] { "User_ID_Id" });
            DropColumn("dbo.Purchase", "User_ID_Id");
            DropColumn("dbo.Purchase", "User_Id");
        }
    }
}
