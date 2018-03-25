using System;
using System.Collections.Generic;
using System.Linq;
using asp_net_mvc_cms.Models;

namespace asp_net_mvc_cms.Data
{
    public class PostRepository : IPostRepository
    {
        public Post Get(string id)
        {
            using (var db = new CmsContext())
            {
                return db.Posts.Include("Author")
                    .FirstOrDefault(p => p.Id == id);
            }
        }

        public IEnumerable<Post> GetAll()
        {
            using (var db = new CmsContext())
            {
                return db.Posts.Include("Author")
                    .OrderByDescending(p => p.Created).ToArray();
            }
        }

        public void Create(Post post)
        {
            using (var db = new CmsContext())
            {
                var post_check = db.Posts.FirstOrDefault(p => p.Id == post.Id);

                if (post != null)
                {
                    throw new ArgumentException("A post with the id of "
                        + post.Id + " already exists.");
                }

                db.Posts.Add(post);
                db.SaveChanges();
            }
        }

        public void Edit(string postId, Post post)
        {
            using (var db = new CmsContext())
            {
                var post_check = db.Posts.FirstOrDefault(p => p.Id == postId);

                if (post_check == null)
                {
                    throw new KeyNotFoundException("A post with the id of "
                        + postId + " does not exist in the data store.");
                }

                post_check.Id = post.Id;
                post_check.Title = post.Title;
                post_check.Content = post.Content;
                post.Published = post.Published;
                post.Tags = post.Tags;

                db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var db = new CmsContext())
            {
                var post = db.Posts.FirstOrDefault(p => p.Id == id);

                if (post == null)
                {
                    throw new KeyNotFoundException("The post with the id of " + id + " does not exist.");
                }

                db.Posts.Remove(post);
                db.SaveChanges();
            }
        }
    }
}