using Hsl_Blog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Hsl_Blog.Blog.Repositories
{
    public interface IPostRepository: IRepository<Post, int>
    {
    }
}
