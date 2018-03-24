using asp_net_mvc_cms.Data;
using asp_net_mvc_cms.Models;
using System.Web.Mvc;

namespace asp_net_mvc_cms.Areas.Admin.Controllers
{
    // /admin/posts
    [RouteArea("admin")]
    [RoutePrefix("post")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _repository;

        public PostsController(IPostRepository repository)
        {
            _repository = repository;
        }

        // GET: admin/posts
        public ActionResult Index()
        {
            var posts = _repository.GetAll();

            return View(posts);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            _repository.Create(post);

            return RedirectToAction("index");
        }

        // /admin/post/edit/post-to-edit
        [HttpGet]
        [Route("edit/{postId}")]
        public ActionResult Edit(string postId)
        {
            var post = _repository.Get(postId);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // /admin/post/edit/post-to-edit
        [HttpPost]
        [Route("edit/{postId}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string postId, Post post)
        {
            var post_id = _repository.Get(postId);

            if (post_id == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(post);
            }

            _repository.Edit(postId, post);

            return RedirectToAction("index");
        }
    }
}