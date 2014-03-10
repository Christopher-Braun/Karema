using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Mvc4WebRole.Filters;
using Mvc4WebRole.Models;

namespace Mvc4WebRole.Controllers
{
    [EnhancedAuthorize(Roles = "Reader")]
    public class RecipeImageController : Controller
    {
        private readonly RecipeDomain repository;

        public RecipeImageController()
        {
            repository = new RecipeDomain();
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            RecipeModel recipemodel = this.repository.GetRecipe(id);
            if ( recipemodel == null )
            {
                return HttpNotFound();
            }

            return View(recipemodel);
        }

        [HttpGet]
        public ActionResult GetRecipeImage(String id)
        {
            var imageModel = repository.GetRecipe(Guid.Parse(id)).ImageModel;
            if ( imageModel == null )
            {
                return HttpNotFound();
            }

            return File(imageModel.Image, imageModel.MimeType);
        }


        [HttpPost]
        public ActionResult SetRecipeImage(HttpPostedFileBase file, Guid id)
        {
            if ( !repository.CanChange(id) )
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            if ( file == null || file.ContentLength == 0 )
            {
                return RedirectToAction("Details", new { id });
            }

            using (var memoryStream = new MemoryStream())
            {
                file.InputStream.CopyTo(memoryStream);
                this.repository.SetImage(id, memoryStream, file.ContentType);
            }

            return RedirectToAction("Details", new { id });
        }

        public ActionResult Delete(Guid id)
        {
            if ( !repository.CanChange(id) )
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var recipeModel = repository.GetRecipe(id);
            if ( recipeModel == null )
            {
                return HttpNotFound();
            }

            return View(recipeModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            repository.RemoveImage(id);
            return RedirectToAction("Details", new { id });
        }
    }
}