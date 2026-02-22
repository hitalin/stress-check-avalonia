using Avalonia.Controls;
using Avalonia.Interactivity;
using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Views;

public partial class BackButtons : UserControl
{
    private readonly StateViewModel _stateViewModel;

    public BackButtons()
    {
        InitializeComponent();
        _stateViewModel = ServiceLocator.GetRequired<StateViewModel>();
        DataContext = _stateViewModel;
    }

    public void BackToTitle_Click(object sender, RoutedEventArgs args)
    {
        _stateViewModel.CurrentState = State.Input;
    }

    public void BackOneScreen_Click(object sender, RoutedEventArgs args)
    {
        _stateViewModel.HandleBack();
    }
}
