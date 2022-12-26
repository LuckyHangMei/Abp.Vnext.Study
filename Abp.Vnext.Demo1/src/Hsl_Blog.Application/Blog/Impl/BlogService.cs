using Hsl_Blog.Blog.Repositories;
using Hsl_Blog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsl_Blog.Blog.Impl
{
    public class BlogService : ServiceBase, IBlogService
    {
        private readonly IPostRepository _postRepository;
        public BlogService(IPostRepository postRepository) {
            _postRepository=postRepository;
        }
        public async Task<bool> DeletePostAsync(int id)
        {
            try
            {
                await _postRepository.DeleteAsync(id);
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<PostDto> GetPostAsync(int id)
        {
            var res= await _postRepository.FindAsync(id);
            return new PostDto {
                Title = res.Title,
                Author = res.Author,
                Url = res.Url,
                Html = res.Html,
                Markdown = res.Markdown,
                CategoryId = res.CategoryId,
                CreationTime = res.CreationTime
            };
        }

        public async Task<bool> InsertPostAsync(PostDto dto)
        {
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
            return post != null;
        }

        public Task<bool> UpdatePostAsync(int id, PostDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
