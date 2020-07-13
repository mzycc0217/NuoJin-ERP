namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class My_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchase", "is_or", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchase", "is_or");
        }
    }
}
