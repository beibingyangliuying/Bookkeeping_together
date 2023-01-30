using System;
using System.Windows;
using UserControls.Interface;

namespace UserControls;

public partial class SelectYear : IDateSelector
{
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    public SelectYear() => InitializeComponent();

    private void SelectYear_OnLoaded(object sender, RoutedEventArgs e)
    {
        IntegerUpDownYear.Minimum = IDateSelector.MinYear;
        IntegerUpDownYear.Maximum = IDateSelector.MaxYear;

        var now = DateTime.Now;
        IntegerUpDownYear.Value = now.Year;
    }

    private void IntegerUpDownYear_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        #region Check

        if (IntegerUpDownYear.Value == null) return;
        var year = (int)IntegerUpDownYear.Value;

        #endregion

        Start = new DateTime(year, IDateSelector.MinMonth, 1);
        End = new DateTime(year, IDateSelector.MaxMonth, 31); // There are always 31 days in December.

        LabelStart.Content = Start.ToString("yyyy-M-d dddd");
        LabelEnd.Content = End.ToString("yyyy-M-d dddd");
    }
}