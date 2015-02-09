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
        private readonly TagDomain tagDomain = new TagDomain();
        private readonly RecipeDomain recipeDomain = new RecipeDomain();

        public ActionResult AssignTags(Guid recipeId)
        {
            if (!recipeDomain.CanChange(recipeId))
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var recipemodel = recipeDomain.GetRecipe(recipeId);

            if (recipemodel == null)
            {
                return HttpNotFound();
            }

            var rmTags = recipemodel.Tags.Select(t => t.ID);

            var tags = tagDomain.Tags.ToList().Select(t =>
            new TagInfo
            {
                Caption = t.Caption,
                ID = t.ID,
                IsChecked = rmTags.Contains(t.ID)
            }).ToList();

            var model = new RecipeTagMapModel { RecipeID = recipeId, TagInfos = tags, RecipeName = recipemodel.Name };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignTags(RecipeTagMapModel model)
        {
            if (ModelState.IsValid)
            {
                tagDomain.SetTags(model.RecipeID, model.TagInfos.Where(t => t.IsChecked).Select(t => t.ID));
                return RedirectToAction("Details", "Recipe", new { ID = model.RecipeID });
            }
            return View(model);
        }

        public ActionResult Index()
        {
            return View(tagDomain.Tags.ToList());
        }

        public ActionResult Overview()
        {
            return View(tagDomain.Tags.ToList());
        }

        public ActionResult Details(Guid id)
        {
            TagModel tagmodel = tagDomain.GetTag(id);
            if (tagmodel == null)
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
            if (ModelState.IsValid)
            {
                tagDomain.CreateTag(tagmodel);
                return RedirectToAction("Index");
            }

            return View(tagmodel);
        }

        [EnhancedAuthorize(Roles = "Editor")]
        public ActionResult Edit(Guid id)
        {
            TagModel tagmodel = tagDomain.GetTag(id);
            if (tagmodel == null)
            {
                return HttpNotFound();
            }
            return View(tagmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TagModel tagmodel)
        {
            if (ModelState.IsValid)
            {
                this.tagDomain.EditTag(tagmodel);
                return RedirectToAction("Index");
            }
            return View(tagmodel);
        }

        [EnhancedAuthorize(Roles = "Editor")]
        public ActionResult Delete(Guid id)
        {
            TagModel tagmodel = tagDomain.GetTag(id);
            if (tagmodel == null)
            {
                return HttpNotFound();
            }
            return View(tagmodel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            this.tagDomain.DeleteTag(id);
            return RedirectToAction("Index");
        }


    }
}