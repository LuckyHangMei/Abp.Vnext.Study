using Hsl_Blog.Blog.Repositories;
using Hsl_Blog.Entity;
using Hsl_Blog.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Hsl_Blog.Repositories.Blog
{
    public class TagRepository : EfCoreRepository<Hsl_BlogDbContext, Tag, int>, ITagRepository
    {
        public TagRepository(IDbContextProvider<Hsl_BlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task BulkInsertAsync(IEnumerable<Tag> tags)
        {
            var dbcontext = await GetDbContextAsync();
            await dbcontext.Set<Tag>().AddRangeAsync(tags);
            await dbcontext.SaveChangesAsync();
        }
    }
}
