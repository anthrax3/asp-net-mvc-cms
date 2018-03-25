using System;
using System.Collections.Generic;
using System.Linq;

namespace asp_net_mvc_cms.Data
{
    public class TagRepository : ITagRepository
    {
        public void Delete(string tag)
        {
            using (var db = new CmsContext())
            {
                var posts = db.Posts.Where(p =>
                p.Tags.Contains(tag, StringComparer.CurrentCultureIgnoreCase))
                     .ToList();

                if (!posts.Any())
                {
                    throw new KeyNotFoundException("The tag " + tag + " does not exist.");
                }

                foreach (var post in posts)
                {
                    post.Tags.Remove(tag);
                }

                db.SaveChanges();
            }
        }

        public void Edit(string tag, string newTag)
        {
            using (var db = new CmsContext())
            {
                var posts = db.Posts.Where(p =>
                p.Tags.Contains(tag, StringComparer.CurrentCultureIgnoreCase))
                     .ToList();

                if (!posts.Any())
                {
                    throw new KeyNotFoundException("The tag " + tag + " does not exist.");
                }

                foreach (var post in posts)
                {
                    post.Tags.Remove(tag);
                    post.Tags.Add(newTag);
                }

                db.SaveChanges();
            }
        }

        public IEnumerable<string> GetAll()
        {
            using (var db = new CmsContext())
            {
                return db.Posts.SelectMany(p => p.Tags).Distinct();
            }
        }

        public string Get(string tag)
        {
            using (var db = new CmsContext())
            {
                var posts = db.Posts.Where(p =>
                p.Tags.Contains(tag, StringComparer.CurrentCultureIgnoreCase))
                     .ToList();

                if (!posts.Any())
                {
                    throw new KeyNotFoundException("The tag " + tag + " does not exist.");
                }

                return tag.ToLower();
            }
        }
    }
}