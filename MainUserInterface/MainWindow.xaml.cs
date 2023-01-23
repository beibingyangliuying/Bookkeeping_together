using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using SqlContext.ContextClass;
using SqlContext.ModelClass;

namespace MainUserInterface;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow() => InitializeComponent();

    private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        #region InitializeDatabase

        await using var context = new BookkeepingContext();
        await context.Database.EnsureCreatedAsync();

        #endregion

        #region InitializeUiElement

        LabelDate.Content = "Current Date: " + DateTime.Now.ToString("yyyy-MM-dd");

        #endregion
    }

    private void CheckBox_OnChecked(object sender, RoutedEventArgs e)
    {
        var checkBox = sender as CheckBox;

        switch (checkBox?.Name)
        {
            case nameof(CheckBoxDate):
                DatePickerBeginDate.IsEnabled = true;
                DatePickerEndDate.IsEnabled = true;
                break;
            case nameof(CheckBoxInAccount):
                ComboBoxInAccount.IsEnabled = true;
                break;
            case nameof(CheckBoxInCategory):
                ComboBoxInCategory.IsEnabled = true;
                break;
            case nameof(CheckBoxOutAccount):
                ComboBoxOutAccount.IsEnabled = true;
                break;
            case nameof(CheckBoxOutCategory):
                ComboBoxOutCategory.IsEnabled = true;
                break;
        }
    }

    private void CheckBox_OnUnchecked(object sender, RoutedEventArgs e)
    {
        var checkBox = sender as CheckBox;

        switch (checkBox?.Name)
        {
            case nameof(CheckBoxDate):
                DatePickerBeginDate.IsEnabled = false;
                DatePickerEndDate.IsEnabled = false;
                break;
            case nameof(CheckBoxInAccount):
                ComboBoxInAccount.IsEnabled = false;
                break;
            case nameof(CheckBoxInCategory):
                ComboBoxInCategory.IsEnabled = false;
                break;
            case nameof(CheckBoxOutAccount):
                ComboBoxOutAccount.IsEnabled = false;
                break;
            case nameof(CheckBoxOutCategory):
                ComboBoxOutCategory.IsEnabled = false;
                break;
        }
    }
}