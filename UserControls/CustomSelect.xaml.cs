using System;
using System.Windows;
using System.Windows.Controls;
using UserControls.Interface;

namespace UserControls;

public partial class CustomSelect : IDateSelector
{
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    public CustomSelect() => InitializeComponent();

    private void CustomSelect_OnLoaded(object sender, RoutedEventArgs e)
    {
        var minDate = new DateTime(IDateSelector.MinYear, IDateSelector.MinMonth, 1);
        var maxDate =
            new DateTime(IDateSelector.MaxYear, IDateSelector.MaxMonth, 31); // There are always 31 days in December

        DatePickerStart.DisplayDateStart = minDate;
        DatePickerStart.DisplayDateEnd = maxDate;
        DatePickerEnd.DisplayDateStart = minDate;
        DatePickerEnd.DisplayDateEnd = maxDate;

        DatePickerEnd.SelectedDate = DateTime.Today;
        DatePickerStart.SelectedDate = DateTime.Today - new TimeSpan(30, 0, 0, 0, 0);
    }

    private void DatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        var datePicker = sender as DatePicker;
        switch (datePicker?.Name)
        {
            case nameof(DatePickerStart):

                break;
            case nameof(DatePickerEnd):

                break;
            default:
                throw new InvalidOperationException($"{nameof(DatePicker_OnSelectedDateChanged)}: Invalid argument!");
        }
    }
}