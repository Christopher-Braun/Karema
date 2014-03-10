using System.Data.Entity;

namespace Mvc4WebRole
{


    //DropCreateDatabaseAlways
    public class DbInit : DropCreateDatabaseAlways<RecipeDbContext>
    {
        protected override void Seed(RecipeDbContext context)
        {
            context = new RecipeDbContext();

            //var recipe = new RecipeModel
            //{
            //    Author = "Hans",
            //    Hint = "Aus Buch",
            //    DefaultPersonCount = 5,
            //    Description = "Warm machen",
            //    Name = "Hans Wurst",
            //    Ingredients = new List<IngredientModel>
            //    {
            //        new IngredientModel{ Amount=2, AmountType="Zehen", Name="Knoblauch"},
            //        new IngredientModel{ Amount=5, AmountType="", Name="Äpfel"},
            //    }
            //};
            //var recipe2 = new RecipeModel
            //{
            //    Author = "Hans2",
            //    Hint = "Aus HasnBUch",
            //    DefaultPersonCount = 5,
            //    Description = "Warm machen",
            //    Name = "Peter Silie",
            //    Ingredients = new List<IngredientModel>
            //    {
            //        new IngredientModel{ Amount=9, AmountType="Zehen", Name="Knoblauch"},
            //        new IngredientModel{ Amount=3, AmountType="Esslöffel", Name="Butter"},
            //    }
            //};


            //var tag1 = new TagModel { Caption = "Schnell" };
            //var tag2 = new TagModel { Caption = "Leicht" };
            //var tag3 = new TagModel { Caption = "Gesund" };

            //recipe.Tags.Add(tag1);
            //recipe.Tags.Add(tag2);
            //recipe2.Tags.Add(tag1);

            //context.Tags.Add(tag1);
            //context.Tags.Add(tag2);
            //context.Tags.Add(tag3);

            //context.Recipes.Add(recipe);
            //context.Recipes.Add(recipe2);

            //context.SaveChanges();

            base.Seed(context);
        }
    }
}