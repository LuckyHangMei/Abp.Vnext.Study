using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Hsl_Blog.EntityFrameworkCore
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    [ConnectionStringName("MySql")]
    public class Hsl_BlogDbContext : AbpDbContext<Hsl_BlogDbContext>
    {
        public Hsl_BlogDbContext(DbContextOptions<Hsl_BlogDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// 定义EF Core 实体映射。先调用 base.OnModelCreating 让 abp 框架为我们实现基础映射，然后调用
        /// builder.Configure()扩展方法来配置应用程序的实体。当然也可以不用扩展，直接写在里面，这样一大坨显得不好看而已。
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configure();//扩展方法来配置应用程序的实体。当然也可以不用扩展，直接写在里面，这样一大坨显得不好看而已。
        }
    }
}
