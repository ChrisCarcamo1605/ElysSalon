using System.Collections.ObjectModel;
using System.Globalization;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using System.Windows;
using ElysSalon2._0.Core.aplication.DTOs;
using ElysSalon2._0.Core.aplication.DTOs.DTOSales;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.domain.Services;

public class ReportsGeneratorUtil
{
    public static void generateAnualReport(DtoAnualData dto)
    {
        ExcelPackage.LicenseContext = LicenseContext.Commercial;
        ;

        // Crear el archivo Excel
        using (var package = new ExcelPackage())
        {
            // Crear una hoja de trabajo
            var worksheet = package.Workbook.Worksheets.Add("Datos");

            // Agregar encabezados
            worksheet.Cells[1, 1].Value = "Mes";
            worksheet.Cells[1, 2].Value = "Ventas";
            worksheet.Cells[1, 3].Value = "Gastos";

            // Datos de ejemplo
            var meses = new List<string>
            {
                "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre",
                "Noviembre", "Diciembre"
            };
            var ventas = new List<decimal>
            {
                dto.jenuaryTotal, dto.februaryTotal, dto.marchTotal, dto.aprilTotal, dto.mayTota, dto.juneTotal,
                dto.julyTotal, dto.augustTotal, dto.septemberTotal, dto.octuberTotal, dto.novemberTotal,
                dto.decemberTotal
            };
            var gastos = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            // Llenar datos
            for (int i = 0; i < meses.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = meses[i];
                worksheet.Cells[i + 2, 2].Value = ventas[i];
                worksheet.Cells[i + 2, 3].Value = gastos[i];
                MessageBox.Show($"Mes: {meses[i]}  valor: {ventas[i]}");
            }

            // Dar formato a los encabezados
            using (var range = worksheet.Cells[1, 1, 1, 3])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            // Dar formato a los números
            worksheet.Cells[2, 2, meses.Count + 1, 3].Style.Numberformat.Format = "#,##0.00";

            // Ajustar el ancho de las columnas
            worksheet.Columns[1].AutoFit();
            worksheet.Columns[2].AutoFit();
            worksheet.Columns[3].AutoFit();

            // Crear un gráfico de columnas
            var chart = worksheet.Drawings.AddChart("Gráfico1", eChartType.ColumnClustered);
            chart.Title.Text = $"Ventas y Gastos por el año {dto.year}";
            chart.SetPosition(1, 0, 5, 0);
            chart.SetSize(800, 400);

            // Configurar las series del gráfico
            var series1 =
                chart.Series.Add(worksheet.Cells[2, 2, meses.Count + 1, 2], worksheet.Cells[2, 1, meses.Count + 1, 1]);
            series1.Header = "Ventas";

            var series2 =
                chart.Series.Add(worksheet.Cells[2, 3, meses.Count + 1, 3], worksheet.Cells[2, 1, meses.Count + 1, 1]);
            series2.Header = "Gastos";

            // Crear un gráfico de líneas en una nueva hoja
            var worksheetGraph = package.Workbook.Worksheets.Add("Gráficos");
            var lineChart = worksheetGraph.Drawings.AddChart("Gráfico2", eChartType.Line);
            lineChart.Title.Text = "Tendencia de Ventas";
            lineChart.SetPosition(1, 0, 1, 0);
            lineChart.SetSize(800, 400);

            // Agregar serie al gráfico de líneas
            var lineSeries = lineChart.Series.Add(worksheet.Cells[2, 2, meses.Count + 1, 2],
                worksheet.Cells[2, 1, meses.Count + 1, 1]);
            lineSeries.Header = "Ventas";

            var date = DateTime.Today.ToString("ddMMyyyy", CultureInfo.InvariantCulture);

            // Guardar el archivo
            var fileInfo = new FileInfo($"C:\\Users\\Christian\\Desktop\\ReporteTest\\ReporteVentaAnual{date}.xlsx");
            package.SaveAs(fileInfo);

            Console.WriteLine("El archivo Excel ha sido creado exitosamente en: " + fileInfo.FullName);
        }
    }


    public static void GenerateMonthReport(DtoMonthFinancialData dto)
    {
        ExcelPackage.LicenseContext = LicenseContext.Commercial;
        ;

        // Crear el archivo Excel
        using (var package = new ExcelPackage())
        {
            // Crear una hoja de trabajo
            var worksheet = package.Workbook.Worksheets.Add("Datos");

            // Agregar encabezados
            worksheet.Cells[1, 1].Value = "Semana";
            worksheet.Cells[1, 2].Value = "Ventas";
            worksheet.Cells[1, 3].Value = "Gastos";

            // Datos de ejemplo
            var weeks = new List<string> { "Semana1", "Semana2", "Semana3", "Semana4" };
            var ventas = new List<decimal> { dto.week1Total, dto.week2Total, dto.week3Total, dto.week4Total };
            var gastos = new List<double> { 800, 850, 900, 950 };

            // Llenar datos
            for (int i = 0; i < weeks.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = weeks[i];
                worksheet.Cells[i + 2, 2].Value = ventas[i];
                worksheet.Cells[i + 2, 3].Value = gastos[i];
            }

            // Dar formato a los encabezados
            using (var range = worksheet.Cells[1, 1, 1, 3])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            // Dar formato a los números
            worksheet.Cells[2, 2, weeks.Count + 1, 3].Style.Numberformat.Format = "#,##0.00";

            // Ajustar el ancho de las columnas
            worksheet.Columns[1].AutoFit();
            worksheet.Columns[2].AutoFit();
            worksheet.Columns[3].AutoFit();

            // Crear un gráfico de columnas
            var chart = worksheet.Drawings.AddChart("Gráfico1", eChartType.ColumnClustered);
            chart.Title.Text = $"Ventas y Gastos por el Mes de {dto.month}";
            chart.SetPosition(1, 0, 5, 0);
            chart.SetSize(800, 400);

            // Configurar las series del gráfico
            var series1 =
                chart.Series.Add(worksheet.Cells[2, 2, weeks.Count + 1, 2], worksheet.Cells[2, 1, weeks.Count + 1, 1]);
            series1.Header = "Ventas";

            var series2 =
                chart.Series.Add(worksheet.Cells[2, 3, weeks.Count + 1, 3], worksheet.Cells[2, 1, weeks.Count + 1, 1]);
            series2.Header = "Gastos";

            // Crear un gráfico de líneas en una nueva hoja
            var worksheetGraph = package.Workbook.Worksheets.Add("Gráficos");
            var lineChart = worksheetGraph.Drawings.AddChart("Gráfico2", eChartType.Line);
            lineChart.Title.Text = "Tendencia de Ventas";
            lineChart.SetPosition(1, 0, 1, 0);
            lineChart.SetSize(800, 400);

            // Agregar serie al gráfico de líneas
            var lineSeries = lineChart.Series.Add(worksheet.Cells[2, 2, weeks.Count + 1, 2],
                worksheet.Cells[2, 1, weeks.Count + 1, 1]);
            lineSeries.Header = "Ventas";

            var date = DateTime.Today.ToString("ddMMyyyy", CultureInfo.InvariantCulture);
            // Guardar el archivo
            var fileInfo = new FileInfo($"C:\\Users\\Christian\\Desktop\\ReporteTest\\ReporteVenta{date}.xlsx");
            package.SaveAs(fileInfo);

            Console.WriteLine("El archivo Excel ha sido creado exitosamente en: " + fileInfo.FullName);
        }
    }

    public static void GenerateReport<T>(DateTime fromDate, DateTime untilDate, List<T> collection, Func<T,DateTime> dateSelector, Func<T,decimal> totalSelector) where T: class
    {
        ExcelPackage.LicenseContext = LicenseContext.Commercial;

        // Crear el archivo Excel
        using (var package = new ExcelPackage())
        {
            // Crear una hoja de trabajo
            var worksheet = package.Workbook.Worksheets.Add("Datos");

            // Agregar encabezados
            worksheet.Cells[1, 1].Value = "Fecha";
            worksheet.Cells[1, 2].Value = "Ventas";
            worksheet.Cells[1, 3].Value = "Gastos";

            // Optimización: calcular gastos una sola vez
            decimal gastoTotal = collection.Aggregate(0m, (acumulador, n) => acumulador + totalSelector(n)) / 4;

            // Preparar los datos
            var ventas = collection.Select(x => totalSelector(x)).ToList(); // Copia directa de la colección
            var gastos = new List<decimal>();

            // Calcular gastos para cada elemento
            foreach (var _ in collection)
            {
                gastos.Add(gastoTotal);
            }

            // Llenar datos
            for (int i = 0; i < ventas.Count; i++)
            {
                var dia = fromDate.AddDays(i);
                worksheet.Cells[i + 2, 1].Value = dateSelector(collection[i]).ToString("ddMMMM", new CultureInfo("es-ES"));
                worksheet.Cells[i + 2, 2].Value = ventas[i];
                worksheet.Cells[i + 2, 3].Value = gastos[i];

                // Aplicar formato numérico a las celdas
                worksheet.Cells[i + 2, 2].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[i + 2, 3].Style.Numberformat.Format = "#,##0.00";
            }

            // Dar formato a los encabezados
            using (var range = worksheet.Cells[1, 1, 1, 3])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            // Ajustar el ancho de las columnas
            worksheet.Columns[1].AutoFit();
            worksheet.Columns[2].AutoFit();
            worksheet.Columns[3].AutoFit();

            // Crear un gráfico de columnas
            var chart = worksheet.Drawings.AddChart("Gráfico1", eChartType.ColumnClustered);
            chart.Title.Text = $"Ventas y Gastos desde {fromDate} hasta {untilDate}";
            chart.SetPosition(1, 0, 5, 0);
            chart.SetSize(800, 400);

            // Configurar las series del gráfico correctamente
            var series1 = chart.Series.Add(worksheet.Cells[2, 2, ventas.Count + 1, 2],
                worksheet.Cells[2, 1, ventas.Count + 1, 1]);
            series1.Header = "Ventas";

            var series2 = chart.Series.Add(worksheet.Cells[2, 3, ventas.Count + 1, 3],
                worksheet.Cells[2, 1, ventas.Count + 1, 1]);
            series2.Header = "Gastos";

            // Crear un gráfico de líneas en una nueva hoja
            var worksheetGraph = package.Workbook.Worksheets.Add("Gráficos");
            var lineChart = worksheetGraph.Drawings.AddChart("Gráfico2", eChartType.Line);
            lineChart.Title.Text = "Tendencia de Ventas";
            lineChart.SetPosition(1, 0, 1, 0);
            lineChart.SetSize(800, 400);

            // Agregar serie al gráfico de líneas
            var lineSeries = lineChart.Series.Add(worksheet.Cells[2, 2, ventas.Count + 1, 2],
                worksheet.Cells[2, 1, ventas.Count + 1, 1]);
            lineSeries.Header = "Ventas";

            // Crear otro gráfico de líneas para gastos
            var lineChartGastos = worksheetGraph.Drawings.AddChart("Gráfico3", eChartType.Line);
            lineChartGastos.Title.Text = "Tendencia de Gastos";
            lineChartGastos.SetPosition(21, 0, 1, 0); // Posicionado debajo del primer gráfico
            lineChartGastos.SetSize(800, 400);

            var lineSeriesGastos = lineChartGastos.Series.Add(worksheet.Cells[2, 3, gastos.Count + 1, 3],
                worksheet.Cells[2, 1, gastos.Count + 1, 1]);
            lineSeriesGastos.Header = "Gastos";

            // Nombre de archivo con formato de fecha actual
            var date = DateTime.Today.ToString("ddMMyyyy", CultureInfo.InvariantCulture);
            var fileInfo =
                new FileInfo(
                    $"C:\\Users\\Christian\\Desktop\\ReporteTest\\Reporte_{fromDate.
                        ToString("ddMMMMyyyy", new CultureInfo("es-ES"))}_Hasta_{untilDate.
                        ToString("ddMMMMyyyy", new CultureInfo("es-ES"))}_{date}.xlsx");

            // Guardar el archivo
         
            package.SaveAs(fileInfo);
            Console.WriteLine("El archivo Excel ha sido creado exitosamente en: " + fileInfo.FullName);
        }
    }
}