﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsl_Blog.Swagger.Filters
{
    /// <summary>
    /// 对应Controller的API文档描述信息
    /// </summary>
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var tags = new List<OpenApiTag>
            {
                new OpenApiTag {
                    Name = "Blog",
                    Description = "个人博客相关接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "包含：文章/标签/分类/友链" }
                },
                new OpenApiTag {
                    Name = "HelloWorld",
                    Description = "通用公共接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "这里是一些通用的公共接口" }
                },
                new OpenApiTag {
                    Name = "Auth",
                    Description = "JWT模式认证授权",
                    ExternalDocs = new OpenApiExternalDocs { Description = "JSON Web Token" }
                }
            };
            #region 实现添加自定义描述时过滤不属于同一分组的api
            //当前分组名称
            var groupName = context.ApiDescriptions.FirstOrDefault()?.GroupName;
            //当前所有的api对象
            var apis = context.ApiDescriptions.GetType().GetField("_source", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(context.ApiDescriptions) as IEnumerable<ApiDescription>;
            // 不属于当前分组的所有Controller
            // 注意：配置的OpenApiTag，Name名称要与Controller的Name对应才会生效。
            var controllers = apis.Where(x=>x.GroupName!=groupName).Select(x=>((ControllerActionDescriptor)x.ActionDescriptor).ControllerName).Distinct();
            #endregion
            // 按照Name升序排序
            swaggerDoc.Tags = tags.Where(x=>!controllers.Contains(x.Name)).OrderBy(x => x.Name).ToList();
        }
    }
}
