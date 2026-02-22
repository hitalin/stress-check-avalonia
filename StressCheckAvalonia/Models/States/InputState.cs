using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Models.States;

public class InputState : IAppState
{
    public static readonly InputState Instance = new();

    public string GetAppTitle(StateViewModel context) => "ストレスチェック開始";
    public string GetDescriptionText(StateViewModel context) => "必須事項を入力してください。";
    public string NextButtonText => "入力を完了して開始";

    public void HandleNext(StateViewModel context)
    {
        if (context.EmployeeViewModel.IsInformationComplete())
        {
            context.CurrentState = State.SectionActive;
            context.SectionViewModel.UpdateDisplayedQuestions(0);
        }
        else
        {
            context.EmployeeViewModel.ValidateInput();
        }
    }

    public void HandleBack(StateViewModel context)
    {
        // No back action in Input state
    }
}
