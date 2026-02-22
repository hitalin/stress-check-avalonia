using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Views;

public partial class SectionDescription : UserControl
{
    public SectionDescription()
    {
        InitializeComponent();
        DataContext = ServiceLocator.GetRequired<StateViewModel>();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
