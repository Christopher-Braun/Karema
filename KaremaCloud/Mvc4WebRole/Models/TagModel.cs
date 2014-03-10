using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc4WebRole.Models
{
    public class TagModel
    {
        public TagModel()
        {
            ID = Guid.NewGuid();
            Recipes = new List<RecipeModel>();
        }

        [Required]
        public Guid ID { get; set; }

        [Required]
        public virtual List<RecipeModel> Recipes { get; set; }

        [Required]
        [Display(Name = "Schlagwort")]
        public String Caption { get; set; }

    }
}