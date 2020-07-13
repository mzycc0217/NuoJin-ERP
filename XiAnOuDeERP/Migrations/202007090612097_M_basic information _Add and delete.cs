namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M_basicinformation_Addanddelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Z_Chemistry", "Density", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_Chemistry", "Statement", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_Chemistry", "Caution", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_FnishedProduct", "Density", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_FnishedProduct", "Statement", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_FnishedProduct", "Caution", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_MaterialCode", "Density", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_MaterialCode", "Statement", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_MaterialCode", "Caution", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_Office", "Density", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_Office", "Statement", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_Office", "Caution", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_Supplies", "Density", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_Supplies", "Statement", c => c.String(maxLength: 100));
            AddColumn("dbo.Z_Supplies", "Caution", c => c.String(maxLength: 100));
            DropColumn("dbo.Z_Chemistry", "ServiceLife");
            DropColumn("dbo.Z_Chemistry", "TechnicalDescription");
            DropColumn("dbo.Z_FnishedProduct", "ServiceLife");
            DropColumn("dbo.Z_FnishedProduct", "TechnicalDescription");
            DropColumn("dbo.Z_MaterialCode", "ServiceLife");
            DropColumn("dbo.Z_MaterialCode", "TechnicalDescription");
            DropColumn("dbo.Z_Office", "ServiceLife");
            DropColumn("dbo.Z_Office", "TechnicalDescription");
            DropColumn("dbo.Z_Supplies", "ServiceLife");
            DropColumn("dbo.Z_Supplies", "TechnicalDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Z_Supplies", "TechnicalDescription", c => c.String(maxLength: 255));
            AddColumn("dbo.Z_Supplies", "ServiceLife", c => c.Double());
            AddColumn("dbo.Z_Office", "TechnicalDescription", c => c.String(maxLength: 255));
            AddColumn("dbo.Z_Office", "ServiceLife", c => c.Double());
            AddColumn("dbo.Z_MaterialCode", "TechnicalDescription", c => c.String(maxLength: 255));
            AddColumn("dbo.Z_MaterialCode", "ServiceLife", c => c.Double());
            AddColumn("dbo.Z_FnishedProduct", "TechnicalDescription", c => c.String(maxLength: 255));
            AddColumn("dbo.Z_FnishedProduct", "ServiceLife", c => c.Double());
            AddColumn("dbo.Z_Chemistry", "TechnicalDescription", c => c.String(maxLength: 255));
            AddColumn("dbo.Z_Chemistry", "ServiceLife", c => c.Double());
            DropColumn("dbo.Z_Supplies", "Caution");
            DropColumn("dbo.Z_Supplies", "Statement");
            DropColumn("dbo.Z_Supplies", "Density");
            DropColumn("dbo.Z_Office", "Caution");
            DropColumn("dbo.Z_Office", "Statement");
            DropColumn("dbo.Z_Office", "Density");
            DropColumn("dbo.Z_MaterialCode", "Caution");
            DropColumn("dbo.Z_MaterialCode", "Statement");
            DropColumn("dbo.Z_MaterialCode", "Density");
            DropColumn("dbo.Z_FnishedProduct", "Caution");
            DropColumn("dbo.Z_FnishedProduct", "Statement");
            DropColumn("dbo.Z_FnishedProduct", "Density");
            DropColumn("dbo.Z_Chemistry", "Caution");
            DropColumn("dbo.Z_Chemistry", "Statement");
            DropColumn("dbo.Z_Chemistry", "Density");
        }
    }
}
