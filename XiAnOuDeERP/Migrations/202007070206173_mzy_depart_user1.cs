namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_depart_user1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departent_User", "Departrement_ID", c => c.Long());
            AlterColumn("dbo.Departent_User", "U_ID", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departent_User", "U_ID", c => c.Long(nullable: false));
            AlterColumn("dbo.Departent_User", "Departrement_ID", c => c.Long(nullable: false));
        }
    }
}
