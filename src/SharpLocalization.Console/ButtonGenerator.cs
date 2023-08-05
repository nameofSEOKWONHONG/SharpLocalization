using System.Text;
using ClosedXML.Excel;
using eXtensionSharp;
using SharpLocalization.Console.Abstract;

namespace SharpLocalization.Console;

public class ButtonGenerator : IGenerator
{
    public void Generate(string excelPath, string outputPath)
    {
        Korean(excelPath, outputPath);
        English(excelPath, outputPath);
    }
    
    private void Korean(string excelPath, string outputPath)
    {
        var workbook = new XLWorkbook(excelPath);
        var worksheet = workbook.Worksheet(3);
        var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header row

        var sb = new StringBuilder();
        sb.Append("{");
        foreach (var row in rows)
        {
            if (row.RowNumber() > 2)
            {
                var group = row.Cell(2).Value;
                var number = row.Cell(3).Value;
                var kor = row.Cell(4).Value;
                var groupName = $@"""{group}{number}""";
                var resultKor = $@"{groupName}:""{kor}"",";
                sb.Append(resultKor);
            }
        }

        var r = sb.ToString().Remove(sb.ToString().Length - 1, 1);
        r = r + "}";
        var resultPath = Path.Combine(outputPath, "btn.ko-KR.json");
        resultPath.xWriteFile(r);
    }

    private void English(string excelPath, string outputPath)
    {
        using (var workbook = new XLWorkbook(excelPath))
        {
            var worksheet = workbook.Worksheet(3);
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header row

            var sb = new StringBuilder();
            sb.Append("{");
            foreach (var row in rows)
            {
                if (row.RowNumber() > 2)
                {
                    var group = row.Cell(2).Value;
                    var number = row.Cell(3).Value;
                    var eng = row.Cell(5).Value;
                    var groupName = $@"""{group}{number}""";
                    var resultEng = $@"{groupName}:""{eng}"",";
                    sb.Append(resultEng);
                }
            }

            var r = sb.ToString().Remove(sb.ToString().Length - 1, 1);
            r = r + "}";
            var resultPath = Path.Combine(outputPath, "btn.en-US.json");
            resultPath.xWriteFile(r);
        }
    }
}