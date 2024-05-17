using Microsoft.Extensions.Logging;
using Razor.Templating.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RazorEngineDockerApi.Infrastructure
{
    public class RazorTemplatingCoreEngineWrapper : IRazorEngineWrapper
    {
        private readonly ILogger<RazorTemplatingCoreEngineWrapper> _logger;

        public RazorTemplatingCoreEngineWrapper(ILogger<RazorTemplatingCoreEngineWrapper> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetHtmlFromFileAsync(string template, object model)
        {
            try
            {
                return await RazorTemplateEngine.RenderAsync(template, model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error compiling template: {Message}", ex.Message);
                throw;
            }
        }

        public Task<string> GetHtmlFromStringAsync(string template, object model)
        {
            throw new NotImplementedException();
        }
    }
}
