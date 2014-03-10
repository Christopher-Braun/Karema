//using System;
//using Microsoft.WindowsAzure.Storage.Table;

//namespace Mvc4WebRole.Models
//{
//    public class IngredientEntity : TableEntity
//    {
//        public IngredientEntity()
//        {
//            this.ID = Guid.NewGuid();
//            SetEntityValues();
//        }

//        public IngredientEntity(Guid id, Single amount, String amountType, String name)
//        {
//            this.AmountType = amountType;
//            this.Amount = amount;
//            this.Name = name;
//            this.ID = id;

//            SetEntityValues();
//        }

//        private void SetEntityValues()
//        {
//            this.RowKey = ID.ToString();
//            this.PartitionKey = "Ingredient";
//            this.ETag = "*";
//        }

//        public Guid ID
//        {
//            get;
//            set;
//        }

//        public Single Amount
//        {
//            get;
//            set;
//        }

//        public String AmountType
//        {
//            get;
//            set;
//        }

//        public String Name
//        {
//            get;
//            set;
//        }

//    }
//}