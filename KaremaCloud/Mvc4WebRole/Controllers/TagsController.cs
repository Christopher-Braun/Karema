using System;
using System.Linq;
using System.Web.Mvc;
using Mvc4WebRole.Filters;
using Mvc4WebRole.Models;
using Mvc4WebRole.ViewModels;

namespace Mvc4WebRole.Controllers
{
    [EnhancedAuthorize(Roles = "Reader")]
    public class TagsController : Controller
    {
        private readonly RecipeDomain repository = new RecipeDomain();

        public ActionResult AssignTags(Guid id)
        {
            if ( !repository.CanChange(id) )
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            RecipeModel recipemodel = repository.GetRecipe(id);

            if ( recipemodel == null )
            {
                return HttpNotFound();
            }

            var tags = repository.Tags.ToList().Select(t =>
            new TagInfo
            {
                Caption = t.Caption,
                ID = t.ID,
                IsChecked = t.Recipes.Contains(recipemodel),
            }).ToList();

            var model = new RecipeTagMapModel { RecipeID = id, TagInfos = tags, RecipeName = recipemodel.Name };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignTags(RecipeTagMapModel model)
        {
            if ( ModelState.IsValid )
            {
                repository.SetTags(model.RecipeID, model.TagInfos.Where(t=> t.IsChecked).Select(t=>t.ID));
                return RedirectToAction("Details", "Recipe", new { ID = model.RecipeID });
            }
            return View(model);
        }

        public ActionResult Index()
        {
            return View(repository.Tags.ToList());
        }

        public ActionResult Overview()
        {
            return View(repository.Tags.ToList());
        }

        public ActionResult Details(Guid id)
        {
            TagModel tagmodel = repository.Tags.Find(id);
            if ( tagmodel == null )
            {
                return HttpNotFound();
            }
            return View(tagmodel);
        }

        [Authorize(Roles = "Author")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagModel tagmodel)
        {
            if ( ModelState.IsValid )
            {
                repository.CreateTag(tagmodel);
                return RedirectToAction("Index");
            }

            return View(tagmodel);
        }

        [EnhancedAuthorize(Roles = "Editor")]
        public ActionResult Edit(Guid id)
        {
            TagModel tagmodel = repository.Tags.Find(id);
            if ( tagmodel == null )
            {
                return HttpNotFound();
            }
            return View(tagmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TagModel tagmodel)
        {
            if ( ModelState.IsValid )
            {
                this.repository.EditTag(tagmodel);
                return RedirectToAction("Index");
            }
            return View(tagmodel);
        }

        [EnhancedAuthorize(Roles = "Editor")]
        public ActionResult Delete(Guid id)
        {
            TagModel tagmodel = repository.Tags.Find(id);
            if ( tagmodel == null )
            {
                return HttpNotFound();
            }
            return View(tagmodel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            this.repository.DeleteTag(id);
            return RedirectToAction("Index");
        }


    }
}