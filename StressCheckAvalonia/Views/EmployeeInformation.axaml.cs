using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Views;

public partial class EmployeeInformation : UserControl
{
    public EmployeeInformation()
    {
        InitializeComponent();
        DataContext = ServiceLocator.GetRequired<EmployeeViewModel>();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public bool IsInformationComplete()
    {
        var employeeViewModel = DataContext as EmployeeViewModel;
        return employeeViewModel?.IsInformationComplete() ?? false;
    }
}