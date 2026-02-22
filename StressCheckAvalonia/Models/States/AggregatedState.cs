using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Models.States;

public class AggregatedState : IAppState
{
    public static readonly AggregatedState Instance = new();

    public string GetAppTitle(StateViewModel context) => "ストレスチェック終了";
    public string GetDescriptionText(StateViewModel context) => "これで質問は、終わりです。お疲れさまでした。";
    public string NextButtonText => "結果を保存して終了";

    public void HandleNext(StateViewModel context)
    {
        Environment.Exit(0);
    }

    public void HandleBack(StateViewModel context)
    {
        var svm = context.SectionViewModel;
        int lastSectionIndex = LoadSections.Sections.Count - 1;

        if (lastSectionIndex >= 0)
        {
            var lastSection = LoadSections.Sections[lastSectionIndex];
            if (lastSection?.Questions != null)
            {
                svm.QuestionStartIndex = (lastSection.Questions.Count - 1) / svm.QuestionsPerPage * svm.QuestionsPerPage;
                svm.UpdateDisplayedQuestions(lastSectionIndex);
            }
        }

        context.CurrentState = State.SectionActive;
    }
}
