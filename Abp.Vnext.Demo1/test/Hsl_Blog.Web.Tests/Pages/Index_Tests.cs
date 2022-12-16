using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Hsl_Blog.Pages;

public class Index_Tests : Hsl_BlogWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
