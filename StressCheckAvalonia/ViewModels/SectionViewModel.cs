using System.Collections.ObjectModel;
using ReactiveUI;
using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;

namespace StressCheckAvalonia.ViewModels;

public class SectionViewModel : ReactiveObject
{
    private Section? _currentSection;
    private ReadOnlyCollection<QuestionViewModel> _questionViewModels = new([]);
    private readonly ObservableCollection<QuestionViewModel> _displayedQuestionViewModels = [];

    public SectionViewModel()
    {
        CurrentSection = LoadSections.Sections[0];
        _questionViewModels = new ReadOnlyCollection<QuestionViewModel>(
            CurrentSection?.Questions?.Select(q => new QuestionViewModel(q, this)).ToList() ?? []);
    }

    public Section? CurrentSection
    {
        get => _currentSection;
        set => this.RaiseAndSetIfChanged(ref _currentSection, value);
    }

    public void SetCurrentSection(int newSectionIndex)
    {
        if (newSectionIndex >= 0 && newSectionIndex < LoadSections.Sections.Count)
        {
            CurrentSection = LoadSections.Sections[newSectionIndex];
            _questionViewModels = new ReadOnlyCollection<QuestionViewModel>(
                CurrentSection?.Questions?.Select(q => new QuestionViewModel(q, this)).ToList() ?? []);
        }
    }

    public ReadOnlyCollection<Question>? Questions => CurrentSection?.Questions;
    public ReadOnlyCollection<string>? Choices => CurrentSection?.Choices;

    public void UpdateScores()
    {
        if (CurrentSection != null && Questions != null)
        {
            CurrentSection.Scores = Questions.ToList().CalculateScore();
        }
    }

    public void UpdateValues()
    {
        if (CurrentSection?.Factors != null)
        {
            foreach (var factor in CurrentSection.Factors)
            {
                factor.Value = Questions?.ToList().CalculateValue(factor) ?? 0;
            }

            CurrentSection.Values = CurrentSection.Factors.Sum(factor => factor.Value);
        }
    }

    public int QuestionStartIndex { get; set; }
    public int QuestionsPerPage { get; } = 10;

    public ObservableCollection<QuestionViewModel> DisplayedQuestionViewModels => _displayedQuestionViewModels;

    public void UpdateDisplayedQuestions(int sectionIndex)
    {
        SetCurrentSection(sectionIndex);

        _displayedQuestionViewModels.Clear();
        for (int i = QuestionStartIndex; i < QuestionStartIndex + QuestionsPerPage && i < _questionViewModels.Count; i++)
        {
            _displayedQuestionViewModels.Add(_questionViewModels[i]);
        }
    }

    public bool AreAllQuestionsDisplayed()
    {
        return QuestionStartIndex + QuestionsPerPage >= Questions?.Count;
    }

    public bool AreAllDisplayedQuestionsAnswered()
    {
        return DisplayedQuestionViewModels.All(q => q.IsAnswered);
    }
}