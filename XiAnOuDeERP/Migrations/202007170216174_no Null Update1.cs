namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noNullUpdate1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ChemistryRoom", new[] { "User_id" });
            DropIndex("dbo.ChemistryRoom", new[] { "EntrepotId" });
            DropIndex("dbo.FnishedProductRoom", new[] { "User_id" });
            DropIndex("dbo.FnishedProductRoom", new[] { "EntrepotId" });
            DropIndex("dbo.OfficeRoom", new[] { "User_id" });
            DropIndex("dbo.OfficeRoom", new[] { "EntrepotId" });
            DropIndex("dbo.RawRoom", new[] { "User_id" });
            DropIndex("dbo.RawRoom", new[] { "EntrepotId" });
            AlterColumn("dbo.ChemistryRoom", "User_id", c => c.Long());
            AlterColumn("dbo.ChemistryRoom", "EntrepotId", c => c.Long());
            AlterColumn("dbo.FnishedProductRoom", "User_id", c => c.Long());
            AlterColumn("dbo.FnishedProductRoom", "EntrepotId", c => c.Long());
            AlterColumn("dbo.OfficeRoom", "User_id", c => c.Long());
            AlterColumn("dbo.OfficeRoom", "EntrepotId", c => c.Long());
            AlterColumn("dbo.RawRoom", "User_id", c => c.Long());
            AlterColumn("dbo.RawRoom", "EntrepotId", c => c.Long());
            CreateIndex("dbo.ChemistryRoom", "User_id");
            CreateIndex("dbo.ChemistryRoom", "EntrepotId");
            CreateIndex("dbo.FnishedProductRoom", "User_id");
            CreateIndex("dbo.FnishedProductRoom", "EntrepotId");
            CreateIndex("dbo.OfficeRoom", "User_id");
            CreateIndex("dbo.OfficeRoom", "EntrepotId");
            CreateIndex("dbo.RawRoom", "User_id");
            CreateIndex("dbo.RawRoom", "EntrepotId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RawRoom", new[] { "EntrepotId" });
            DropIndex("dbo.RawRoom", new[] { "User_id" });
            DropIndex("dbo.OfficeRoom", new[] { "EntrepotId" });
            DropIndex("dbo.OfficeRoom", new[] { "User_id" });
            DropIndex("dbo.FnishedProductRoom", new[] { "EntrepotId" });
            DropIndex("dbo.FnishedProductRoom", new[] { "User_id" });
            DropIndex("dbo.ChemistryRoom", new[] { "EntrepotId" });
            DropIndex("dbo.ChemistryRoom", new[] { "User_id" });
            AlterColumn("dbo.RawRoom", "EntrepotId", c => c.Long(nullable: false));
            AlterColumn("dbo.RawRoom", "User_id", c => c.Long(nullable: false));
            AlterColumn("dbo.OfficeRoom", "EntrepotId", c => c.Long(nullable: false));
            AlterColumn("dbo.OfficeRoom", "User_id", c => c.Long(nullable: false));
            AlterColumn("dbo.FnishedProductRoom", "EntrepotId", c => c.Long(nullable: false));
            AlterColumn("dbo.FnishedProductRoom", "User_id", c => c.Long(nullable: false));
            AlterColumn("dbo.ChemistryRoom", "EntrepotId", c => c.Long(nullable: false));
            AlterColumn("dbo.ChemistryRoom", "User_id", c => c.Long(nullable: false));
            CreateIndex("dbo.RawRoom", "EntrepotId");
            CreateIndex("dbo.RawRoom", "User_id");
            CreateIndex("dbo.OfficeRoom", "EntrepotId");
            CreateIndex("dbo.OfficeRoom", "User_id");
            CreateIndex("dbo.FnishedProductRoom", "EntrepotId");
            CreateIndex("dbo.FnishedProductRoom", "User_id");
            CreateIndex("dbo.ChemistryRoom", "EntrepotId");
            CreateIndex("dbo.ChemistryRoom", "User_id");
        }
    }
}
