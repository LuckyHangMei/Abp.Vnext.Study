using System;
using System.Threading.Tasks;
using hslAbpDemo.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace hslAbpDemo.Books;

/// <summary>
/// CrudAppService<...>.它实现了 ICrudAppService 定义的CRUD方法.
/// </summary>
public class BookAppService:CrudAppService<Book,BookDto,Guid,PagedAndSortedResultRequestDto,CreateUpdateBookDto>,IBookAppService
{
    public BookAppService(IRepository<Book, Guid> repository) : base(repository)
    {
        GetPolicyName = hslAbpDemoPermissions.Books.Default;
        GetListPolicyName = hslAbpDemoPermissions.Books.Default;
        CreatePolicyName = hslAbpDemoPermissions.Books.Create;
        UpdatePolicyName = hslAbpDemoPermissions.Books.Edit;
        DeletePolicyName = hslAbpDemoPermissions.Books.Delete;
    }
}