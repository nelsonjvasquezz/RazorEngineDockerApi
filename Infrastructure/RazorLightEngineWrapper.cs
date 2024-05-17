using System;
using System.IO;
using System.Threading.Tasks;
using RazorLight;

namespace RazorLightConsole;

public class RazorLightEngineWrapper : IRazorEngineWrapper
{
    private readonly RazorLightEngine _engine;

    public RazorLightEngineWrapper()
    {
        _engine = new RazorLightEngineBuilder()
                    .UseFileSystemProject(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RazorTemplates"))
                    .UseMemoryCachingProvider()
                    .Build();
    }

    public async Task<string> GetHtmlFromFileAsync(string template, object model)
    {
        return await _engine.CompileRenderAsync(template, model);
    }

    public async Task<string> GetHtmlFromStringAsync(string template, object model)
    {
        return await _engine.CompileRenderStringAsync("key", template, model);
    }
}