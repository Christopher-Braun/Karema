using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage.Table;

namespace Mvc4WebRole.Models
{
    [DebuggerDisplay("{Name} {Id}")]
    [DataServiceEntity]
    public class RecipeEntity : TableEntity
    {
        public RecipeEntity()
        {
            this.ID = Guid.NewGuid();
            SetEntityValues();
        }

        public RecipeEntity(Guid id, String name, String description, Int32 defaultPersonCount, String ingredients)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.DefaultPersonCount = defaultPersonCount;
            this.Ingredients = ingredients;

            SetEntityValues();
        }

        private void SetEntityValues()
        {
            this.RowKey = ID.ToString();
            this.PartitionKey = "Recipe";
            this.ETag = "*";
        }

        public Guid ID
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

        public DateTime LastTimeChanged
        {
            get;
            set;
        }

        public DateTime TimeCreated
        {
            get;
            set;
        }

        public String Author
        {
            get;
            set;
        }

        public String Hint
        {
            get;
            set;
        }

        public String Ingredients
        {
            get;
            set;
        }

        //public virtual ICollection<IngredientEntity> Ingredients
        //{
        //    get;
        //    set;
        //}

        //public virtual List<Guid> Tags
        //{
        //    get;
        //    set;
        //}

    }
}

//[DebuggerDisplay("{Name} {Id}")]
//[DataServiceEntity]
//public class RecipeEntity : TableEntity
//{
//    public RecipeEntity()
//    {
//        this.Id = Guid.NewGuid();
//        SetEntityValues();
//    }

//    public RecipeEntity(Guid id, String jsonValue, String name)
//    {
//        this.Id = id;
//        this.JsonValue = jsonValue;
//        this.Name = name;

//        SetEntityValues();
//    }

//    private void SetEntityValues()
//    {
//        this.RowKey = Id.ToString();
//        this.PartitionKey = "Recipe";
//    }

//    public Guid Id
//    {
//        get;
//        set;
//    }

//    public String Name
//    {
//        get;
//        set;
//    }

//    public String JsonValue
//    {
//        get;
//        set;
//    }
//}
