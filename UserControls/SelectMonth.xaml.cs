using System;
using System.Windows;
using UserControls.Interface;

namespace UserControls
{
    /// <summary>
    /// SelectMonth.xaml 的交互逻辑
    /// </summary>
    public partial class SelectMonth : IDateSelector
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public SelectMonth() => InitializeComponent();

        private void UserControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            IntegerUpDownYear.Minimum = IDateSelector.MinYear;
            IntegerUpDownYear.Maximum = IDateSelector.MaxYear;
            IntegerUpDownMonth.Minimum = IDateSelector.MinMonth;
            IntegerUpDownMonth.Maximum = IDateSelector.MaxMonth;

            var now = DateTime.Now;
            IntegerUpDownYear.Value = now.Year;
            IntegerUpDownMonth.Value = now.Month;
        }

        private void IntegerUpDown_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            #region Check

            if (IntegerUpDownYear.Value == null) return;
            var year = (int)IntegerUpDownYear.Value;
            if (IntegerUpDownMonth.Value == null) return;
            var month = (int)IntegerUpDownMonth.Value;

            #endregion

            Start = new DateTime(year, month, 1);
            End = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            LabelStart.Content = Start.ToString("yyyy-M-d dddd");
            LabelEnd.Content = End.ToString("yyyy-M-d dddd");
        }
    }
}