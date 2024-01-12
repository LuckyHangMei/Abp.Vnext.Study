using System.Threading.Tasks;
using hslAbpDemo.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace hslAbpDemo.Web.Pages.Books;
/// <summary>
/// 该类派生于 hslAbpDemoPageModel 而非默认的 PageModel. hslAbpDemoPageModel
/// 间接继承了 PageModel 并且添加了一些可以被你的page model类使用的通用属性和方法.
/// </summary>
public class CreateModalModel  : hslAbpDemoPageModel
{
    /// <summary>
    /// Book 属性上的 [BindProperty] 特性将post请求提交上来的数据绑定到该属性上.
    /// </summary>
    [BindProperty]
    public CreateUpdateBookDto Book { get; set; }

    private readonly IBookAppService _bookAppService;

    public CreateModalModel(IBookAppService bookAppService)
    {
        _bookAppService = bookAppService;
    }

    public void OnGet()
    {
        Book = new CreateUpdateBookDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _bookAppService.CreateAsync(Book);
        return NoContent();
    }
}