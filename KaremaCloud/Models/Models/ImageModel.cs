using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc4WebRole.Models
{
    public class ImageModel
    {
        [Key]
        [ForeignKey("RecipeModel")]
        public Guid RecipeModelID { get; set; }

        [Required]
        public virtual RecipeModel RecipeModel { get; set; }

        [Display(Name = "Bild")]
        public byte[] Image { get; set; }

        public String MimeType { get; set; }

        public bool HasImage
        {
            get
            {
                return Image.Length > 0;
            }
        }

    }
}