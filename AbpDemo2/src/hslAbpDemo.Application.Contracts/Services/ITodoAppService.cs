using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace hslAbpDemo;

/// <summary>
/// 框架定义应用程序服务的接口不是必需的. 但是,它被建议作为最佳实践.
///ICrudAppService定义了常见的CRUD方法:GetAsync,GetListAsync,CreateAsync,UpdateAsync和DeleteAsync.
/// 从这个接口扩展不是必需的,你可以从空的IApplicationService接口继承并手动定义自己的方法(将在下一部分中完成).
///   ICrudAppService有一些变体, 你可以在每个方法中使用单独的DTO(例如使用不同的DTO进行创建和更新).
/// </summary>
public interface ITodoAppService: IApplicationService
{
    Task<List<TodoItemDto>> GetListAsync();
    Task<TodoItemDto> CreateAsync(string text);
    Task DeleteAsync(Guid id);
}