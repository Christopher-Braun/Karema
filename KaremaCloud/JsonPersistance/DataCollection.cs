using System.Collections.Generic;
using Mvc4WebRole.Models;

namespace KaReMa.Interfaces
{
    public class DataCollection
    {
        public DataCollection()
        {
            Recipes = new List<RecipeModel>();
            Tags = new List<TagModel>();
        }

        public List<RecipeModel> Recipes
        {
            get;
            set;
        }

        public List<TagModel> Tags
        {
            get;
            set;
        }
    }
}
