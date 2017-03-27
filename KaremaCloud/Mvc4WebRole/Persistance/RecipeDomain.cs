using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Mvc4WebRole.Models;
using Mvc4WebRole.ViewModels;
using WebMatrix.WebData;

namespace Mvc4WebRole
{
    public class RecipeDomain
    {
        private readonly RecipeDbContext recipeContext;

        public RecipeDomain()
        {
            this.recipeContext = new RecipeDbContext();
        }

        public IEnumerable<RecipeInfo> RecipeInfos
        {
            get
            {
                return this.recipeContext.Recipes.Select(t => new RecipeInfo {Id = t.ID, Name = t.Name, Star = t.Star});
            }
        }

        public Boolean CanChange(Guid recipeId)
        {
            if (Roles.IsUserInRole("Editor"))
            {
                return true;
            }

            var recipemodel = this.GetRecipe(recipeId);
            return String.Compare(recipemodel.Author, WebSecurity.CurrentUserName, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public void Create(RecipeModel recipemodel)
        {
            recipemodel.ID = Guid.NewGuid();
            recipemodel.Author = WebSecurity.CurrentUserName;
            recipemodel.TimeCreated = DateTime.UtcNow;
            recipemodel.LastTimeChanged = DateTime.UtcNow;

        //    OrderIngredients(recipemodel.Ingredients);
            this.recipeContext.Recipes.Add(recipemodel);
            this.recipeContext.SaveChanges();
        }

        public void EditRecipe(RecipeModel modifiedRecipe)
        {
            modifiedRecipe.LastTimeChanged = DateTime.UtcNow;

            var originalRecipe = recipeContext.Recipes
                .Where(p => p.ID == modifiedRecipe.ID)
                .Include(p => p.Ingredients)
                .SingleOrDefault();


            if (originalRecipe == null)
            {
                throw new InvalidOperationException("No recipe found for id " + modifiedRecipe.ID);
            }

            var originalEntry = recipeContext.Entry(originalRecipe);
            originalEntry.CurrentValues.SetValues(modifiedRecipe);


            foreach (var ingredient in modifiedRecipe.Ingredients)
            {
                var originalChildItem = originalRecipe.Ingredients.SingleOrDefault(i => i.ID == ingredient.ID && i.ID != Guid.Empty);
                // Is original child item with same ID in DB?
                if (originalChildItem != null)
                {
                    // Yes -> Update scalar properties of child item
                    var childEntry = recipeContext.Entry(originalChildItem);
                    childEntry.CurrentValues.SetValues(ingredient);
                }
                else
                {
                    // No -> It's a new child item -> Insert
                    originalRecipe.Ingredients.Add(ingredient);
                }
            }

            foreach (var originalChildItem in originalRecipe.Ingredients.Where(c => c.ID != Guid.Empty).ToList())
            {
                // Are there child items in the DB which are NOT in the
                // new child item collection anymore?
                if (modifiedRecipe.Ingredients.All(c => c.ID != originalChildItem.ID))
                    // Yes -> It's a deleted child item -> Delete
                    recipeContext.Ingredients.Remove(originalChildItem);
            }

            //Anderes Verfahren jetzt
         //   OrderIngredients(originalRecipe.Ingredients);
            this.recipeContext.SaveChanges();
        }

        //private void OrderIngredients(IEnumerable<IngredientModel> ingredients)
        //{
        //    int i = 0;
        //    ingredients.ForEach(x => x.Order = i++);
        //}

        public RecipeModel GetCompleteRecipe(Guid id)
        {
            //Eager loading instead of lazy            
            //  return Recipes.Find(id); 
            var recipe = this.recipeContext.Recipes.Include(x => x.Ingredients).Include(x => x.Tags).First(x => x.ID == id);
            recipe.Ingredients = recipe.Ingredients.OrderBy(i => i.Order).ToList();
            return recipe;
        }

        public RecipeModel GetRecipe(Guid id)
        {
            return this.recipeContext.Recipes.Find(id);
        }

        public void DeleteRecipe(Guid id)
        {
            var recipe = this.recipeContext.Recipes.Find(id);
            this.recipeContext.Entry(recipe).State = EntityState.Deleted;
            this.recipeContext.SaveChanges();
        }
    }
}