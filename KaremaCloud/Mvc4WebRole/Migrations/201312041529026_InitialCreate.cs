namespace Mvc4WebRole.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecipeModels",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        DefaultPersonCount = c.Int(nullable: false),
                        LastTimeChanged = c.DateTime(nullable: false),
                        TimeCreated = c.DateTime(nullable: false),
                        Author = c.String(),
                        Hint = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IngredientModels",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RecipeModelID = c.Guid(nullable: false),
                        Amount = c.Single(nullable: false),
                        AmountType = c.String(),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RecipeModels", t => t.RecipeModelID, cascadeDelete: true)
                .Index(t => t.RecipeModelID);
            
            CreateTable(
                "dbo.TagModels",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Caption = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecipeTagMap",
                c => new
                    {
                        RecipeID = c.Guid(nullable: false),
                        TagID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecipeID, t.TagID })
                .ForeignKey("dbo.TagModels", t => t.RecipeID, cascadeDelete: true)
                .ForeignKey("dbo.RecipeModels", t => t.TagID, cascadeDelete: true)
                .Index(t => t.RecipeID)
                .Index(t => t.TagID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RecipeTagMap", new[] { "TagID" });
            DropIndex("dbo.RecipeTagMap", new[] { "RecipeID" });
            DropIndex("dbo.IngredientModels", new[] { "RecipeModelID" });
            DropForeignKey("dbo.RecipeTagMap", "TagID", "dbo.RecipeModels");
            DropForeignKey("dbo.RecipeTagMap", "RecipeID", "dbo.TagModels");
            DropForeignKey("dbo.IngredientModels", "RecipeModelID", "dbo.RecipeModels");
            DropTable("dbo.RecipeTagMap");
            DropTable("dbo.TagModels");
            DropTable("dbo.IngredientModels");
            DropTable("dbo.RecipeModels");
        }
    }
}
