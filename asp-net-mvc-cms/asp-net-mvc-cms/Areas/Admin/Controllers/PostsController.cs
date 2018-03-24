using asp_net_mvc_cms.Models;
using System.Web.Mvc;

namespace asp_net_mvc_cms.Areas.Admin.Controllers
{
    // /admin/posts
    [RouteArea("admin")]
    [RoutePrefix("post")]
    public class PostsController : Controller
    {
        // GET: admin/posts
        public ActionResult Index()
        {
            return View();
        }

        // /admin/posts/create
        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            var post = new Post();

            return View(post);
        }

        // /admin/posts/create
        [HttpPost]
        [Route("create")]
        public ActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            return RedirectToAction("index");
        }

        // /admin/post/edit/post-to-edit
        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(string id)
        {
            var post = new Post();

            return View(post);
        }

        // /admin/post/edit/post-to-edit
        [HttpPost]
        [Route("edit/{id}")]
        public ActionResult Edit(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            return RedirectToAction("index");
        }
    }
}