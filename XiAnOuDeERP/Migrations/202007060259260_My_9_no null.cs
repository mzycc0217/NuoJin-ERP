namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class My_9_nonull : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Content_User", "ContentID", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Content_User", "ContentID");
        }
    }
}
