using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc4WebRole.ViewModels
{
    public class RecipeTagMapModel
    {
        [Display(Name = "Tags")]
        public List<TagInfo> TagInfos
        {
            get;
            set;
        }

        public Guid RecipeID { get; set; }

        [Display(Name = "Rezept")]
        public String RecipeName { get; set; }
    }
}