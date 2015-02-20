using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace JsonPersistance
{
    using System.Drawing;
 using KaReMa.Interfaces;
    using Mvc4WebRole.Models;
    using System.Linq;

    public static class ModelExtensions
    {
        public static RecipeData ToData(this RecipeModel recipeModel)
        {
            var metaInfo = new MetaInfoDTO
            {
                Author = recipeModel.Author,
                Hint = recipeModel.Hint,
                LastTimeChanged = recipeModel.LastTimeChanged,
                TimeCreated = recipeModel.TimeCreated,
                Tags = recipeModel.Tags.Select(t => t.ID).ToList(),
            };

            var recipeData = new RecipeData
            {
                MetaInfo = metaInfo,
                DefaultPersonCount = recipeModel.DefaultPersonCount,
                Description = recipeModel.Description ?? " Fehlt",
                Id = recipeModel.ID,
                Name = recipeModel.Name,
            };

            foreach (var ingredientModel in recipeModel.Ingredients)
            {
                var ingredientData = new IngredientData(ingredientModel.ID, ingredientModel.Amount, ingredientModel.AmountType, ingredientModel.Name);

                recipeData.Ingredients.Add(ingredientData);
            }

            return recipeData;
        }


        public static TagData ToData(this TagModel model)
        {
            return new TagData { Caption = model.Caption, Id = model.ID };
        }


    }
}
