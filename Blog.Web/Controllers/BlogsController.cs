using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

public class BlogsController : Controller
{
    private readonly IBlogPostRepository _blogPostRepository;

    public BlogsController(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    [HttpGet]

    public IActionResult Details(string urlHandle)
    {
        var blogPost = _blogPostRepository.Get(urlHandle);
        return View(blogPost);
    }
}
