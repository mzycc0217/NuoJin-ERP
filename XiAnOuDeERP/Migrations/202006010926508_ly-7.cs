namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RawMaterial", "EnglishName", c => c.String(maxLength: 255));
            AddColumn("dbo.RawMaterial", "BeCommonlyCalled1", c => c.String(maxLength: 255));
            AddColumn("dbo.RawMaterial", "BeCommonlyCalled2", c => c.String());
            DropColumn("dbo.RawMaterial", "BeCommonlyCalled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RawMaterial", "BeCommonlyCalled", c => c.String(maxLength: 255));
            DropColumn("dbo.RawMaterial", "BeCommonlyCalled2");
            DropColumn("dbo.RawMaterial", "BeCommonlyCalled1");
            DropColumn("dbo.RawMaterial", "EnglishName");
        }
    }
}
