using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Mvc4WebRole.Models;

namespace Mvc4WebRole
{
    public class TagDomain
    {
        private readonly RecipeDbContext recipeContext;

        public TagDomain()
        {
            this.recipeContext = new RecipeDbContext();
        }

        public IEnumerable<TagModel> Tags
        {
            get { return recipeContext.Tags; }
        }

        public TagModel GetTag(Guid id)
        {
            //Eager loading instead of lazy            
            return recipeContext.Tags.Find(id);
        }

        public void SetTags(Guid recipeId, IEnumerable<Guid> tagIds)
        {
            var recipeModel = recipeContext.Recipes.Find(recipeId);

            recipeModel.Tags.RemoveAll(t => !tagIds.Contains(t.ID));

            var assignedTagIds = recipeModel.Tags.Select(y => y.ID).ToList();
            var tagsToAdd = tagIds.Where(t => !assignedTagIds.Contains(t)).Select(this.GetTag);
            recipeModel.Tags.AddRange(tagsToAdd);

            recipeContext.SaveChanges();
        }

        public void CreateTag(TagModel tagmodel)
        {
            tagmodel.ID = Guid.NewGuid();
            recipeContext.Tags.Add(tagmodel);
            recipeContext.SaveChanges();
        }

        public void EditTag(TagModel tagmodel)
        {
            recipeContext.Entry(tagmodel).State = EntityState.Modified;
            recipeContext.SaveChanges();
        }

        public void DeleteTag(Guid id)
        {
            var tagmodel = recipeContext.Tags.Find(id);
            recipeContext.Tags.Remove(tagmodel);
            recipeContext.SaveChanges();
        }
    }
}