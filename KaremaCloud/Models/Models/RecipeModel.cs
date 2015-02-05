using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Mvc4WebRole.Models
{
    [DebuggerDisplay("{Name} {ID}")]
    public class RecipeModel
    {
        public const Int32 MaxPersonCount = 24;

        public RecipeModel()
        {
            this.ID = Guid.NewGuid();
            this.Ingredients = new List<IngredientModel>();
            this.Tags = new List<TagModel>();
        }

        public Guid ID
        {
            get;
            set;
        }

        [Display(Name = "Zutaten")]
        public virtual List<IngredientModel> Ingredients
        {
            get;
            set;
        }

        [Display(Name = "Tags")]
        public virtual List<TagModel> Tags
        {
            get;
            set;
        }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Name")]
        public String Name
        {
            get;
            set;
        }

        [Display(Name = "Beschreibung")]
        [DataType(DataType.MultilineText)]
        [Required]
        public String Description
        {
            get;
            set;
        }

        [Display(Name = "Anzahl / Personenzahl")]
        [Required]
        public Int32 DefaultPersonCount
        {
            get;
            set;
        }

        [Display(Name = "Zuletzt geändert")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd.MM.yy HH:mm}")]
        public DateTime LastTimeChanged
        {
            get;
            set;
        }

        [Display(Name = "Erstellt")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd.MM.yy HH:mm}")]
        public DateTime TimeCreated
        {
            get;
            set;
        }

        [Display(Name = "Autor")]
        public String Author
        {
            get;
            set;
        }

        [Display(Name = "Bemerkung")]
        public String Hint
        {
            get;
            set;
        }
    }
}
