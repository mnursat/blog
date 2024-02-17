using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Blog.Web.Controllers;

public class AdminController : Controller
{
    private readonly IBlogPostRepository _blogPostRepository;

    public AdminController(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    [HttpGet]
    public IActionResult AddBlog()
    {
        return View("./Blogs/Add");
    }

    [HttpPost]
    public IActionResult AddBlog(AddBlogPost blog)
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

        _blogPostRepository.Add(blogPost);

        var notification = new Notification() { Message = "New blog created.", Type = Enums.NotificationType.Success };

        TempData["Notification"] = JsonSerializer.Serialize(notification);

        return RedirectToAction(nameof(ListBlog));
    }

    [HttpGet]
    public IActionResult ListBlog()
    {
        var blogPosts = _blogPostRepository.GetAll();
        var notificationJson = (string)TempData["Notification"];

        if (notificationJson is not null)
        {
            ViewBag.Notification = JsonSerializer.Deserialize<Notification>(notificationJson);
        }

        return View("./Blogs/List", blogPosts);
    }

    [ModelBinder(Name = "FeaturedImage")]
    public IFormFile FeaturedImage { get; set; }

    [HttpGet]
    public IActionResult EditBlog([FromRoute] Guid id)
    {
        var blogPost = _blogPostRepository.Get(id);


        return View("./Blogs/Edit", blogPost);
    }

    [HttpPost]
    public IActionResult EditBlog([FromForm] BlogPost blogPost)
    {
        try
        {
            _blogPostRepository.Update(blogPost);

            ViewBag.Notification = new Notification { Message = "Successfully edited.", Type = Enums.NotificationType.Success };
        }
        catch (Exception ex)
        {
            ViewBag.Notification = new Notification { Message = "Something went wrong.", Type = Enums.NotificationType.Error };
        }

        return View("./Blogs/Edit", blogPost);
    }

    [HttpPost]
    public IActionResult Delete([FromForm] BlogPost blogPost)
    {
        var deleted = _blogPostRepository.Delete(blogPost.Id);

        if (deleted)
        {
            var notification = new Notification { Message = "Post successfully delete", Type = Enums.NotificationType.Success };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToAction(nameof(ListBlog));
        }

        return View(blogPost);

    }
}
