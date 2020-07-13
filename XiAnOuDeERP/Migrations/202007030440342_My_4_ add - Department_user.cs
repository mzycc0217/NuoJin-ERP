namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class My_4_addDepartment_user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departent_User",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Departrement_ID = c.Long(nullable: false),
                        U_ID = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Departent_User");
        }
    }
}
