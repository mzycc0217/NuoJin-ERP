namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mzy_upadteAll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pursh_User",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        user_Id = c.Long(nullable: false),
                        Purchase_Id = c.Long(nullable: false),
                        ContentDes = c.String(maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        Purchase_Id1 = c.Long(),
                        UserDetails_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchase", t => t.Purchase_Id1)
                .ForeignKey("dbo.UserDetails", t => t.UserDetails_Id)
                .Index(t => t.Purchase_Id1)
                .Index(t => t.UserDetails_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pursh_User", "UserDetails_Id", "dbo.UserDetails");
            DropForeignKey("dbo.Pursh_User", "Purchase_Id1", "dbo.Purchase");
            DropIndex("dbo.Pursh_User", new[] { "UserDetails_Id" });
            DropIndex("dbo.Pursh_User", new[] { "Purchase_Id1" });
            DropTable("dbo.Pursh_User");
        }
    }
}
