using Application.Excel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/excel")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly ISender _mediator;

        public ExcelController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generate-excel")]
        public async Task<IActionResult> GenerateExcel([FromBody] GenerateExcelRequest request)
        {
            var command = new GenerateExcelCommand(request.Columns, request.Data);
            var result = await _mediator.Send(command);

            if (result.IsError)
            {
                return BadRequest(result.FirstError.Description);
            }

            return File(result.Value, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "simple_data.xlsx");
        }
    }

    public class GenerateExcelRequest
    {
        public List<string> Columns { get; set; } = new();
        public List<Dictionary<string, object>> Data { get; set; } = new();
    }
}
