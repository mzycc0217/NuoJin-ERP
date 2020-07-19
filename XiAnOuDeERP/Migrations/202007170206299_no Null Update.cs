namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noNullUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChemistryRoom", "RawNumber", c => c.Double());
            AlterColumn("dbo.ChemistryRoom", "RawOutNumber", c => c.Double());
            AlterColumn("dbo.ChemistryRoom", "Warning_RawNumber", c => c.Double());
            AlterColumn("dbo.FnishedProductRoom", "RawOutNumber", c => c.Double());
            AlterColumn("dbo.FnishedProductRoom", "Warning_RawNumber", c => c.Double());
            AlterColumn("dbo.OfficeRoom", "RawOutNumber", c => c.Double());
            AlterColumn("dbo.OfficeRoom", "Warning_RawNumber", c => c.Double());
            AlterColumn("dbo.RawRoom", "RawOutNumber", c => c.Double());
            AlterColumn("dbo.RawRoom", "Warning_RawNumber", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RawRoom", "Warning_RawNumber", c => c.Double(nullable: false));
            AlterColumn("dbo.RawRoom", "RawOutNumber", c => c.Double(nullable: false));
            AlterColumn("dbo.OfficeRoom", "Warning_RawNumber", c => c.Double(nullable: false));
            AlterColumn("dbo.OfficeRoom", "RawOutNumber", c => c.Double(nullable: false));
            AlterColumn("dbo.FnishedProductRoom", "Warning_RawNumber", c => c.Double(nullable: false));
            AlterColumn("dbo.FnishedProductRoom", "RawOutNumber", c => c.Double(nullable: false));
            AlterColumn("dbo.ChemistryRoom", "Warning_RawNumber", c => c.Double(nullable: false));
            AlterColumn("dbo.ChemistryRoom", "RawOutNumber", c => c.Double(nullable: false));
            AlterColumn("dbo.ChemistryRoom", "RawNumber", c => c.Double(nullable: false));
        }
    }
}
