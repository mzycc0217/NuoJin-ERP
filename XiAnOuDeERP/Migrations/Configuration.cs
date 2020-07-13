namespace XiAnOuDeERP.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<XiAnOuDeERP.Models.Db.XiAnOuDeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(XiAnOuDeERP.Models.Db.XiAnOuDeContext context)
        {

        }
    }
}
