using System.Collections.Generic;

namespace asp_net_mvc_cms.Data
{
    public interface ITagRepository
    {
        IEnumerable<string> GetAll();

        void Edit(string tag, string newTag);

        void Delete(string tag);

        string Get(string tag);
    }
}
