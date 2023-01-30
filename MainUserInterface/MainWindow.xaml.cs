using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ScottPlot;
using ScottPlot.Plottable;
using SqlContext.Context;
using SqlContext.Serialization;
using Xceed.Wpf.Toolkit;

namespace MainUserInterface;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    #region InstanceField

    private Crosshair _crosshairLineIncomeMonth;
    private Crosshair _crosshairLineOutcomeMonth;
    private Crosshair _crosshairLineBalanceMonth;
    private Crosshair _crosshairLineIncomeYear;
    private Crosshair _crosshairLineOutcomeYear;
    private Crosshair _crosshairLineBalanceYear;

    #endregion

    #region NestedClass

    private static class PlotManage
    {
        #region StaticField

        private static readonly Func<double, string> DateFormatter = d =>
            DateTime.FromOADate(d).ToString("yyyy-M-d dddd");

        private static readonly Func<double, string> DoubleFormatter = d => d.ToString("F");

        #endregion


        public static Crosshair CreateCrosshair(WpfPlot wpfPlot, (double x, double y) point)
        {
            var plt = wpfPlot.Plot;
            var crosshair = plt.AddCrosshair(point.x, point.y);
            crosshair.VerticalLine.PositionFormatter = DateFormatter;
            crosshair.HorizontalLine.PositionFormatter = DoubleFormatter;
            return crosshair;
        }

        public static void SetCrosshairPosition(WpfPlot wpfPlot, Crosshair crosshair, (double x, double y) point)
        {
            crosshair.X = point.x;
            crosshair.Y = point.y;
            wpfPlot.Refresh();
        }
    }

    private static class Algorithm
    {
        public static DateTime[] GenerateDateSeries(in DateTime startDate, in DateTime endDate, in TimeSpan span)
        {
            var count = (endDate - startDate).Days + 1;
            if (count <= 0)
            {
                throw new ArgumentException($"{endDate} is earlier than {startDate}!");
            }

            var result = new DateTime[count];
            result[0] = startDate;

            for (var i = 1; i < count; i++)
            {
                var temp = result[i - 1] + span;
                if (temp <= endDate)
                {
                    result[i] = temp;
                }
            }

            return result;
        }
    }

    #endregion

    public MainWindow()
    {
        InitializeComponent();

        #region InitializeWpfplot

        _crosshairLineIncomeMonth = PlotManage.CreateCrosshair(WpfPlotLineIncomeMonth, (0, 0));
        _crosshairLineOutcomeMonth = PlotManage.CreateCrosshair(WpfPlotLineOutcomeMonth, (0, 0));
        _crosshairLineBalanceMonth = PlotManage.CreateCrosshair(WpfPlotLineBalanceMonth, (0, 0));
        _crosshairLineIncomeYear = PlotManage.CreateCrosshair(WpfPlotLineIncomeYear, (0, 0));
        _crosshairLineOutcomeYear = PlotManage.CreateCrosshair(WpfPlotLineOutcomeYear, (0, 0));
        _crosshairLineBalanceYear = PlotManage.CreateCrosshair(WpfPlotLineBalanceYear, (0, 0));

        #endregion
    }

    #region EventHandler

    private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        await using var context = new BookkeepingContext();
    }

    private void DropDownButtonSelectMonth_OnClosed(object sender, RoutedEventArgs e)
    {
    }

    /// <summary>
    /// Make crosshair visible when mouse entered.
    /// </summary>
    private void WpfPlot_OnMouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is not WpfPlot wpfPlot)
        {
            throw new ArgumentNullException(nameof(wpfPlot), "Not initialized!");
        }

        switch (wpfPlot.Name)
        {
            #region LineSeries

            case nameof(WpfPlotLineIncomeMonth):
                _crosshairLineIncomeMonth.IsVisible = true;
                break;
            case nameof(WpfPlotLineOutcomeMonth):
                _crosshairLineOutcomeMonth.IsVisible = true;
                break;
            case nameof(WpfPlotLineBalanceMonth):
                _crosshairLineBalanceMonth.IsVisible = true;
                break;
            case nameof(WpfPlotLineIncomeYear):
                _crosshairLineIncomeYear.IsVisible = true;
                break;
            case nameof(WpfPlotLineOutcomeYear):
                _crosshairLineOutcomeYear.IsVisible = true;
                break;
            case nameof(WpfPlotLineBalanceYear):
                _crosshairLineBalanceYear.IsVisible = true;
                break;

            #endregion
        }
    }

    /// <summary>
    /// Track mouse to update Crosshair.
    /// </summary>
    /// <exception cref="ArgumentNullException">Exception for null wpfplot</exception>
    private void WpfPlot_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (sender is not WpfPlot wpfPlot)
        {
            throw new ArgumentNullException(nameof(wpfPlot), "Not initialized!");
        }

        var (x, y) = wpfPlot.GetMouseCoordinates();

        switch (wpfPlot.Name)
        {
            #region LineSeries

            case nameof(WpfPlotLineIncomeMonth):
                PlotManage.SetCrosshairPosition(wpfPlot, _crosshairLineIncomeMonth, (x, y));
                break;
            case nameof(WpfPlotLineOutcomeMonth):
                PlotManage.SetCrosshairPosition(wpfPlot, _crosshairLineOutcomeMonth, (x, y));
                break;
            case nameof(WpfPlotLineBalanceMonth):
                PlotManage.SetCrosshairPosition(wpfPlot, _crosshairLineBalanceMonth, (x, y));
                break;
            case nameof(WpfPlotLineIncomeYear):
                PlotManage.SetCrosshairPosition(wpfPlot, _crosshairLineIncomeYear, (x, y));
                break;
            case nameof(WpfPlotLineOutcomeYear):
                PlotManage.SetCrosshairPosition(wpfPlot, _crosshairLineOutcomeYear, (x, y));
                break;
            case nameof(WpfPlotLineBalanceYear):
                PlotManage.SetCrosshairPosition(wpfPlot, _crosshairLineBalanceYear, (x, y));
                break;

            #endregion
        }
    }

    /// <summary>
    /// Make crosshair invisible when mouse leaved.
    /// </summary>
    private void WpfPlot_OnMouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is not WpfPlot wpfPlot)
        {
            throw new ArgumentNullException(nameof(wpfPlot), "Not initialized!");
        }

        switch (wpfPlot.Name)
        {
            #region LineSeries

            case nameof(WpfPlotLineIncomeMonth):
                _crosshairLineIncomeMonth.IsVisible = false;
                break;
            case nameof(WpfPlotLineOutcomeMonth):
                _crosshairLineOutcomeMonth.IsVisible = false;
                break;
            case nameof(WpfPlotLineBalanceMonth):
                _crosshairLineBalanceMonth.IsVisible = false;
                break;
            case nameof(WpfPlotLineIncomeYear):
                _crosshairLineIncomeYear.IsVisible = false;
                break;
            case nameof(WpfPlotLineOutcomeYear):
                _crosshairLineOutcomeYear.IsVisible = false;
                break;
            case nameof(WpfPlotLineBalanceYear):
                _crosshairLineBalanceYear.IsVisible = false;
                break;

            #endregion
        }

        wpfPlot.Refresh();
    }

    #endregion


    #region Procedure

    /// <summary>
    /// Plot line series in wpfplot control.
    /// </summary>
    /// <param name="wpfPlot">The WpfPlot control.<seealso cref="WpfPlot"/></param>
    /// <param name="start">The number of hours, minutes and seconds need to be zero.</param>
    /// <param name="end">The number of hours, minutes and seconds need to be zero.</param>
    // private async Task PlotLineSeries(WpfPlot wpfPlot, DateTime start, DateTime end)
    // {
    //     await using var context = new BookkeepingContext();
    //
    //     var dateList = Algorithm.GenerateDateSeries(start, end, new TimeSpan(1, 0, 0, 0));
    //
    //     var xPoints = new double[dateList.Length];
    //     var yPoints = new double[dateList.Length];
    //     var result = Array.Empty<DateAmount>();
    //
    //     #region QueryData
    //
    //     switch (wpfPlot.Name)
    //     {
    //         case nameof(WpfplotIncomeLineChart):
    //             var incomesByDay = BookkeepingContext.QueryMonthIncomesByDay(context, start, end);
    //             result = dateList.GroupJoin(incomesByDay, date => date, dateAmount => dateAmount.Date,
    //                     (date, dateAmount) => new DateAmount
    //                     {
    //                         Date = date,
    //                         Amount = dateAmount.Sum(amount => amount.Amount)
    //                     })
    //                 .ToArray();
    //
    //             _crosshairLineIncomeMonth = PlotManage.CreateCrosshair(wpfPlot, (xPoints.First(), yPoints.First()));
    //             break;
    //         case nameof(WpfPlotOutcomeLineChart):
    //             var outcomesByDay = BookkeepingContext.QueryMonthOutcomesByDay(context, start, end);
    //             result = dateList.GroupJoin(outcomesByDay, date => date, dateAmount => dateAmount.Date,
    //                     (date, dateAmount) => new DateAmount
    //                     {
    //                         Date = date,
    //                         Amount = dateAmount.Sum(amount => amount.Amount)
    //                     })
    //                 .ToArray();
    //
    //
    //             break;
    //     }
    //
    //     // The length of dateAmounts is equal with result
    //     for (var i = 0; i < result.Length; i++)
    //     {
    //         xPoints[i] = result[i].Date.ToOADate();
    //         yPoints[i] = result[i].Amount;
    //     }
    //
    //     #endregion
    //
    //     #region Plot
    //
    //     var plt = wpfPlot.Plot;
    //     plt.Clear();
    //     plt.XLabel("Datetime");
    //     plt.YLabel("Amount");
    //     plt.XAxis.DateTimeFormat(true);
    //     plt.AddScatter(xPoints, yPoints);
    //
    //     switch (wpfPlot.Name)
    //     {
    //         case nameof(WpfplotIncomeLineChart):
    //             plt.Title("Income Line Chart");
    //             _crosshairLineIncomeMonth = PlotManage.CreateCrosshair(wpfPlot, (xPoints.First(), yPoints.First()));
    //             break;
    //         case nameof(WpfPlotOutcomeLineChart):
    //             plt.Title("Outcome Line Chart");
    //             _crosshairLineOutcomeMonth = plt.AddCrosshair(xPoints[0], yPoints[0]);
    //             break;
    //     }
    //
    //     wpfPlot.Refresh();
    //
    //     #endregion
    // }

    #endregion
}