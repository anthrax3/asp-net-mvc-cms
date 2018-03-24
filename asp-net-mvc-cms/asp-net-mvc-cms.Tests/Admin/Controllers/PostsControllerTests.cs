using System;
using System.Web.Mvc;
using asp_net_mvc_cms.Areas.Admin.Controllers;
using asp_net_mvc_cms.Data;
using asp_net_mvc_cms.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace asp_net_mvc_cms.Tests.Admin.Controllers
{
    [TestClass]
    public class PostsControllerTests
    {
        [TestMethod]
        public void Edit_GetRequestSendsPostToView()
        {
            var id = "test-post";

            var repository = Mock.Create<IPostRepository>();
            var controller = new PostsController(repository);

            Mock.Arrange(() => repository.Get(id))
                .Returns(new Post { Id = id });

            var result = (ViewResult)controller.Edit(id);
            var model = (Post)result.Model;

            Assert.AreEqual(id, model.Id);
        }

        [TestMethod]
        public void Edit_GetRequestNotFoundResult()
        {
            var id = "test-post";

            var repository = Mock.Create<IPostRepository>();
            var controller = new PostsController(repository);

            Mock.Arrange(() => repository.Get(id))
                .Returns((Post)null);

            var result = controller.Edit(id);

            Assert.IsTrue(result is HttpNotFoundResult);
        }

        [TestMethod]
        public void Edit_PostRequestNotFoundResult()
        {
            var id = "test-post";

            var repository = Mock.Create<IPostRepository>();
            var controller = new PostsController(repository);

            Mock.Arrange(() => repository.Get(id))
                .Returns((Post)null);

            var result = controller.Edit(id, new Post());

            Assert.IsTrue(result is HttpNotFoundResult);
        }

        [TestMethod]
        public void Edit_PostRequestSendsPostToView()
        {
            var id = "test-post";

            var repository = Mock.Create<IPostRepository>();
            var controller = new PostsController(repository);

            Mock.Arrange(() => repository.Get(id))
                .Returns(new Post { Id = id });

            controller.ViewData.ModelState.AddModelError("key", "error message");

            var result = (ViewResult)controller.Edit(id, new Post() { Id = "test-post-2" });
            var model = (Post)result.Model;

            Assert.AreEqual("test-post-2", model.Id);
        }


        [TestMethod]
        public void Edit_PostRequestCallsEditAndRedirects()
        {
            var repository = Mock.Create<IPostRepository>();
            var controller = new PostsController(repository);

            Mock.Arrange(() => repository.Edit(Arg.IsAny<string>(), Arg.IsAny<Post>()))
                .MustBeCalled();

            var result = controller.Edit("foo", new Post() { Id = "test-post-2" });

            Assert.IsTrue(result is RedirectToRouteResult);
        }
    }
}
