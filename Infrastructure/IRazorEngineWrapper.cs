using System.Threading.Tasks;

namespace RazorLightConsole;

public interface IRazorEngineWrapper
{
    Task<string> GetHtmlFromFileAsync(string template, object model);

    Task<string> GetHtmlFromStringAsync(string template, object model);
}