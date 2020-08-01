namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_AddCounma1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chemistry_UserDetils", "OutIutRoom", c => c.Double(nullable: false));
            AddColumn("dbo.Chemistry_UserDetils", "is_or", c => c.Int(nullable: false));
            AddColumn("dbo.Chemistry_UserDetils", "entrepotid", c => c.Long());
            AddColumn("dbo.ChemistryRoom", "del_or", c => c.Boolean(nullable: false));
            AddColumn("dbo.FnishedProduct_UserDetils", "is_or", c => c.Int(nullable: false));
            AddColumn("dbo.FnishedProduct_UserDetils", "entrepotid", c => c.Long());
            AddColumn("dbo.Office_UsrDetils", "is_or", c => c.Int(nullable: false));
            AddColumn("dbo.Office_UsrDetils", "GetRawTime", c => c.DateTime());
            AddColumn("dbo.Office_UsrDetils", "entrepotid", c => c.Long());
            CreateIndex("dbo.Chemistry_UserDetils", "entrepotid");
            CreateIndex("dbo.FnishedProduct_UserDetils", "entrepotid");
            CreateIndex("dbo.Office_UsrDetils", "entrepotid");
            AddForeignKey("dbo.Chemistry_UserDetils", "entrepotid", "dbo.Entrepot", "Id");
            AddForeignKey("dbo.FnishedProduct_UserDetils", "entrepotid", "dbo.Entrepot", "Id");
            AddForeignKey("dbo.Office_UsrDetils", "entrepotid", "dbo.Entrepot", "Id");
            DropColumn("dbo.Office_UsrDetils", "GetTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Office_UsrDetils", "GetTime", c => c.DateTime());
            DropForeignKey("dbo.Office_UsrDetils", "entrepotid", "dbo.Entrepot");
            DropForeignKey("dbo.FnishedProduct_UserDetils", "entrepotid", "dbo.Entrepot");
            DropForeignKey("dbo.Chemistry_UserDetils", "entrepotid", "dbo.Entrepot");
            DropIndex("dbo.Office_UsrDetils", new[] { "entrepotid" });
            DropIndex("dbo.FnishedProduct_UserDetils", new[] { "entrepotid" });
            DropIndex("dbo.Chemistry_UserDetils", new[] { "entrepotid" });
            DropColumn("dbo.Office_UsrDetils", "entrepotid");
            DropColumn("dbo.Office_UsrDetils", "GetRawTime");
            DropColumn("dbo.Office_UsrDetils", "is_or");
            DropColumn("dbo.FnishedProduct_UserDetils", "entrepotid");
            DropColumn("dbo.FnishedProduct_UserDetils", "is_or");
            DropColumn("dbo.ChemistryRoom", "del_or");
            DropColumn("dbo.Chemistry_UserDetils", "entrepotid");
            DropColumn("dbo.Chemistry_UserDetils", "is_or");
            DropColumn("dbo.Chemistry_UserDetils", "OutIutRoom");
        }
    }
}
