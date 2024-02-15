using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Controllers;

public class BlogsController : Controller
{
    private readonly BlogDbContext _blogDbContext;

    public BlogsController(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(AddBlogPost blog)
    {
        var blogPost = new BlogPost()
        {
            Heading = blog.Heading,
            PageTitle = blog.PageTitle,
            Content = blog.Content,
            ShortDescription = blog.ShortDescription,
            FeaturedImageUrl = blog.FeaturedImageUrl,
            UrlHandle = blog.UrlHandle,
            PublishedDate = blog.PublishedDate,
            Author = blog.Author,
            Visible = blog.Visible,
        };

        _blogDbContext.BLogPosts.Add(blogPost);
        _blogDbContext.SaveChanges();

        TempData["SuccessCreated"] = "New blog post successfully create.";

        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult List()
    {
        var blogPosts = _blogDbContext.BLogPosts.ToList();
        var messageDescription = TempData["SuccessCreated"];

        if (messageDescription is not null)
        {
            ViewBag.MessageDescription = messageDescription;
        }

        return View(blogPosts);
    }

    [HttpGet]
    public IActionResult Edit([FromRoute] Guid id)
    {
        var blogPost = _blogDbContext.BLogPosts.Find(id);


        return View(blogPost);
    }

    [HttpPost]
    public IActionResult Edit([FromForm] BlogPost blogPost)
    {
        var existingBlogPost = _blogDbContext.BLogPosts.FirstOrDefault(p => p.Id == blogPost.Id);

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

            _blogDbContext.SaveChanges();

            ViewBag.Notification = new Notification { Message = "Successfully edited.", Type = Enums.NotificationType.Success };

            return View(existingBlogPost);
        }

        return View(blogPost);
    }

    [HttpPost]
    public IActionResult Delete([FromForm] BlogPost blogPost)
    {
        var blog = _blogDbContext.BLogPosts
            .AsNoTracking()
            .FirstOrDefault(b => b.Id == blogPost.Id);

        if (blog is null)
        {
            return View();
        }

        _blogDbContext.BLogPosts.Remove(blog);
        _blogDbContext.SaveChanges();
        return RedirectToAction(nameof(List));
    }
}
