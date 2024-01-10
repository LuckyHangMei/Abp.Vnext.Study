using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace hslAbpDemo;

public interface ITodoAppService: IApplicationService
{
    Task<List<TodoItemDto>> GetListAsync();
    Task<TodoItemDto> CreateAsync(string text);
    Task DeleteAsync(Guid id);
}