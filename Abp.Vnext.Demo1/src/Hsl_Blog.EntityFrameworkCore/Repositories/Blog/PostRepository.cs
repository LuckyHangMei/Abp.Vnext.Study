using Hsl_Blog.Blog.Repositories;
using Hsl_Blog.Entity;
using Hsl_Blog.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Hsl_Blog.Repositories.Blog
{
    public class PostRepository : EfCoreRepository<Hsl_BlogDbContext, Post, int>, IPostRepository
    {
        public PostRepository(IDbContextProvider<Hsl_BlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
