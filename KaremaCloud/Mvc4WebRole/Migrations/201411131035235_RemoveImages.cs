namespace Mvc4WebRole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveImages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageModels", "RecipeModelID", "dbo.RecipeModels");
            CreateIndex("dbo.IngredientModels", "RecipeModelID");
            CreateIndex("dbo.RecipeTagMap", "RecipeID");
            CreateIndex("dbo.RecipeTagMap", "TagID");
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
            
            DropIndex("dbo.RecipeTagMap", new[] { "TagID" });
            DropIndex("dbo.RecipeTagMap", new[] { "RecipeID" });
            DropIndex("dbo.IngredientModels", new[] { "RecipeModelID" });
            AddForeignKey("dbo.ImageModels", "RecipeModelID", "dbo.RecipeModels", "ID");
        }
    }
}
