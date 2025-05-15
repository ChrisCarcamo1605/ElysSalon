using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using ElysSalon2._0.aplication.DTOs.Request.Report;
using ElysSalon2._0.domain.Services;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;

namespace ElysSalon2._0.aplication.Utils;

public class ReportsGeneratorUtil
{
    public static void GenerateAnualReport(DTOAddAnualData dto)
    {
        ExcelPackage.LicenseContext = LicenseContext.Commercial;

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Data");

            worksheet.Cells[1, 1].Value = "Month";
            worksheet.Cells[1, 2].Value = "Sales";
            worksheet.Cells[1, 3].Value = "Expenses";

            var months = new List<string>
            {
                "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                "November", "December"
            };
            var sales = new List<decimal>
            {
                dto.jenuaryTotal, dto.februaryTotal, dto.marchTotal, dto.aprilTotal, dto.mayTota, dto.juneTotal,
                dto.julyTotal, dto.augustTotal, dto.septemberTotal, dto.octuberTotal, dto.novemberTotal,
                dto.decemberTotal
            };
            var expenses = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (var i = 0; i < months.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = months[i];
                worksheet.Cells[i + 2, 2].Value = sales[i];
                worksheet.Cells[i + 2, 3].Value = expenses[i];
            }

            using (var range = worksheet.Cells[1, 1, 1, 3])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            worksheet.Cells[2, 2, months.Count + 1, 3].Style.Numberformat.Format = "#,##0.00";

            worksheet.Columns[1].AutoFit();
            worksheet.Columns[2].AutoFit();
            worksheet.Columns[3].AutoFit();

            var chart = worksheet.Drawings.AddChart("Chart1", eChartType.ColumnClustered);
            chart.Title.Text = $"Sales and Expenses for the year {dto.year}";
            chart.SetPosition(1, 0, 5, 0);
            chart.SetSize(800, 400);

            var series1 = chart.Series.Add(worksheet.Cells[2, 2, months.Count + 1, 2],
                worksheet.Cells[2, 1, months.Count + 1, 1]);
            series1.Header = "Sales";

            var series2 = chart.Series.Add(worksheet.Cells[2, 3, months.Count + 1, 3],
                worksheet.Cells[2, 1, months.Count + 1, 1]);
            series2.Header = "Expenses";

            var worksheetGraph = package.Workbook.Worksheets.Add("Graphs");
            var lineChart = worksheetGraph.Drawings.AddChart("Chart2", eChartType.Line);
            lineChart.Title.Text = "Sales Trend";
            lineChart.SetPosition(1, 0, 1, 0);
            lineChart.SetSize(800, 400);

            var lineSeries = lineChart.Series.Add(worksheet.Cells[2, 2, months.Count + 1, 2],
                worksheet.Cells[2, 1, months.Count + 1, 1]);
            lineSeries.Header = "Sales";

            var date = DateTime.Today.ToString("ddMMyyyy", CultureInfo.InvariantCulture);

            var fileInfo = new FileInfo($"C:\\Users\\Christian\\Desktop\\ReporteTest\\AnualSalesReport{date}.xlsx");
            package.SaveAs(fileInfo);
        }
    }

    public static void GenerateMonthReport(DtoMonthFinancialData dto)
    {
        var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var folderName = "Mis Reportes\\Reportes Mensuales";
        var folderPath = Path.Combine(documentsDirectory, folderName);

        var baseName =
            $"Reporte_Mensual_{dto.month}.xlsx";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Console.WriteLine($"Carpeta creada en: {folderPath}");
        }

        ExcelPackage.LicenseContext = LicenseContext.Commercial;

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Data");

            worksheet.Cells[1, 1].Value = "Week";
            worksheet.Cells[1, 2].Value = "Sales";
            worksheet.Cells[1, 3].Value = "Expenses";

            var weeks = new List<string> { "Week1", "Week2", "Week3", "Week4" };
            var sales = new List<decimal> { dto.week1Total, dto.week2Total, dto.week3Total, dto.week4Total };
            var expenses = new List<double> { 800, 850, 900, 950 };

            for (var i = 0; i < weeks.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = weeks[i];
                worksheet.Cells[i + 2, 2].Value = sales[i];
                worksheet.Cells[i + 2, 3].Value = expenses[i];
            }

            using (var range = worksheet.Cells[1, 1, 1, 3])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            worksheet.Cells[2, 2, weeks.Count + 1, 3].Style.Numberformat.Format = "#,##0.00";

            worksheet.Columns[1].AutoFit();
            worksheet.Columns[2].AutoFit();
            worksheet.Columns[3].AutoFit();

            var chart = worksheet.Drawings.AddChart("Chart1", eChartType.ColumnClustered);
            chart.Title.Text = $"Sales and Expenses for the month of {dto.month}";
            chart.SetPosition(1, 0, 5, 0);
            chart.SetSize(800, 400);

            var series1 = chart.Series.Add(worksheet.Cells[2, 2, weeks.Count + 1, 2],
                worksheet.Cells[2, 1, weeks.Count + 1, 1]);
            series1.Header = "Sales";

            var series2 = chart.Series.Add(worksheet.Cells[2, 3, weeks.Count + 1, 3],
                worksheet.Cells[2, 1, weeks.Count + 1, 1]);
            series2.Header = "Expenses";

            var worksheetGraph = package.Workbook.Worksheets.Add("Graphs");
            var lineChart = worksheetGraph.Drawings.AddChart("Chart2", eChartType.Line);
            lineChart.Title.Text = "Sales Trend";
            lineChart.SetPosition(1, 0, 1, 0);
            lineChart.SetSize(800, 400);

            var lineSeries = lineChart.Series.Add(worksheet.Cells[2, 2, weeks.Count + 1, 2],
                worksheet.Cells[2, 1, weeks.Count + 1, 1]);
            lineSeries.Header = "Sales";

            var filepath = Path.Combine(folderPath, baseName);
            var fileInfo = new FileInfo(filepath);
            package.SaveAs(fileInfo);
        }
    }

    public static ResultFromService GenerateReport<T>(DateTime fromDate, DateTime untilDate, List<T> collection,
        Func<T, DateTime> dateSelector, Func<T, decimal> totalSelector) where T : class
    {
        var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var folderName = "Mis Reportes";
        var folderPath = Path.Combine(documentsDirectory, folderName);
        var baseName =
            $"Reporte_{fromDate.ToString("ddMMMMyyyy", new CultureInfo("es-ES"))}_Hasta_{untilDate.ToString("ddMMMMyyyy", new CultureInfo("es-ES"))}.xlsx";

        var uniqueName = GetUniqueFileNameInDirectory(folderPath, baseName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Console.WriteLine($"Carpeta creada en: {folderPath}");
        }

        var saveFileDialog = new SaveFileDialog
        {
            Filter = "Archivos Excel (*.xlsx)|*.xlsx",
            Title = "Guardar Reporte",
            DefaultExt = "xlsx",
            FileName = Path.GetFileName(uniqueName),
            InitialDirectory = folderPath,
            AddExtension = true
        };

        ExcelPackage.LicenseContext = LicenseContext.Commercial;

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Datos");

            worksheet.Cells[1, 1].Value = "Fecha";
            worksheet.Cells[1, 2].Value = "Ventas";
            worksheet.Cells[1, 3].Value = "Gastos";

            var totalExpenses =
                collection.Aggregate(0m, (accumulator, item) => accumulator + totalSelector(item)) / 4;

            var sales = collection.Select(x => totalSelector(x)).ToList();
            var expenses = new List<decimal>();

            foreach (var _ in collection) expenses.Add(totalExpenses);

            for (var i = 0; i < sales.Count; i++)
            {
                var day = fromDate.AddDays(i);
                worksheet.Cells[i + 2, 1].Value =
                    dateSelector(collection[i]).ToString("ddMMMM", new CultureInfo("es-ES"));
                worksheet.Cells[i + 2, 2].Value = sales[i];
                worksheet.Cells[i + 2, 3].Value = expenses[i];

                worksheet.Cells[i + 2, 2].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[i + 2, 3].Style.Numberformat.Format = "#,##0.00";
            }

            using (var range = worksheet.Cells[1, 1, 1, 3])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            worksheet.Columns[1].AutoFit();
            worksheet.Columns[2].AutoFit();
            worksheet.Columns[3].AutoFit();

            var chart = worksheet.Drawings.AddChart("Gráfico1", eChartType.ColumnClustered);
            chart.Title.Text = $"Ventas y Gastos desde {fromDate} hasta {untilDate}";
            chart.SetPosition(1, 0, 5, 0);
            chart.SetSize(800, 400);

            var series1 = chart.Series.Add(worksheet.Cells[2, 2, sales.Count + 1, 2],
                worksheet.Cells[2, 1, sales.Count + 1, 1]);
            series1.Header = "Ventas";

            var series2 = chart.Series.Add(worksheet.Cells[2, 3, sales.Count + 1, 3],
                worksheet.Cells[2, 1, sales.Count + 1, 1]);
            series2.Header = "Gastos";

            var worksheetGraph = package.Workbook.Worksheets.Add("Gráficos");
            var lineChart = worksheetGraph.Drawings.AddChart("Gráfico2", eChartType.Line);
            lineChart.Title.Text = "Tendencia de Ventas";
            lineChart.SetPosition(1, 0, 1, 0);
            lineChart.SetSize(800, 400);

            var lineSeries = lineChart.Series.Add(worksheet.Cells[2, 2, sales.Count + 1, 2],
                worksheet.Cells[2, 1, sales.Count + 1, 1]);
            lineSeries.Header = "Ventas";

            var lineChartExpenses = worksheetGraph.Drawings.AddChart("Gráfico3", eChartType.Line);
            lineChartExpenses.Title.Text = "Tendencia de Gastos";
            lineChartExpenses.SetPosition(21, 0, 1, 0);
            lineChartExpenses.SetSize(800, 400);

            var lineSeriesExpenses = lineChartExpenses.Series.Add(worksheet.Cells[2, 3, expenses.Count + 1, 3],
                worksheet.Cells[2, 1, expenses.Count + 1, 1]);
            lineSeriesExpenses.Header = "Gastos";

            var filePath =
                $"Reporte_{fromDate.ToString("ddMMMMyyyy", new CultureInfo("es-ES"))}_Hasta_{untilDate.ToString("ddMMMMyyyy", new CultureInfo("es-ES"))}.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                var selectedPath = saveFileDialog.FileName;
                var finalPath = GetUniqueFileName(selectedPath);
                package.SaveAs(new FileInfo(finalPath));
                saveFileDialog.FileName = "";

                return ResultFromService.SuccessResult("Archivo guardado correctamente");
            }

            return ResultFromService.Failed("Guardado cancelado");
        }
    }

    private static string GetUniqueFileNameInDirectory(string directory, string fileName)
    {
        var fullPath = Path.Combine(directory, fileName);

        if (!File.Exists(fullPath)) return fileName;

        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var extension = Path.GetExtension(fileName);

        var regex = new Regex(@"(.*)\((\d+)\)$");
        var match = regex.Match(fileNameWithoutExtension);

        string baseName;
        int counter;

        if (match.Success)
        {
            baseName = match.Groups[1].Value.TrimEnd();
            counter = int.Parse(match.Groups[2].Value) + 1;
        }
        else
        {
            baseName = fileNameWithoutExtension;
            counter = 1;
        }

        string newName;
        string newPath;

        do
        {
            newName = $"{baseName} ({counter}){extension}";
            newPath = Path.Combine(directory, newName);
            counter++;
        } while (File.Exists(newPath));

        return newName;
    }

    private static string GetUniqueFileName(string filePath)
    {
        if (!File.Exists(filePath)) return filePath;

        var directory = Path.GetDirectoryName(filePath);
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var extension = Path.GetExtension(filePath);

        var regex = new Regex(@"(.*)\((\d+)\)$");
        var match = regex.Match(fileName);

        string baseName;
        int counter;

        if (match.Success)
        {
            baseName = match.Groups[1].Value.TrimEnd();
            counter = int.Parse(match.Groups[2].Value) + 1;
        }
        else
        {
            baseName = fileName;
            counter = 1;
        }

        string newName;
        string newPath;

        do
        {
            newName = $"{baseName} ({counter})";
            newPath = Path.Combine(directory, newName + extension);
            counter++;
        } while (File.Exists(newPath));

        return newPath;
    }
}