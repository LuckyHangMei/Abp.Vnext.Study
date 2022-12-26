using Hsl_Blog.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using static Hsl_Blog.HslBlogDbConsts;

namespace Hsl_Blog.EntityFrameworkCore
{
    public static class Hsl_BlogDbContextModelCreatingExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            //使用的是FliuntApi
            builder.Entity<Post>(p => {
                p.ToTable(HslBlogConsts.DbTablePrefix+HslBlogDbConsts.DbTableName.Posts);//设置表名
                p.HasKey(x => x.Id);
                p.Property(x=>x.Title).HasMaxLength(200).IsRequired();
                p.Property(x => x.Author).HasMaxLength(10);
                p.Property(x => x.Url).HasMaxLength(100).IsRequired();
                p.Property(x => x.Html).HasColumnType("longtext").IsRequired();
                p.Property(x => x.Markdown).HasColumnType("longtext").IsRequired();
                p.Property(x => x.CategoryId).HasColumnType("int");
                p.Property(x => x.CreationTime).HasColumnType("datetime");
            });
            builder.Entity<Category>(c => {
                c.ToTable(HslBlogConsts.DbTablePrefix + HslBlogDbConsts.DbTableName.Categories);
                c.HasKey(x => x.Id);
                c.Property(x => x.CategoryName).HasMaxLength(50).IsRequired();
                c.Property(x=>x.DisplayName).HasMaxLength(50).IsRequired();
            });
            builder.Entity<Tag>(t => {
                t.ToTable(HslBlogConsts.DbTablePrefix + DbTableName.Tags);
                t.HasKey(x => x.Id);
                t.Property(x=>x.TagName).HasMaxLength(50).IsRequired();
                t.Property(x=>x.DisplayName).HasMaxLength(50).IsRequired();
            });
            builder.Entity<PostTag>(b =>
            {
                b.ToTable(HslBlogConsts.DbTablePrefix + DbTableName.PostTags);
                b.HasKey(x => x.Id);
                b.Property(x => x.PostId).HasColumnType("int").IsRequired();
                b.Property(x => x.TagId).HasColumnType("int").IsRequired();
            });
            builder.Entity<FriendLink>(b =>
            {
                b.ToTable(HslBlogConsts.DbTablePrefix + DbTableName.Friendlinks);
                b.HasKey(x => x.Id);
                b.Property(x => x.Title).HasMaxLength(20).IsRequired();
                b.Property(x => x.LinkUrl).HasMaxLength(100).IsRequired();
            });
        }
    }
}
