using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Security;
using Mvc4WebRole.Models;
using Mvc4WebRole.ViewModels;
using WebMatrix.WebData;

namespace Mvc4WebRole
{
    public class RecipeDomain
    {
        private readonly RecipeDbContext recipeDb;

        public RecipeDomain()
        {
            this.recipeDb = new RecipeDbContext();
        }

        public IQueryable<RecipeInfo> RecipeInfos
        {
            get
            {
                return this.recipeDb.Recipes.Select(t => new RecipeInfo { Id = t.ID, Name = t.Name });
            }
        }

        public DbSet<TagModel> Tags
        {
            get { return this.recipeDb.Tags; }
        }

        public Boolean CanChange(Guid recipeId)
        {
            if ( Roles.IsUserInRole("Editor") )
            {
                return true;
            }

            RecipeModel recipemodel = this.GetRecipe(recipeId);
            return recipemodel.Author == WebSecurity.CurrentUserName;
        }

        public void SetImage(Guid id, MemoryStream memoryStream, String contentType)
        {
            var recipeModel = this.recipeDb.Recipes.Find(id);
            if ( recipeModel == null )
            {
                throw new InvalidOperationException();
            }

            if ( recipeModel.ImageModel == null )
            {
                recipeModel.ImageModel = new ImageModel();
            }
            recipeModel.ImageModel.Image = memoryStream.ToArray();
            recipeModel.ImageModel.MimeType = contentType;

            SaveChangedRecipe(recipeModel);
        }

        public void RemoveImage(Guid id)
        {
            RecipeModel recipeModel = this.recipeDb.Recipes.Find(id);

            if ( recipeModel == null )
            {
                throw new InvalidOperationException();
            }

            recipeModel.ImageModel = null;

            this.SaveChangedRecipe(recipeModel);
        }

        public void Create(RecipeModel recipemodel)
        {
            recipemodel.ID = Guid.NewGuid();
            recipemodel.Author = WebSecurity.CurrentUserName;
            OrderIngredients(recipemodel);
            this.recipeDb.Recipes.Add(recipemodel);
            this.recipeDb.SaveChanges();
        }

        public void EditRecipe(RecipeModel recipemodel)
        {
            recipemodel.UpdateLastTimeChanged();
            this.recipeDb.Ingredients.Where(t => t.RecipeModelID == recipemodel.ID).ForEach(t => this.recipeDb.Ingredients.Remove(t));
            recipemodel.Ingredients.ForEach(t => this.recipeDb.Ingredients.Add(t));
            SaveChangedRecipe(recipemodel);
        }

        public RecipeModel GetRecipe(Guid id)
        {
            //Eager loading instead of lazy            //  return Recipes.Find(id); 
            var recipe = this.recipeDb.Recipes.Include(x => x.Ingredients).Include(x => x.Tags).First(x => x.ID == id);
            recipe.Ingredients = recipe.Ingredients.OrderBy(i => i.Order).ToList();
            return recipe;
        }

        public void SaveChangedRecipe(RecipeModel recipemodel)
        {
            OrderIngredients(recipemodel);
            this.recipeDb.Entry(recipemodel).State = EntityState.Modified;
            this.recipeDb.SaveChanges();
        }

        private void OrderIngredients(RecipeModel model)
        {
            int i = 0;
            model.Ingredients.ForEach(x => x.Order = i++);
        }

        public void DeleteRecipe(Guid id)
        {
            var recipe = this.recipeDb.Recipes.Find(id);
            this.recipeDb.Entry(recipe).State = EntityState.Deleted;
            this.recipeDb.SaveChanges();
        }

        public void SetTags(Guid recipeId, IEnumerable<Guid> tagGuids)
        {
            RecipeModel recipeModel = this.GetRecipe(recipeId);
            
            recipeModel.Tags.RemoveAll(t => ! tagGuids.Contains(t.ID) );

            var assignedTagIds = recipeModel.Tags.Select(y => y.ID).ToList();
            var tagsToAdd = tagGuids.Where(t => !assignedTagIds.Contains(t)).Select(i => this.Tags.Find(i));
            recipeModel.Tags.AddRange(tagsToAdd);

            this.recipeDb.SaveChanges();
        }

        public void CreateTag(TagModel tagmodel)
        {
            tagmodel.ID = Guid.NewGuid();
            this.recipeDb.Tags.Add(tagmodel);
            this.recipeDb.SaveChanges();
        }

        public void EditTag(TagModel tagmodel)
        {
            this.recipeDb.Entry(tagmodel).State = EntityState.Modified;
            this.recipeDb.SaveChanges();
        }

        public void DeleteTag(Guid id)
        {
            TagModel tagmodel = this.recipeDb.Tags.Find(id);
            this.recipeDb.Tags.Remove(tagmodel);
            this.recipeDb.SaveChanges();
        }
    }
}