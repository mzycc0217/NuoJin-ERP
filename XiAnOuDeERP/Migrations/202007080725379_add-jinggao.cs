namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addjinggao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Z_Raw", "Density", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_Raw", "Statement", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_Raw", "Caution", c => c.String(maxLength: 100));
            DropColumn("dbo.Z_Raw", "ServiceLife");
            DropColumn("dbo.Z_Raw", "TechnicalDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Z_Raw", "TechnicalDescription", c => c.String(maxLength: 255));
            AddColumn("dbo.Z_Raw", "ServiceLife", c => c.Double());
            DropColumn("dbo.Z_Raw", "Caution");
            DropColumn("dbo.Z_Raw", "Statement");
            DropColumn("dbo.Z_Raw", "Density");
        }
    }
}
