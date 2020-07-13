namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Number : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Z_Raw", "Number", c => c.Double());
            AddColumn("dbo.Z_Chemistry", "Number", c => c.Double());
            AddColumn("dbo.Z_FnishedProduct", "Number", c => c.Double());
            AddColumn("dbo.Z_MaterialCode", "Number", c => c.Double());
            AddColumn("dbo.Z_Office", "Number", c => c.Double());
            AddColumn("dbo.Z_Supplies", "Number", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Z_Supplies", "Number");
            DropColumn("dbo.Z_Office", "Number");
            DropColumn("dbo.Z_MaterialCode", "Number");
            DropColumn("dbo.Z_FnishedProduct", "Number");
            DropColumn("dbo.Z_Chemistry", "Number");
            DropColumn("dbo.Z_Raw", "Number");
        }
    }
}
