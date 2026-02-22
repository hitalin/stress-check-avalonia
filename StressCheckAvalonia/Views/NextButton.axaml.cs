using Avalonia.Controls;
using Avalonia.Interactivity;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Views;

public partial class NextButton : UserControl
{
    private readonly StateViewModel _stateViewModel;

    public NextButton()
    {
        InitializeComponent();
        _stateViewModel = ServiceLocator.GetRequired<StateViewModel>();
        DataContext = _stateViewModel;
    }

    public void ClickHandler(object sender, RoutedEventArgs args)
    {
        if (sender is Button)
        {
            _stateViewModel.HandleNext();
        }
    }
}
