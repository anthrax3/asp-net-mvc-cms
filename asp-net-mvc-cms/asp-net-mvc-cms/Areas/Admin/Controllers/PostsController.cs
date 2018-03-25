using asp_net_mvc_cms.Data;
using asp_net_mvc_cms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace asp_net_mvc_cms.Areas.Admin.Controllers
{
    // /admin/posts
    [RouteArea("admin")]
    [RoutePrefix("posts")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _repository;

        public PostsController() : this(new PostRepository()) { } // FOR: No parameterless constructor defined for this object.

        public PostsController(IPostRepository repository)
        {
            _repository = repository;
        }

        // GET: admin/posts
        [Route("")]
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
            return View(new Post());
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

            if (string.IsNullOrWhiteSpace(post.Id))
            {
                post.Id = post.Title;
            }

            post.Id = post.Id.MakeUrlFriendly();
            post.Tags = post.Tags.Select(tag => tag.MakeUrlFriendly()).ToList();
            post.Created = DateTime.Now;
            post.AuthorId = "5481f221-5fc1-4cc2-ab21-43ece16e9ed3";

            try
            {
                _repository.Create(post);

                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("key", e);
                return View(post);
            }
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
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            if (string.IsNullOrWhiteSpace(post.Id))
            {
                post.Id = post.Title;
            }

            post.Id = post.Id.MakeUrlFriendly();
            post.Tags = post.Tags.Select(tag => tag.MakeUrlFriendly()).ToList();

            try
            {
                _repository.Edit(postId, post);

                return RedirectToAction("index");
            }
            catch (KeyNotFoundException e)
            {
                return HttpNotFound();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View(post);
            }
        }

        // /admin/post/delete/post-to-edit
        [HttpGet]
        [Route("delete/{postId}")]
        public ActionResult Delete(string postId)
        {
            var post = _repository.Get(postId);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // /admin/post/delete/post-to-edit
        [HttpPost]
        [ActionName("Delete")]
        [Route("delete/{postId}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string postId)
        {
            try
            {
                _repository.Delete(postId);

                return RedirectToAction("index");
            }
            catch (KeyNotFoundException e)
            {
                return HttpNotFound();
            }
        }
    }
}