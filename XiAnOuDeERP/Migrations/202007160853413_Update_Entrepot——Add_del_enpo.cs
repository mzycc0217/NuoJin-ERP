namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_EntrepotAdd_del_enpo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entrepot", "del_Enpto", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Entrepot", "del_Enpto");
        }
    }
}
