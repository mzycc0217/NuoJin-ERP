namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Score", "AddbyId", c => c.Long(nullable: false));
            CreateIndex("dbo.Score", "AddbyId");
            AddForeignKey("dbo.Score", "AddbyId", "dbo.UserDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Score", "AddbyId", "dbo.UserDetails");
            DropIndex("dbo.Score", new[] { "AddbyId" });
            DropColumn("dbo.Score", "AddbyId");
        }
    }
}
