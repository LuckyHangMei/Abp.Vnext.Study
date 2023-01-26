using Hsl_Blog.Blog;
using Hsl_Blog.ToolKits.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using static Hsl_Blog.HslBlogConsts;

namespace Hsl_Blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v3)]
    public class BlogController: AbpControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        /// <summary>
        /// 新建博客
        /// </summary>
        /// <param name="dto">博客信息</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("post")]
        public async Task<ServiceResult<string>> InSertPost([FromBody] PostDto dto)
        {
            return await _blogService.InsertPostAsync(dto);
        }

        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("posts")]
        [Route("post/query")]
        public async Task<ServiceResult<PostDto>> QueryPostsAsync([Required] int id)
        {
            return await _blogService.GetPostAsync(id);
        }
        /// <summary>
        /// 删除指定博客
        /// </summary>
        /// <param name="id">博客唯一标识</param>
        /// <returns></returns>
  
        [HttpDelete]
        [Authorize]
        [Route("posts")]
        public async Task<ServiceResult> Delete([Required] int id)
        {
            return await _blogService.DeletePostAsync(id);
        }

    }
}
