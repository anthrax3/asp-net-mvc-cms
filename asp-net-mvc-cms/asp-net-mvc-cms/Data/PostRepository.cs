using System.Collections.Generic;
using asp_net_mvc_cms.Models;

namespace asp_net_mvc_cms.Data
{
    public class PostRepository : IPostRepository
    {
        public void Create(Post post)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(string postId, Post post)
        {
            throw new System.NotImplementedException();
        }

        public Post Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}