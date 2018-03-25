using asp_net_mvc_cms.Models;
using System.Collections.Generic;

namespace asp_net_mvc_cms.Data
{
    public interface IPostRepository
    {
        Post Get(string id);

        void Edit(string postId, Post post);

        void Create(Post post);

        void Delete(string id);

        IEnumerable<Post> GetAll();
    }
}
