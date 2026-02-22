using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StressCheckAvalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Opened += OnOpened;
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        var screen = Screens.Primary;
        if (screen is not null)
        {
            Width = Math.Max(screen.WorkingArea.Width / screen.Scaling * 0.70, 1100);
            Height = Math.Max(screen.WorkingArea.Height / screen.Scaling * 0.70, 700);
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
