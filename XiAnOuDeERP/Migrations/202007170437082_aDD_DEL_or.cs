namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aDD_DEL_or : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Z_Raw", "del_or", c => c.Int(nullable: false));
            AddColumn("dbo.Z_Chemistry", "del_or", c => c.Int(nullable: false));
            AddColumn("dbo.Z_FnishedProduct", "del_or", c => c.Int(nullable: false));
            AddColumn("dbo.Z_Office", "del_or", c => c.Int(nullable: false));
            AddColumn("dbo.Z_Supplies", "del_or", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Z_Supplies", "del_or");
            DropColumn("dbo.Z_Office", "del_or");
            DropColumn("dbo.Z_FnishedProduct", "del_or");
            DropColumn("dbo.Z_Chemistry", "del_or");
            DropColumn("dbo.Z_Raw", "del_or");
        }
    }
}
