using ErrorOr;
using MediatR;
using System.Collections.Generic;

namespace Application.Excel
{
   
    public record GenerateExcelCommand(
        List<string> Columns,
        List<Dictionary<string, object>> Data
    ) : IRequest<ErrorOr<byte[]>>;

    public record ExcelResponse(
        byte[] FileContent,
        string FileName
    );
}
