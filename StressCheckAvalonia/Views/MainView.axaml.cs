using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        var stateViewModel = ServiceLocator.GetRequired<StateViewModel>();
        DataContext = stateViewModel;

        stateViewModel.Initialize();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
