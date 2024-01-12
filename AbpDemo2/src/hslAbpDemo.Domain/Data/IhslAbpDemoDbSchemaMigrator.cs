using System.Threading.Tasks;

namespace hslAbpDemo.Data;

public interface IhslAbpDemoDbSchemaMigrator
{
    Task MigrateAsync();
}
