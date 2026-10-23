using System.Windows;

namespace EightQueens;

/// <summary>
/// Interaktionslogik für MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        LayoutRoot.DataContext = new MainViewModel();
    }
}
