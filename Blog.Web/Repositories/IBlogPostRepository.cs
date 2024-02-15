using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories;

public interface IBlogPostRepository
{
    ICollection<BlogPost> GetAll();
    BlogPost Get(Guid id);
    BlogPost Add(BlogPost blogPost);
    BlogPost Update(BlogPost blogPost);
    bool Delete(Guid id);
}
