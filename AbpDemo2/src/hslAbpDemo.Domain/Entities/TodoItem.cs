using System;
using Volo.Abp.Domain.Entities;

namespace hslAbpDemo;

/// <summary>
/// BasicAggregateRoot 是创建根实体的最简单的基础类. Guid 是这里实体的主键 (Id).
/// </summary>
public class TodoItem:BasicAggregateRoot<Guid>
{
    public string Text { get; set; }
}