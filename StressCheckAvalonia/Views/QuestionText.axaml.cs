using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StressCheckAvalonia.Views;

public partial class QuestionText : UserControl
{
    public QuestionText()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
