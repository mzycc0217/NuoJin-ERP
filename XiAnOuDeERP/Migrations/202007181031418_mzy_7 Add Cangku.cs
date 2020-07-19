namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_7AddCangku : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Raw_UserDetils", "entrepotid", c => c.Long());
            CreateIndex("dbo.Raw_UserDetils", "entrepotid");
            AddForeignKey("dbo.Raw_UserDetils", "entrepotid", "dbo.Entrepot", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Raw_UserDetils", "entrepotid", "dbo.Entrepot");
            DropIndex("dbo.Raw_UserDetils", new[] { "entrepotid" });
            DropColumn("dbo.Raw_UserDetils", "entrepotid");
        }
    }
}
