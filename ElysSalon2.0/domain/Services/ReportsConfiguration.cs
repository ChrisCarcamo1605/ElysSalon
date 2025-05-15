using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using ElysSalon2._0.aplication.DTOs.Request.Reports;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.domain.Services;

public class ReportsConfiguration
{
    private DateTime Week1Start = DateTime.Now;
    private DateTime Week1End = DateTime.Now;

    private DateTime Week2Start = DateTime.Now;
    private DateTime Week2End = DateTime.Now;

    private DateTime Week3Start = DateTime.Now;
    private DateTime Week3End = DateTime.Now;

    private DateTime Week4Start = DateTime.Now;
    private DateTime Week4End = DateTime.Now;


    public DtoWeeksRanges GetWeeksRanges(ObservableCollection<Sales> collection)
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);

        var firstDayMonth = new DateTime(now.Year, now.Month, 1);
        var lastDayMonth = firstDayMonth.AddMonths(1).AddDays(-1);

        var totalsDaysInMonth = (int)(lastDayMonth - firstDayMonth).TotalDays + 1;
        var daysPerWeek = (double)totalsDaysInMonth / 4;

        // MessageBox.Show("DayPerWeek " + daysPerWeek + "Mes: " + now.Month);

        if (daysPerWeek == 7.75)
        {
            Week1Start = firstDayMonth;
            Week1End = firstDayMonth.AddDays(daysPerWeek).AddHours(6).AddSeconds(-1);

            Week2Start = Week1End.AddSeconds(1);
            Week2End = firstDayMonth.AddDays(daysPerWeek * 2).AddHours(12).AddSeconds(-1);

            Week3Start = Week2End.AddSeconds(1);
            Week3End = firstDayMonth.AddDays(daysPerWeek * 3).AddHours(-6).AddSeconds(-1);

            Week4Start = Week3End.AddSeconds(1);
            Week4End = lastDayMonth.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
        else if (daysPerWeek == 7.25)
        {
            Week1Start = firstDayMonth;
            Week1End = firstDayMonth.AddDays(daysPerWeek).AddHours(-6).AddSeconds(-1);

            Week2Start = Week1End.AddSeconds(1);
            Week2End = firstDayMonth.AddDays(daysPerWeek * 2).AddHours(12).AddSeconds(-1);

            Week3Start = Week2End.AddSeconds(1);
            Week3End = firstDayMonth.AddDays(daysPerWeek * 3).AddHours(6).AddSeconds(-1);

            Week4Start = Week3End.AddSeconds(1);
            Week4End = lastDayMonth.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
        else if (daysPerWeek == 7)
        {
            Week1Start = firstDayMonth;
            Week1End = firstDayMonth.AddDays(daysPerWeek).AddSeconds(-1);

            Week2Start = Week1End.AddSeconds(1);
            Week2End = firstDayMonth.AddDays(daysPerWeek * 2).AddSeconds(-1);

            Week3Start = Week2End.AddSeconds(1);
            Week3End = firstDayMonth.AddDays(daysPerWeek * 3).AddSeconds(-1);

            Week4Start = Week3End.AddSeconds(1);
            Week4End = lastDayMonth.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
        else if (daysPerWeek == 7.5)
        {
            MessageBox.Show("dentro del 7.5");
            Week1Start = firstDayMonth;
            Week1End = firstDayMonth.AddDays(daysPerWeek).AddHours(-12).AddSeconds(-1);

            Week2Start = Week1End.AddSeconds(1);
            Week2End = firstDayMonth.AddDays(daysPerWeek * 2).AddSeconds(-1);

            Week3Start = Week2End.AddSeconds(1);
            Week3End = firstDayMonth.AddDays(daysPerWeek * 3).AddHours(-12).AddSeconds(-1);

            Week4Start = Week3End.AddSeconds(1);
            Week4End = lastDayMonth.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        var month = DateTime.Now.ToString("MMMM", new CultureInfo("es-ES")).ToUpperInvariant();

        //MessageBox.Show(
        //    $"Week1Start= {Week1Start} Week1End= {Week1End} Week2Start= {Week2Start} " +
        //    $"Week2End ={Week2End} Week3Start= {Week3Start} Week3End= {Week3End} Week4Start= {Week4Start} Week4End= {Week4End} " +
        //    $"DIAS POR SEMANA={daysPerWeek} MES= {month}" +
        //    $"");


        return new DtoWeeksRanges(Week1Start, Week1End, Week2Start, Week2End, Week3Start, Week3End, Week4Start,
            Week4End);
    }

    public DtoWeeksRanges GetWeeksRanges(ObservableCollection<Ticket> collection)
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);

        var firstDayMonth = new DateTime(now.Year, now.Month, 1);
        var lastDayMonth = firstDayMonth.AddMonths(1).AddDays(-1);

        var totalsDaysInMonth = (int)(lastDayMonth - firstDayMonth).TotalDays + 1;
        var daysPerWeek = (double)totalsDaysInMonth / 4;

        // MessageBox.Show("DayPerWeek " + daysPerWeek + "Mes: " + now.Month);

        if (daysPerWeek == 7.75)
        {
            Week1Start = firstDayMonth;
            Week1End = firstDayMonth.AddDays(daysPerWeek).AddHours(6).AddSeconds(-1);

            Week2Start = Week1End.AddSeconds(1);
            Week2End = firstDayMonth.AddDays(daysPerWeek * 2).AddHours(12).AddSeconds(-1);

            Week3Start = Week2End.AddSeconds(1);
            Week3End = firstDayMonth.AddDays(daysPerWeek * 3).AddHours(-6).AddSeconds(-1);

            Week4Start = Week3End.AddSeconds(1);
            Week4End = lastDayMonth.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
        else if (daysPerWeek == 7.25)
        {
            Week1Start = firstDayMonth;
            Week1End = firstDayMonth.AddDays(daysPerWeek).AddHours(-6).AddSeconds(-1);

            Week2Start = Week1End.AddSeconds(1);
            Week2End = firstDayMonth.AddDays(daysPerWeek * 2).AddHours(12).AddSeconds(-1);

            Week3Start = Week2End.AddSeconds(1);
            Week3End = firstDayMonth.AddDays(daysPerWeek * 3).AddHours(6).AddSeconds(-1);

            Week4Start = Week3End.AddSeconds(1);
            Week4End = lastDayMonth.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
        else if (daysPerWeek == 7)
        {
            Week1Start = firstDayMonth;
            Week1End = firstDayMonth.AddDays(daysPerWeek).AddSeconds(-1);

            Week2Start = Week1End.AddSeconds(1);
            Week2End = firstDayMonth.AddDays(daysPerWeek * 2).AddSeconds(-1);

            Week3Start = Week2End.AddSeconds(1);
            Week3End = firstDayMonth.AddDays(daysPerWeek * 3).AddSeconds(-1);

            Week4Start = Week3End.AddSeconds(1);
            Week4End = lastDayMonth.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
        else if (daysPerWeek == 7.5)
        {
            MessageBox.Show("dentro del 7.5");
            Week1Start = firstDayMonth;
            Week1End = firstDayMonth.AddDays(daysPerWeek).AddHours(-12).AddSeconds(-1);

            Week2Start = Week1End.AddSeconds(1);
            Week2End = firstDayMonth.AddDays(daysPerWeek * 2).AddSeconds(-1);

            Week3Start = Week2End.AddSeconds(1);
            Week3End = firstDayMonth.AddDays(daysPerWeek * 3).AddHours(-12).AddSeconds(-1);

            Week4Start = Week3End.AddSeconds(1);
            Week4End = lastDayMonth.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        var month = DateTime.Now.ToString("MMMM", new CultureInfo("es-ES")).ToUpperInvariant();

        //MessageBox.Show(
        //    $"Week1Start= {Week1Start} Week1End= {Week1End} Week2Start= {Week2Start} " +
        //    $"Week2End ={Week2End} Week3Start= {Week3Start} Week3End= {Week3End} Week4Start= {Week4Start} Week4End= {Week4End} " +
        //    $"DIAS POR SEMANA={daysPerWeek} MES= {month}" +
        //    $"");


        return new DtoWeeksRanges(Week1Start, Week1End, Week2Start, Week2End, Week3Start, Week3End, Week4Start,
            Week4End);
    }
}