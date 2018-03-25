using asp_net_mvc_cms.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace asp_net_mvc_cms.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("tags")]
    public class TagsController : Controller
    {
        private readonly ITagRepository _repository;

        public TagsController() : this(new TagRepository()) { }

        public TagsController(ITagRepository repository)
        {
            _repository = repository;
        }

        // GET: Admin/Tags
        [Route("")]
        public ActionResult Index()
        {
            var tags = _repository.GetAll();

            return View(tags);
        }

        [HttpGet]
        [Route("edit/{tag}")]
        public ActionResult Edit(string tag)
        {
            try
            {
                var tag_check = _repository.Get(tag);

                return View(model: tag_check);
            }
            catch (KeyNotFoundException e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [Route("edit/{tag}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string tag, string newTag)
        {
            var tags = _repository.GetAll();

            if (!tags.Contains(tag))
            {
                return HttpNotFound();
            }

            if (tags.Contains(newTag))
            {
                return RedirectToAction("index");
            }

            if (string.IsNullOrWhiteSpace(newTag))
            {
                ModelState.AddModelError("key", "New tag value cannot be empty.");

                return View(model: tag);
            }

            _repository.Edit(tag, newTag);

            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("delete/{tag}")]
        public ActionResult Delete(string tag)
        {
            try
            {
                var tag_check = _repository.Get(tag);

                return View(model: tag_check);
            }
            catch (KeyNotFoundException e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [Route("delete/{tag}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string tag)
        {
            try
            {
                _repository.Delete(tag);

                return RedirectToAction("index");
            }
            catch (KeyNotFoundException e)
            {
                return HttpNotFound();
            }
        }
    }
}