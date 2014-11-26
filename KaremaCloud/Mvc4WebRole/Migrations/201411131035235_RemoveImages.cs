namespace Mvc4WebRole.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveImages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageModels", "RecipeModelID", "dbo.RecipeModels");
            DropTable("dbo.ImageModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        RecipeModelID = c.Guid(nullable: false),
                        Image = c.Binary(),
                        MimeType = c.String(),
                    })
                .PrimaryKey(t => t.RecipeModelID);
            
            AddForeignKey("dbo.ImageModels", "RecipeModelID", "dbo.RecipeModels", "ID");
        }
    }
}
