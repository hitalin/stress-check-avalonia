using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Views;

public partial class AppHeader : UserControl
{
    public AppHeader()
    {
        InitializeComponent();
        DataContext = ServiceLocator.GetRequired<EmployeeViewModel>();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
