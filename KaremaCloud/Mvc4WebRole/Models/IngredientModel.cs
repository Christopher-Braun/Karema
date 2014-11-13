using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Mvc4WebRole.Models
{
    [DebuggerDisplay("{Amount} {AmountType} {Name}")]
    public class IngredientModel
    {
        [Required]
        public Guid ID { get; set; }

        [ForeignKey("RecipeModel")]
        public Guid RecipeModelID { get; set; }

        public virtual RecipeModel RecipeModel { get; set; }

        public IngredientModel()
        {
            ID = Guid.NewGuid();
        }

        public IngredientModel(Guid id, Single amount, String amountType, String name)
        {
            this.ID = id;
            this.AmountType = amountType;
            this.Amount = amount;
            this.Name = name;
        }

        [Required(ErrorMessage = "Menge muss angegeben werden")]
        [Display(Name = "Menge")]
        [Range(0, 1000, ErrorMessage = "Wert muss zwischen 0 und 1000 sein")]
        public Single Amount
        {
            get;
            set;
        }

        [Display(Name = "Mengenart")]
        public String AmountType
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Name muss angegeben werden")]
        [Display(Name = "Zutat")]
        public String Name
        {
            get;
            set;
        }

        public Int32 Order
        {
            get;
            set;
        }
    }
}