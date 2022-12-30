using Hsl_Blog.Swagger.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hsl_Blog.HslBlogConsts;

namespace Hsl_Blog.Swagger
{
    public static class Hsl_BlogSwaggerExtensions
    {
        /// <summary>
        /// 当前API版本，从appsettings.json获取
        /// </summary>
        private static readonly string version = "v1.0.0";// $"v{AppSettings.ApiVersion}";

        /// <summary>
        /// Swagger描述信息
        /// </summary>
        private static readonly string description = @"<b>Blog</b>：<a target=""_blank"" href=""https://meowv.com"">https://meowv.com</a> <b>GitHub</b>：<a target=""_blank"" href=""https://github.com/Meowv/Blog"">https://github.com/Meowv/Blog</a> <b>Hangfire</b>：<a target=""_blank"" href=""/hangfire"">任务调度中心</a> <code>Powered by .NET Core 3.1 on Linux</code>";
        /// <summary>
        /// swaggerxml文件路径的指定
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options => {
                // 遍历并应用Swagger分组信息
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerDoc(x.UrlPrefix, x.OpenApiInfo);
                });
                /*
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title ="hsl的接口",
                    Description="接口描述"
                });*/
               // options.DocInclusionPredicate((docName, description) => true);
                //  swagger界面默认只显示 方法&字段 注释，不显示 控制器注释
              //  第二个参数为true， 则是controller的注释
                 //options.IncludeXmlComments(xmlPath);
                // options.IncludeXmlComments(xmlPath, true);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hsl_Blog.HttpApi.xml"),true);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hsl_Blog.Application.xml"),true);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hsl_Blog.Domain.xml"),true);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hsl_Blog.Application.Contracts.xml"),true);
                //防止同类名冲突
               // options.CustomSchemaIds(type => type.FullName);

                // 应用Controller的API文档描述信息
                options.DocumentFilter<SwaggerDocumentFilter>();

                #region 绿锁
                options.AddSecurityDefinition("JwtBearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Description = "这是方式一(直接在输入框中输入认证信息，不需要在开头添加Bearer)",
                    Name = "Authorization",//jwt默认的参数名称
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                /*options.AddSecurityDefinition("JwtBearer",new OpenApiSecurityScheme() {
                    Description = "这是方式一(直接在输入框中输入认证信息，不需要在开头添加Bearer)",
                    Name = "Authorization",//jwt默认的参数名称
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });*/
                //声明一个Scheme，注意下面的Id要和上面AddSecurityDefinition中的参数name一致
                var scheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "JwtBearer" }
                };
                //注册全局认证（所有的接口都可以使用认证）
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    [scheme] = new string[0]
                });
                //options.OperationFilter<AddResponseHeadersFilter>();
                //options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //options.OperationFilter<SecurityRequirementsOperationFilter>();
                #endregion

            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                // 遍历分组信息，生成Json
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerEndpoint($"/swagger/{x.UrlPrefix}/swagger.json", x.Name);
                });

                // 模型的默认扩展深度，设置为 -1 完全隐藏模型
                options.DefaultModelsExpandDepth(-1); //模型的默认扩展深度，设置为 - 1 完全隐藏模型。
                // API文档仅展开标记
                options.DocExpansion(DocExpansion.List);//代表API文档仅展开标记，不默然展开所有接口，需要我们手动去点击才展开，可以自行查看DocExpansion。
                // API前缀设置为空
               // options.RoutePrefix = string.Empty;//代表路由设置为空，直接打开页面就可以访问了。
                // API页面Title
                options.DocumentTitle = "😍接口文档 - 阿星Plus⭐⭐⭐";// 是设置文档页面的标题的。

                // options.SwaggerEndpoint($"/swagger/v1/swagger.json", "默认接口");
            });
        }

        internal class SwaggerApiInfo
        {
            /// <summary>
            /// URL前缀
            /// </summary>
            public string UrlPrefix { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// <see cref="Microsoft.OpenApi.Models.OpenApiInfo"/>
            /// </summary>
            public OpenApiInfo OpenApiInfo { get; set; }
        }
        /// <summary>
        /// Swagger分组信息，将进行遍历使用
        /// </summary>
        private static readonly List<SwaggerApiInfo> ApiInfos = new List<SwaggerApiInfo>()
        {
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v1,
                Name = "博客前台接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version ="v1.0.0", //version,
                    Title = "阿星Plus - 博客前台接口",
                    Description = "ceshi1"//description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v2,
                Name = "博客后台接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version ="v2.0.0",// version,
                    Title = "阿星Plus - 博客后台接口",
                    Description ="ceshi2"// description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v3,
                Name = "通用公共接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version ="v3.0.0",// version,
                    Title = "阿星Plus - 通用公共接口",
                    Description = "ceshi3"//description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v4,
                Name = "JWT授权接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version ="v4.0.0",// version,
                    Title = "阿星Plus - JWT授权接口",
                    Description = "ceshi4"//description
                }
            }
        };
    }
}
