using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace hslAbpDemo.Data;

/* This is used if database provider does't define
 * IhslAbpDemoDbSchemaMigrator implementation.
 */
public class NullhslAbpDemoDbSchemaMigrator : IhslAbpDemoDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
