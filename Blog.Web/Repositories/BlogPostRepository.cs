using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Blog.Web.Repositories;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly BlogDbContext _blogDbContext;

    public BlogPostRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }

    public BlogPost Add(BlogPost blogPost)
    {
        _blogDbContext.BLogPosts.Add(blogPost);
        _blogDbContext.SaveChanges();
        return blogPost;
    }

    public ICollection<BlogPost> GetAll()
    {
        return _blogDbContext.BLogPosts.ToList();
    }

    public BlogPost Get(Guid id)
    {
        return _blogDbContext.BLogPosts.Include(nameof(BlogPost.Tags)).FirstOrDefault(bp => bp.Id ==  id);
    }

    public BlogPost Get(string urlHandle)
    {
        return _blogDbContext.BLogPosts.FirstOrDefault(bp => bp.UrlHandle == urlHandle)!;
    }

    public BlogPost Update(BlogPost blogPost)
    {
        var existingBlogPost = _blogDbContext.BLogPosts.Find(blogPost.Id);

        if (existingBlogPost is not null)
        {
            existingBlogPost.Heading = blogPost.Heading;
            existingBlogPost.PageTitle = blogPost.PageTitle;
            existingBlogPost.Content = blogPost.Content;
            existingBlogPost.ShortDescription = blogPost.ShortDescription;
            existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
            existingBlogPost.UrlHandle = blogPost.UrlHandle;
            existingBlogPost.PublishedDate = blogPost.PublishedDate;
            existingBlogPost.Author = blogPost.Author;
            existingBlogPost.Visible = blogPost.Visible;
            


        }
        _blogDbContext.SaveChanges();
        return existingBlogPost;
    }

    public bool Delete(Guid id)
    {
        var existingBlog = _blogDbContext.BLogPosts.Find(id);

        if (existingBlog != null)
        {
            _blogDbContext.BLogPosts.Remove(existingBlog);
            _blogDbContext.SaveChanges();
            return true;
        }

        return false;
    }

}
