using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using Mvc4WebRole.Models;

namespace Mvc4WebRole
{
    public class RecipeRepository
    {
        public CloudTable RecipeTable
        {
            get { return RecipeTableServiceContext.Instance.RecipeTable; }
        }

        public TableServiceContext TableServiceContext
        {
            get { return RecipeTableServiceContext.Instance.TableServiceContext; }
        }


        public RecipeModel Find(Guid id)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<RecipeEntity>("Recipe", id.ToString());
            TableResult retrievedResult = RecipeTable.Execute(retrieveOperation);

            var recipeEntity = retrievedResult.Result as RecipeEntity;
            if ( recipeEntity == null )
            {
                return null;
            }

            return recipeEntity.ToModel();
        }

        public IQueryable<RecipeInfo> RecipeInfos
        {
            get
            {
                return RecipeQuery.Select(t => new RecipeInfo { Id = t.ID, Name = t.Name });
            }
        }


        private IQueryable<RecipeEntity> RecipeQuery
        {
            get
            {
                return TableServiceContext.CreateQuery<RecipeEntity>("Recipes").Where(t => t.PartitionKey == "Recipe");
            }
        }

        public void AddOrUpdate(RecipeModel recipe)
        {
            recipe.UpdateLastTimeChanged();
            TableOperation addOp = TableOperation.InsertOrReplace(recipe.ToEntity());
            RecipeTable.Execute(addOp);
        }

        public void Delete(Guid id)
        {
            TableOperation addOp = TableOperation.Delete(new DynamicTableEntity("Recipe", id.ToString()) { ETag = "*" });
            RecipeTable.Execute(addOp);
        }
    }
}