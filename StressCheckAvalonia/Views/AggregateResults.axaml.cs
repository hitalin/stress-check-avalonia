using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Views;

public partial class AggregateResults : UserControl
{
    public AggregateResults()
    {
        InitializeComponent();
        var resultsViewModel = ServiceLocator.GetRequired<ResultsViewModel>();
        DataContext = resultsViewModel;

        var stateViewModel = ServiceLocator.GetRequired<StateViewModel>();
        stateViewModel.WhenAnyValue(x => x.CurrentState)
            .Subscribe(Observer.Create<State>(state =>
            {
                if (state == State.Aggregated)
                {
                    resultsViewModel.Calculate();
                    IsVisible = true;
                }
                else
                {
                    IsVisible = false;
                }
            }));
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
