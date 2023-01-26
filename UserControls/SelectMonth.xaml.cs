using System;
using System.Windows;
using System.Windows.Controls;
using UserControls.Interface;

namespace UserControls
{
    /// <summary>
    /// SelectMonth.xaml 的交互逻辑
    /// </summary>
    public partial class SelectMonth : UserControl, IDateSelector
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public SelectMonth()
        {
            InitializeComponent();
        }

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
            int year = (int)IntegerUpDownYear.Value;
            int month = (int)IntegerUpDownMonth.Value;
            Start = new DateTime(year, month, 1);
        }
    }
}