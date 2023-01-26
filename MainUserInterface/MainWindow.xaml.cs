using System.Windows;
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

        await BookkeepingContext.InitializeDataBase();

        #endregion

        #region InitializeUiElement

        #endregion
    }
}