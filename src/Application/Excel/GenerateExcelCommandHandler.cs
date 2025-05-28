using ClosedXML.Excel;
using ErrorOr;
using MediatR;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Excel
{
    internal sealed class GenerateExcelCommandHandler : IRequestHandler<GenerateExcelCommand, ErrorOr<byte[]>>
    {
        public async Task<ErrorOr<byte[]>> Handle(GenerateExcelCommand command, CancellationToken cancellationToken)
        {
            if (command.Data == null || command.Data.Count == 0)
            {
                return Error.Failure("NoData", "No hay datos para generar el archivo Excel.");
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Datos");

            // Agregar encabezados
            for (int i = 0; i < command.Columns.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = command.Columns[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.Green;
                worksheet.Cell(1, i + 1).Style.Font.FontColor = XLColor.White;
                worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            // Agregar datos
            for (int rowIndex = 0; rowIndex < command.Data.Count; rowIndex++)
            {
                var row = command.Data[rowIndex];
                for (int colIndex = 0; colIndex < command.Columns.Count; colIndex++)
                {
                    string columnName = command.Columns[colIndex];

                    // Verifica si la clave existe en el diccionario antes de acceder
                    string cellValue = row.TryGetValue(columnName, out var value) ? value?.ToString() ?? "" : "";

                    var cell = worksheet.Cell(rowIndex + 2, colIndex + 1);
                    cell.Value = cellValue;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }
            }

            // Aplicar estilos a la cabecera
            worksheet.Range(1, 1, 1, command.Columns.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Range(1, 1, 1, command.Columns.Count).Style.Font.Bold = true;
            worksheet.Range(1, 1, 1, command.Columns.Count).Style.Fill.BackgroundColor = XLColor.Green;
            worksheet.Range(1, 1, 1, command.Columns.Count).Style.Font.FontColor = XLColor.White;

            // Ajustar tamaño de columnas
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
