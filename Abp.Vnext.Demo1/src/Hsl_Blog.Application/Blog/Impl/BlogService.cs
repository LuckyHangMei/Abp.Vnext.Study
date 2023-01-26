using Hsl_Blog.Blog.Repositories;
using Hsl_Blog.Entity;
using Hsl_Blog.ToolKits.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hsl_Blog.HslBlogConsts;

namespace Hsl_Blog.Blog.Impl
{
    public class BlogService : ServiceBase, IBlogService
    {
        private readonly IPostRepository _postRepository;
        public BlogService(IPostRepository postRepository) {
            _postRepository=postRepository;
        }
        public async Task<ServiceResult> DeletePostAsync(int id)
        {
            var result = new ServiceResult();
            try
            {
                await _postRepository.DeleteAsync(id);
            }catch(Exception ex)
            {
                result.IsFailed(ex);
                return result;
            }
            result.IsSuccess("成功");
            return result;
        }
        public async Task<ServiceResult<PostDto>> GetPostAsync(int id)
        {
            var result = new ServiceResult<PostDto>();
            var post = await _postRepository.GetAsync(id);
            if (post == null)
            {
                result.IsFailed("文章不存在");
                return result;
            }
            //var res= await _postRepository.FindAsync(id);
            /* return new PostDto {
                 Title = res.Title,
                 Author = res.Author,
                 Url = res.Url,
                 Html = res.Html,
                 Markdown = res.Markdown,
                 CategoryId = res.CategoryId,
                 CreationTime = res.CreationTime
             };*/
            var dto = new PostDto
            {
                Title = post.Title,
                Author = post.Author,
                Url = post.Url,
                Html = post.Html,
                Markdown = post.Markdown,
                CategoryId = post.CategoryId,
                CreationTime = post.CreationTime
            };

            result.IsSuccess(dto);
            return result;
        }

        public async Task<ServiceResult<string>> InsertPostAsync(PostDto dto)
        {
            var result = new ServiceResult<string>();
            var entity = new Post
            {
                Title = dto.Title,
                Author = dto.Author,
                Url = dto.Url,
                Html = dto.Html,
                Markdown = dto.Markdown,
                CategoryId = dto.CategoryId,
                CreationTime = dto.CreationTime
            };

            var post = await _postRepository.InsertAsync(entity);
            if (post == null)
            {
                result.IsFailed("添加失败");
                return result;
            }

            result.IsSuccess("添加成功");
            return result;
        }
    

        public async Task<ServiceResult<string>> UpdatePostAsync(int id, PostDto dto)
        {
          var result = new ServiceResult<string>();

            var post = await _postRepository.GetAsync(id);
            if (post == null)
            {
                result.IsFailed("文章不存在");
                return result;
            }

            post.Title = dto.Title;
            post.Author = dto.Author;
            post.Url = dto.Url;
            post.Html = dto.Html;
            post.Markdown = dto.Markdown;
            post.CategoryId = dto.CategoryId;
            post.CreationTime = dto.CreationTime;

            await _postRepository.UpdateAsync(post);


            result.IsSuccess("更新成功");
            return result;
        }
    }
}
