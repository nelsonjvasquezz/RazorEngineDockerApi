using Microsoft.AspNetCore.Mvc;
using RazorEngineDockerApi.Infrastructure;
using System;
using System.Data;
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
                var dataset = GetSampleDataSet();
                htmlResult = await _razorEngineWrapper.GetHtmlFromFileAsync("Template.cshtml", dataset);
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

        private static DataSet GetSampleDataSet()
        {
            var dataSet = new DataSet();
            var table = new DataTable("SampleTable");

            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(int));

            table.Rows.Add(1, "John Doe", 30);
            table.Rows.Add(2, "Jane Smith", 25);

            dataSet.Tables.Add(table);
            return dataSet;
        }
    }
}
