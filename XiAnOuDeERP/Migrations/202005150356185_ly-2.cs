namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ly2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonnelRts", "Sex", c => c.String());
            DropColumn("dbo.PersonnelRts", "SexType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonnelRts", "SexType", c => c.Int(nullable: false));
            DropColumn("dbo.PersonnelRts", "Sex");
        }
    }
}
