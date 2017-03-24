using System;

namespace Mvc4WebRole.ViewModels
{
    public class RecipeInfo
    {
        public RecipeInfo()
        {
            
        }

        public Guid Id { get; set; }
     

        public String Name { get; set; }

        public bool Star { get; set; }
    }
}