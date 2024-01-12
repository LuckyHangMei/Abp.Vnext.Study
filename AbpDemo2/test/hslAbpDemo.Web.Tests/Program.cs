using Microsoft.AspNetCore.Builder;
using hslAbpDemo;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<hslAbpDemoWebTestModule>();

public partial class Program
{
}
