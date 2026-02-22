using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Models.States;

public class SectionActiveState : IAppState
{
    public static readonly SectionActiveState Instance = new();

    public string GetAppTitle(StateViewModel context)
    {
        var section = context.SectionViewModel.CurrentSection;
        return section != null ? $"STEP {section.Step} {section.Name}" : "";
    }

    public string GetDescriptionText(StateViewModel context)
    {
        return context.SectionViewModel.CurrentSection?.Description ?? "";
    }

    public string NextButtonText => "1つ後の画面へ進む";

    public void HandleNext(StateViewModel context)
    {
        var svm = context.SectionViewModel;
        if (svm.CurrentSection == null) return;

        int currentIndex = LoadSections.Sections.IndexOf(svm.CurrentSection);

        if (svm.AreAllDisplayedQuestionsAnswered())
        {
            svm.UpdateScores();
            svm.UpdateValues();

            if (currentIndex >= 0 && currentIndex < LoadSections.Sections.Count - 1 && svm.AreAllQuestionsDisplayed())
            {
                svm.QuestionStartIndex = 0;
                svm.UpdateDisplayedQuestions(currentIndex + 1);
                context.CurrentState = State.SectionActive;
            }
            else if (!svm.AreAllQuestionsDisplayed())
            {
                svm.QuestionStartIndex += svm.QuestionsPerPage;
                svm.UpdateDisplayedQuestions(currentIndex);
            }
            else
            {
                context.CurrentState = State.Aggregated;
            }
        }
        else
        {
            foreach (var qvm in svm.DisplayedQuestionViewModels)
            {
                qvm.ValidateAnswered();
            }
        }
    }

    public void HandleBack(StateViewModel context)
    {
        var svm = context.SectionViewModel;
        if (svm.CurrentSection == null) return;

        int currentIndex = LoadSections.Sections.IndexOf(svm.CurrentSection);

        if (svm.QuestionStartIndex == 0)
        {
            if (currentIndex > 0)
            {
                svm.UpdateScores();
                svm.UpdateValues();

                currentIndex--;

                var previousSection = LoadSections.Sections.ElementAtOrDefault(currentIndex);
                if (previousSection?.Questions != null)
                {
                    svm.QuestionStartIndex = (previousSection.Questions.Count - 1) / svm.QuestionsPerPage * svm.QuestionsPerPage;
                }

                svm.UpdateDisplayedQuestions(currentIndex);
                context.CurrentState = State.SectionActive;
            }
            else
            {
                context.CurrentState = State.Input;
            }
        }
        else
        {
            svm.QuestionStartIndex -= svm.QuestionsPerPage;
            svm.UpdateDisplayedQuestions(currentIndex);
        }
    }
}
