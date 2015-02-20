using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace KaReMa.Interfaces
{
    [DebuggerDisplay("{Amount} {AmountType} {Name}")]
    public class IngredientData
    {
        public IngredientData()
        {
            this.ID = Guid.NewGuid();
        }

        public IngredientData(Guid id, Single amount, String amountType, String name)
        {
            this.AmountType = amountType;
            this.Amount = amount;
            this.Name = name;
            this.ID = id;
        }

        public Guid ID
        {
            get;
            set;
        }

        public Single Amount
        {
            get;
            set;
        }

        public String AmountType
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public IngredientData Multiply(Double multiplier)
        {
            var newAmount = Convert.ToSingle(Math.Round(this.Amount * multiplier, 2));
            return new IngredientData(ID, newAmount, this.AmountType, this.Name);
        }
    }
}
