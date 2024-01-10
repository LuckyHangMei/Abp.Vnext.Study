using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace hslAbpDemo;

public class TodoAppService:ApplicationService,ITodoAppService
{
    private readonly IRepository<TodoItem,Guid>_todoItemRepository;
    public TodoAppService(IRepository<TodoItem,Guid>todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }
    public async Task<List<TodoItemDto>> GetListAsync()
    {
        var items = await _todoItemRepository.GetListAsync();
        return items.Select(item => new TodoItemDto
            {
                Id = item.Id,
                Text = item.Text
            }).ToList();
    }

    public async Task<TodoItemDto> CreateAsync(string text)
    {
        var todoItem = new TodoItem { Text = text };
        var entity=await _todoItemRepository.InsertAsync(todoItem);
        return new TodoItemDto()
        {
            Id = entity.Id,
            Text =  entity.Text
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        await _todoItemRepository.DeleteAsync(id);
    }
}