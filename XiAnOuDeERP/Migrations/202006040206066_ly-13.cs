namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RawMaterial", "ServiceLife", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RawMaterial", "ServiceLife", c => c.Double(nullable: false));
        }
    }
}
