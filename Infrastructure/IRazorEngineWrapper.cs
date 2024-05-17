using System.Threading.Tasks;

namespace RazorEngineDockerApi.Infrastructure;

public interface IRazorEngineWrapper
{
    Task<string> GetHtmlFromFileAsync(string template, object model);

    Task<string> GetHtmlFromStringAsync(string template, object model);
}