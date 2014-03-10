namespace Mvc4WebRole.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddImageFormatString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageModels", "MimeType", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.ImageModels", "MimeType");
        }
    }
}
