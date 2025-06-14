using System.Drawing;
using System.Globalization;
using Application.DTOs.Request.Reports;
using Application.DTOs.Request.TicketsDetails;
using Application.DTOs.Response.Expenses;
using Core.Common;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;

namespace Application.Utils;

public class ReportsGeneratorUtil
{
    public static ResultFromService GenerateAnualReport(DTOAddAnualData salesCollection,
        DTOAddAnualData expensesCollection)
    {
        ExcelPackage.LicenseContext =
            LicenseContext.NonCommercial;
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Data");

            worksheet.Cells[1, 1].Value = "Month";
            worksheet.Cells[1, 2].Value = "Sales";
            worksheet.Cells[1, 3].Value = "Expenses";
            worksheet.Cells[1, 4].Value = "Profit";

            var months = new List<string>
            {
                "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                "November", "December"
            };

            var sales = new List<decimal>
            {
                salesCollection.jenuaryTotal, salesCollection.februaryTotal, salesCollection.marchTotal,
                salesCollection.aprilTotal, salesCollection.mayTota, salesCollection.juneTotal,
                salesCollection.julyTotal, salesCollection.augustTotal, salesCollection.septemberTotal,
                salesCollection.octuberTotal, salesCollection.novemberTotal,
                salesCollection.decemberTotal
            };

            var expenses = new List<decimal>
            {
                expensesCollection.jenuaryTotal, expensesCollection.februaryTotal, expensesCollection.marchTotal,
                expensesCollection.aprilTotal, expensesCollection.mayTota, expensesCollection.juneTotal,
                expensesCollection.julyTotal, expensesCollection.augustTotal, expensesCollection.septemberTotal,
                expensesCollection.octuberTotal, expensesCollection.novemberTotal,
                expensesCollection.decemberTotal
            };

            var profits = new List<decimal>();
            decimal totalProfit = 0;

            for (var i = 0; i < months.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = months[i];
                worksheet.Cells[i + 2, 2].Value = sales[i];
                worksheet.Cells[i + 2, 3].Value = expenses[i];
                var monthlyProfit = sales[i] - expenses[i];
                worksheet.Cells[i + 2, 4].Value = monthlyProfit;
                profits.Add(monthlyProfit);
                totalProfit += monthlyProfit;
            }

            using (var range = worksheet.Cells[1, 1, 1, 4])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            worksheet.Cells[2, 2, months.Count + 1, 4].Style.Numberformat.Format = "#,##0.00";


            var totalRow = months.Count + 3; // Place it a couple of rows below the data
            worksheet.Cells[totalRow, 3].Value = "Total Profit:";
            worksheet.Cells[totalRow, 3].Style.Font.Bold = true;
            worksheet.Cells[totalRow, 4].Value = totalProfit;
            worksheet.Cells[totalRow, 4].Style.Numberformat.Format = "#,##0.00";
            worksheet.Cells[totalRow, 4].Style.Font.Bold = true;


            worksheet.Columns[1].AutoFit();
            worksheet.Columns[2].AutoFit();
            worksheet.Columns[3].AutoFit();
            worksheet.Columns[4].AutoFit();

            var dataSheetName = worksheet.Name;
            var chart = worksheet.Drawings.AddChart("AnnualSalesExpensesChart", eChartType.ColumnClustered);
            chart.Title.Text = $"Sales and Expenses for {salesCollection.year}";
            chart.SetPosition(1, 0, 5, 0); // Row, RowOffsetPixels, Col, ColOffsetPixels
            chart.SetSize(800, 400);


            var salesSeries = chart.Series.Add(worksheet.Cells[2, 2, months.Count + 1, 2],
                worksheet.Cells[2, 1, months.Count + 1, 1]);
            salesSeries.Header = "Sales";

            var expensesSeries = chart.Series.Add(worksheet.Cells[2, 3, months.Count + 1, 3],
                worksheet.Cells[2, 1, months.Count + 1, 1]);
            expensesSeries.Header = "Expenses";

            chart.Legend.Position = eLegendPosition.Bottom;


            var worksheetGraph = package.Workbook.Worksheets.Add("Annual Graphs");
            var lineChart = worksheetGraph.Drawings.AddChart("AnnualSalesTrendChart", eChartType.Line);


            lineChart.Title.Text = $"Sales Trend for {salesCollection.year}";

            lineChart.SetPosition(0, 0, 0, 0); // Top-left of the "Annual Graphs" sheet

            lineChart.SetSize(800, 400);

            var lineSeriesSales =
                lineChart.Series.Add(package.Workbook.Worksheets[dataSheetName].Cells[2, 2, months.Count + 1, 2],
                    package.Workbook.Worksheets[dataSheetName].Cells[2, 1, months.Count + 1, 1]);
            lineSeriesSales.Header = "Sales";
            lineChart.Legend.Position = eLegendPosition.Bottom;


            // File Saving

            try
            {
                var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var folderName = "Mis Reportes\\Reportes Anuales";
                var folderPath = Path.Combine(documentsDirectory, folderName);

                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                var date = DateTime.Today.ToString("yyyyMMdd",
                    CultureInfo.InvariantCulture);
                var fileName = $"AnualSalesReport_{salesCollection.year}_{date}.xlsx";
                var fileInfo = new FileInfo(Path.Combine(folderPath, fileName));
                package.SaveAs(fileInfo);

                return ResultFromService.SuccessResult(fileInfo.FullName, "Reporte anual generado correctamente");
            }
            catch (Exception e)
            {
                return ResultFromService.Failed("Hubo un error al generar el reporte: " + e.Message);
            }
        }
    }

    public static ResultFromService GenerateMonthReport(DtoMonthFinancialData salesCollection,
        DtoMonthFinancialData expensesCollection)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var folderName =
            "Mis Reportes\\Reportes Mensuales\\";
        var folderPath = Path.Combine(documentsDirectory, folderName);

        var baseName = $"Reporte_Mensual_{salesCollection.month}_{DateTime.Now.Date.ToString("yyyy")}.xlsx";

        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Data");

            worksheet.Cells[1, 1].Value = "Semanas";
            worksheet.Cells[1, 2].Value = "Ventas";
            worksheet.Cells[1, 3].Value = "Gastos";
            worksheet.Cells[1, 4].Value = "Ganancia";

            var weeks = new List<string> { "Semana1", "Semana2", "Semana3", "Semana4" };

            var sales = new List<decimal>
            {
                salesCollection.week1Total, salesCollection.week2Total, salesCollection.week3Total,
                salesCollection.week4Total
            };
            var expenses = new List<decimal> // Assuming expenses are decimal from the DTO
            {
                expensesCollection.week1Total, expensesCollection.week2Total, expensesCollection.week3Total,
                expensesCollection.week4Total
            };
            var profits = new List<decimal>();
            decimal totalProfit = 0;

            for (var i = 0; i < weeks.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = weeks[i];
                worksheet.Cells[i + 2, 2].Value = sales[i];
                worksheet.Cells[i + 2, 3].Value = expenses[i];
                var weeklyProfit = sales[i] - expenses[i];
                worksheet.Cells[i + 2, 4].Value = weeklyProfit;
                profits.Add(weeklyProfit);
                totalProfit += weeklyProfit;
            }

            // Style Headers
            using (var range = worksheet.Cells[1, 1, 1, 4])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // Format numbers
            worksheet.Cells[2, 2, weeks.Count + 1, 4].Style.Numberformat.Format = "#,##0.00";

            // Add Total Profit
            var totalRow = weeks.Count + 3;
            worksheet.Cells[totalRow, 3].Value = "Total Profit:";
            worksheet.Cells[totalRow, 3].Style.Font.Bold = true;
            worksheet.Cells[totalRow, 4].Value = totalProfit;
            worksheet.Cells[totalRow, 4].Style.Numberformat.Format = "#,##0.00";
            worksheet.Cells[totalRow, 4].Style.Font.Bold = true;

            worksheet.Columns[1].AutoFit();
            worksheet.Columns[2].AutoFit();
            worksheet.Columns[3].AutoFit();
            worksheet.Columns[4].AutoFit();

            // --- Clustered Column Chart for Sales and Expenses ---
            var dataSheetName = worksheet.Name;
            var chart = worksheet.Drawings.AddChart("MonthlySalesExpensesChart", eChartType.ColumnClustered);
            chart.Title.Text = $"Sales and Expenses for {salesCollection.month} {DateTime.Now.Date.ToString("YYYY")}";
            chart.SetPosition(1, 0, 5, 0); // Below data, starting in column E (index 5)
            chart.SetSize(800, 400);

            var salesSeries = chart.Series.Add(worksheet.Cells[2, 2, weeks.Count + 1, 2],
                worksheet.Cells[2, 1, weeks.Count + 1, 1]);
            salesSeries.Header = "Sales";

            var expensesSeries = chart.Series.Add(worksheet.Cells[2, 3, weeks.Count + 1, 3],
                worksheet.Cells[2, 1, weeks.Count + 1, 1]);
            expensesSeries.Header = "Expenses";
            chart.Legend.Position = eLegendPosition.Bottom;

            // --- Line Chart for Sales Trend ---
            // If you want this on a separate sheet:
            var worksheetGraph = package.Workbook.Worksheets.Add("Monthly Graphs");
            var lineChart = worksheetGraph.Drawings.AddChart("MonthlySalesTrendChart", eChartType.Line);
            // To add to the same sheet:
            // var lineChart = worksheet.Drawings.AddChart("MonthlySalesTrendChart", eChartType.Line);

            lineChart.Title.Text =
                $"Tendencia de ventas de {salesCollection.month} {DateTime.Now.Date.ToString("YYYY")}";
            // Adjust position if on the same sheet to avoid overlap
            // If on new sheet:
            lineChart.SetPosition(0, 0, 0, 0); // Top-left of the "Monthly Graphs" sheet
            lineChart.SetSize(800, 400);


            var lineSeriesSales =
                lineChart.Series.Add(package.Workbook.Worksheets[dataSheetName].Cells[2, 2, weeks.Count + 1, 2],
                    package.Workbook.Worksheets[dataSheetName].Cells[2, 1, weeks.Count + 1, 1]);
            lineSeriesSales.Header = "Sales";
            lineChart.Legend.Position = eLegendPosition.Bottom;


            try
            {
                var filepath = Path.Combine(folderPath, baseName);
                var fileInfo = new FileInfo(filepath);
                package.SaveAs(fileInfo);

                return ResultFromService.SuccessResult(filepath, "Reporte generado correctamente");
            }
            catch (Exception e)
            {
                return ResultFromService.Failed("Hubo un error al generar el reporte: " + e.Message);
            }
        }
    }

    public static ResultFromService GenerateReport<T>(DateTime fromDate, DateTime untilDate, List<T> salesCollection,
        List<T> expensesCollection,
        Func<T, DateTime> dateSelector, Func<T, decimal> totalSelector, string finalPath) where T : class
    {
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Datos");

                worksheet.Cells[1, 1].Value = "Fecha";
                worksheet.Cells[1, 2].Value = "Ventas";
                worksheet.Cells[1, 3].Value = "Gastos";


                var sales = salesCollection.Select(x => totalSelector(x)).ToList();
                var expenses = expensesCollection.Select(x => totalSelector(x)).ToList();


                for (var i = 0; i < expenses.Count; i++)
                {
                    worksheet.Cells[i + 2, 3].Value = expenses[i];
                    worksheet.Cells[i + 2, 3].Style.Numberformat.Format = "#,##0.00";
                }

                for (var i = 0; i < sales.Count; i++)
                {
                    var day = fromDate.AddDays(i);
                    worksheet.Cells[i + 2, 1].Value =
                        dateSelector(salesCollection[i]).ToString("ddMMMM", new CultureInfo("es-ES"));
                    worksheet.Cells[i + 2, 2].Value = sales[i];

                    worksheet.Cells[i + 2, 2].Style.Numberformat.Format = "#,##0.00";
                }

                using (var range = worksheet.Cells[1, 1, 1, 3])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
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

                if (finalPath == null) finalPath = filePath;
                else if (!finalPath.EndsWith(".xlsx"))
                    finalPath = Path.Combine(finalPath, filePath);
                package.SaveAs(new FileInfo(finalPath));
                return ResultFromService.SuccessResult(finalPath,
                    "Reporte generado exitosamente en: " + finalPath);
            }
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
            return ResultFromService.Failed("Error al generar el reporte: " + e.Message);
        }
    }

    public static ResultFromService GenerateDailyReport(List<DTOSetTicketDetailsReport> ticketDetailsCollection,
        List<DTOGetExpense> expensesCollection,
        string filePath)
    {
        try
        {
            var today = DateTime.Now.Date.ToString("dd-MMMM-yyyy",
                new CultureInfo("es-ES"));
            ExcelPackage.LicenseContext =
                LicenseContext.NonCommercial; // O LicenseContext.Commercial si tienes licencia

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Datos");

                // --- Definición de Cabeceras ---
                worksheet.Cells[1, 1].Value = "Hora";
                worksheet.Cells[1, 2].Value = "Descripción"; // Para Nombre Producto o Razón Gasto
                worksheet.Cells[1, 3].Value = "Cantidad Producto";
                worksheet.Cells[1, 4].Value = "Total Venta";
                worksheet.Cells[1, 5].Value = "Total Gasto";

                // --- Estilo para Cabeceras ---
                using (var range = worksheet.Cells[1, 1, 1, 5]) // Ajustado al número de columnas
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // --- Llenado de Datos ---
                var numTicketDetails = ticketDetailsCollection?.Count ?? 0;
                var numExpenses = expensesCollection?.Count ?? 0;
                var maxRows = Math.Max(numTicketDetails, numExpenses);

                for (var i = 0; i < maxRows; i++)
                {
                    var currentRow = i + 2; // Los datos comienzan en la fila 2

                    // Datos de DTOSetTicketDetailsReport (Ventas)
                    if (i < numTicketDetails)
                    {
                        // Columna 1 (Hora): DTOSetTicketDetailsReport no tiene hora.
                        // Si tuvieras una hora para la venta, la pondrías aquí.
                        // worksheet.Cells[currentRow, 1].Value = ticketDetailsCollection[i].HoraVenta; (Ejemplo)

                        worksheet.Cells[currentRow, 2].Value =
                            ticketDetailsCollection[i].Article; // Descripción (Nombre Producto)
                        worksheet.Cells[currentRow, 3].Value = ticketDetailsCollection[i].Quantity; // Cantidad Producto
                        worksheet.Cells[currentRow, 3].Style.Numberformat.Format = "#,##0"; // Formato para cantidad
                        worksheet.Cells[currentRow, 4].Value = ticketDetailsCollection[i].TotalPrice; // Total Venta
                        worksheet.Cells[currentRow, 4].Style.Numberformat.Format = "$#,##0.00"; // Formato moneda
                    }

                    // Datos de DTOGetExpense (Gastos)
                    if (i < numExpenses)
                    {
                        worksheet.Cells[currentRow, 1].Value =
                            expensesCollection[i].Date.ToString("HH:mm:ss"); // Hora del Gasto

                        // Si no hay un detalle de ticket en esta fila pero sí un gasto,
                        // la descripción del gasto se pone en la columna "Descripción".
                        if (i >= numTicketDetails)
                            worksheet.Cells[currentRow, 2].Value =
                                expensesCollection[i].Reason; // Descripción (Razón Gasto)
                        else if
                            (worksheet.Cells[currentRow, 2].Value ==
                             null) // Si la celda de descripción está vacía (porque no hay producto)
                            worksheet.Cells[currentRow, 2].Value = expensesCollection[i].Reason;


                        worksheet.Cells[currentRow, 5].Value = expensesCollection[i].Amount; // Total Gasto
                        worksheet.Cells[currentRow, 5].Style.Numberformat.Format = "$#,##0.00"; // Formato moneda
                    }
                }

                // --- Autoajuste de Columnas ---
                for (var col = 1; col <= 5; col++) worksheet.Column(col).AutoFit();

                // --- Gráficos ---
                // Gráfico 1: Ventas y Gastos por Descripción (Columnas Agrupadas)
                if (maxRows > 0) // Solo crear gráficos si hay datos
                {
                    var chart = worksheet.Drawings.AddChart("GraficoVentasGastos", eChartType.ColumnClustered);
                    chart.Title.Text = $"Ventas y Gastos de {today}";
                    chart.SetPosition(1, 0, 6, 0); // Fila 2 (índice 1), Columna F (índice 6)
                    chart.SetSize(800, 400);

                    // Serie de Ventas
                    if (numTicketDetails > 0)
                    {
                        var series1 = chart.Series.Add(worksheet.Cells[2, 4, numTicketDetails + 1, 4],
                            worksheet.Cells[2, 2, numTicketDetails + 1, 2]);
                        series1.Header = "Ventas";
                    }

                    // Serie de Gastos
                    if (numExpenses > 0)
                    {
                        // Para la serie de gastos, usamos las descripciones de la columna B como etiquetas.
                        // Si un gasto no tiene un producto coincidente en la misma fila, su "Razón" se habrá colocado en la Col B.
                        var series2 = chart.Series.Add(worksheet.Cells[2, 5, numExpenses + 1, 5],
                            worksheet.Cells[2, 2, numExpenses + 1, 2]);
                        series2.Header = "Gastos";
                    }

                    chart.Legend.Position = eLegendPosition.Bottom;


                    // Hoja para Gráficos de Líneas (si se desea mantener)
                    var worksheetGraph = package.Workbook.Worksheets.Add("Tendencias");

                    // Gráfico 2: Tendencia de Ventas (Líneas)
                    if (numTicketDetails > 0)
                    {
                        var lineChartSales =
                            worksheetGraph.Drawings.AddChart("GraficoTendenciaVentas", eChartType.Line);
                        lineChartSales.Title.Text = "Tendencia de Ventas (por Artículo)";
                        lineChartSales.SetPosition(0, 0, 0, 0); // Fila 1, Col A en la nueva hoja
                        lineChartSales.SetSize(800, 400);
                        // Usa la Descripción (Nombre Artículo) para el eje X y Total Venta para el eje Y
                        var lineSeriesSales = lineChartSales.Series.Add(worksheet.Cells[2, 4, numTicketDetails + 1, 4],
                            worksheet.Cells[2, 2, numTicketDetails + 1, 2]);
                        lineSeriesSales.Header = "Ventas";
                        lineChartSales.Legend.Position = eLegendPosition.Bottom;
                    }

                    // Gráfico 3: Tendencia de Gastos (Líneas)
                    if (numExpenses > 0)
                    {
                        var lineChartExpenses =
                            worksheetGraph.Drawings.AddChart("GraficoTendenciaGastos", eChartType.Line);
                        lineChartExpenses.Title.Text = "Tendencia de Gastos (por Hora)";
                        lineChartExpenses.SetPosition(20, 0, 0, 0); // Debajo del anterior, Fila 21, Col A
                        lineChartExpenses.SetSize(800, 400);
                        // Usa la Hora del Gasto (Col A) para el eje X y Total Gasto (Col E) para el eje Y
                        var lineSeriesExpenses = lineChartExpenses.Series.Add(worksheet.Cells[2, 5, numExpenses + 1, 5],
                            worksheet.Cells[2, 1, numExpenses + 1, 1]);
                        lineSeriesExpenses.Header = "Gastos";
                        lineChartExpenses.Legend.Position = eLegendPosition.Bottom;
                    }
                }

                var fullPath =
                    Path.Combine(filePath,
                        $"Reporte_{today.Replace("-", "_")}.xlsx"); // Reemplazar caracteres no válidos si es necesario
                package.SaveAs(new FileInfo(fullPath));
                return ResultFromService.SuccessResult(fullPath, "Reporte generado exitosamente en: " + fullPath);
            }
        }
        catch (Exception e)
        {
            // Considerar loguear la excepción completa (e.ToString()) para más detalles
            return ResultFromService.Failed("Error al generar el reporte: " + e.Message);
        }
    }
}