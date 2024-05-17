using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RazorLight;

namespace RazorLightConsole;

public class RazorLightEngineWrapper : IRazorEngineWrapper
{
    private readonly RazorLightEngine _engine;
    private readonly ILogger<RazorLightEngineWrapper> _logger;

    public RazorLightEngineWrapper(ILogger<RazorLightEngineWrapper> logger)
    {
        _logger = logger;
        _engine = GetConfiguredRazorEngine();
    }

    private RazorLightEngine GetConfiguredRazorEngine()
    {
        try
        {
            return new RazorLightEngineBuilder()
                        .UseFileSystemProject(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RazorTemplates"))
                        .UseMemoryCachingProvider()
                        .Build();
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError("Error initializing RazorLightEngine: {Message}", ex.Message);
            return null;
        }
    }

    public async Task<string> GetHtmlFromFileAsync(string template, object model)
    {
        try
        {
            return await _engine.CompileRenderAsync(template, model);
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError("Error compiling template: {Message}", ex.Message);
            throw;
        }
    }

    public async Task<string> GetHtmlFromStringAsync(string template, object model)
    {
        try
        {
            return await _engine.CompileRenderStringAsync("key", template, model);
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError("Error compiling template: {Message}", ex.Message);
            throw;
        }
    }
}