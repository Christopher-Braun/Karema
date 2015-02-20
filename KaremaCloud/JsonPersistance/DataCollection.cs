using System.Collections.Generic;
using Mvc4WebRole.Models;

namespace KaReMa.Interfaces
{
    public class DataCollection
    {
        public DataCollection()
        {
            Recipes = new List<RecipeData>();
            Tags = new List<TagData>();
        }

        public List<RecipeData> Recipes
        {
            get;
            set;
        }

        public List<TagData> Tags
        {
            get;
            set;
        }
    }
}
