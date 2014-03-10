using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;


namespace Mvc4WebRole.Models
{
    public static class RecipeExtenions
    {
        public static RecipeModel ToModel(this RecipeEntity entity)
        {

            var ingredients = new JavaScriptSerializer().Deserialize(entity.Ingredients, typeof(IEnumerable<IngredientModel>)) as IEnumerable<IngredientModel>;

            var ingredientModels = ingredients.ToList();

            var model = new RecipeModel(entity.ID, entity.Name, entity.Description, entity.DefaultPersonCount, ingredientModels) { Author = entity.Author, Hint = entity.Hint, TimeCreated = entity.TimeCreated, LastTimeChanged = entity.LastTimeChanged };

            // var recipe = new JavaScriptSerializer().Deserialize(entity.JsonValue, typeof(RecipeModel)) as RecipeModel;
            //   recipe.PersonCount = recipe.DefaultPersonCount;
            return model;
        }

        //public static IngredientModel ToModel(this IngredientEntity entity)
        //{

        //    var model = new IngredientModel(entity.ID, entity.Amount, entity.AmountType, entity.Name);

        //    // var recipe = new JavaScriptSerializer().Deserialize(entity.JsonValue, typeof(RecipeModel)) as RecipeModel;
        //    //   recipe.PersonCount = recipe.DefaultPersonCount;
        //    return model;
        //}

        public static RecipeEntity ToEntity(this RecipeModel recipeData)
        {
            var ingredients = new JavaScriptSerializer().Serialize(recipeData.Ingredients);

            return new RecipeEntity(recipeData.ID, recipeData.Name, recipeData.Description, recipeData.DefaultPersonCount, ingredients) { Author = recipeData.Author, Hint = recipeData.Hint, LastTimeChanged = recipeData.LastTimeChanged, TimeCreated = recipeData.TimeCreated };
        }

        //public static IngredientEntity ToEntity(this IngredientModel ingredientData)
        //{
        //    return new IngredientEntity(ingredientData.ID, ingredientData.Amount, ingredientData.AmountType, ingredientData.Name);
        //}
    }
}