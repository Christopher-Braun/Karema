using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace KaReMa.Interfaces
{
    [DebuggerDisplay("{Name} {Id}")]
    public class RecipeData
    {
        public RecipeData()
        {
            this.Id = Guid.NewGuid();
            this.Ingredients = new List<IngredientData>();
            this.MetaInfo = new MetaInfoDTO();
            this.DefaultPersonCount = 1;
        }

        public RecipeData(Guid id, String name, String description, Int32 defaultPersonCount, List<IngredientData> ingredients, MetaInfoDTO metaInfo)
        {
            this.Id = id;
            this.Description = description;
            this.Name = name;
            this.DefaultPersonCount = defaultPersonCount;
            this.Ingredients = ingredients;
            this.MetaInfo = metaInfo;
        }

        public Guid Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public String Description
        {
            get;
            set;
        }

        public Int32 DefaultPersonCount
        {
            get;
            set;
        }

        public List<IngredientData> Ingredients
        {
            get;
            set;
        }

        public IEnumerable<IngredientData> GetIngredients(Int32 personCount)
        {
            if ( this.Ingredients == null )
            {
                return Enumerable.Empty<IngredientData>();
            }

            Single multiplier = (Single)personCount / this.DefaultPersonCount;
            var multipliedIngredients = this.Ingredients.Select(t => t.Multiply(multiplier));
            return multipliedIngredients.Where(t => (!string.IsNullOrEmpty(t.Name) && t.Amount > 0));
        }

        public MetaInfoDTO MetaInfo
        {
            get;
            set;
        }

        public Bitmap Image
        {
            get
            {
                return MetaInfo.ImageData.Image;
            }
        }

        public override string ToString()
        {
            return this.Name + this.Id;
        }
    }
}
