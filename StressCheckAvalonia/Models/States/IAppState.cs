using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Models.States;

public interface IAppState
{
    string GetAppTitle(StateViewModel context);
    string GetDescriptionText(StateViewModel context);
    string NextButtonText { get; }
    void HandleNext(StateViewModel context);
    void HandleBack(StateViewModel context);
}
