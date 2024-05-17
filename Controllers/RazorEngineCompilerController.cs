using Microsoft.AspNetCore.Mvc;
using RazorEngineDockerApi.Infrastructure;
using System;
using System.Threading.Tasks;

namespace RazorEngineDockerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RazorEngineCompilerController : ControllerBase
    {
        private readonly IRazorEngineWrapper _razorEngineWrapper;

        public RazorEngineCompilerController(IRazorEngineWrapper razorEngineWrapper)
        {
            _razorEngineWrapper = razorEngineWrapper;
        }

        [HttpGet("string")]
        public async Task<ActionResult<string>> GetHtmlFromStringRazorTemplateAsync()
        {
            string htmlResult;
            try
            {
                htmlResult = await _razorEngineWrapper.GetHtmlFromStringAsync("<h1>Hello, @Model.Name</h1>", new { Name = "Razor Engine" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", GetInnerExceptions(ex));
                return BadRequest(ModelState);
            }

            return htmlResult;
        }

        [HttpGet("file")]
        public async Task<ActionResult<string>> GetHtmlFromFileRazorTemplateAsync()
        {
            string htmlResult;
            try
            {
                htmlResult = await _razorEngineWrapper.GetHtmlFromFileAsync("Template.cshtml", new { Name = "Razor Engine" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", GetInnerExceptions(ex));
                return BadRequest(ModelState);
            }

            return htmlResult;
        }

        private static string GetInnerExceptions(Exception ex)
        {
            string message = ex.Message;
            if (ex.InnerException != null)
            {
                message += " | " + GetInnerExceptions(ex.InnerException);
            }
            return message;
        }
    }
}
