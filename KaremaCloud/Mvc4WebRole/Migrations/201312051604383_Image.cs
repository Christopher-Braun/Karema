namespace Mvc4WebRole.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Image : DbMigration
    {
        public override void Up()
        {
            CreateTable(
    "dbo.ImageModels",
    c => new
    {
        RecipeModelID = c.Guid(nullable: false),
        Image = c.Binary(),
    })
    .PrimaryKey(t => t.RecipeModelID)
    .ForeignKey("dbo.RecipeModels", t => t.RecipeModelID)
    .Index(t => t.RecipeModelID);
        }

        public override void Down()
        {
            DropIndex("dbo.ImageModels", new[] { "RecipeModelID" });
            DropForeignKey("dbo.ImageModels", "RecipeModelID", "dbo.RecipeModels");
            DropTable("dbo.ImageModels");
        }
    }
}
