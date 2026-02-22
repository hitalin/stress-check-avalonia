using System.Collections.ObjectModel;
using Avalonia.Media;
using ReactiveUI;
using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;

namespace StressCheckAvalonia.ViewModels;

public record SectionResultItem(
    string Title,
    string ScoresText,
    string ValuesText,
    string? Group,
    IReadOnlyList<RadarChartData>? RadarChartItems);

public class ResultsViewModel : ReactiveObject
{
    public EmployeeViewModel Employee { get; }

    private readonly ObservableCollection<SectionResultItem> _sectionResults = [];
    public ObservableCollection<SectionResultItem> SectionResults => _sectionResults;

    public ResultsViewModel(EmployeeViewModel employeeViewModel)
    {
        Employee = employeeViewModel;
    }

    public void Calculate()
    {
        var scores = LoadSections.Sections.Select(s => s.Scores).ToList();
        var values = LoadSections.Sections.Select(s => s.Values).ToList();
        var levelResult = scores.CalculateLevel(values);

        Employee.Level = levelResult.Method1 && levelResult.Method2 ? StressLevel.High : StressLevel.Low;

        var color = Employee.Level == StressLevel.High
            ? EmployeeViewModel.HighStressColor
            : EmployeeViewModel.LowStressColor;

        _sectionResults.Clear();
        foreach (var section in LoadSections.Sections.Take(LoadSections.Sections.Count - 1))
        {
            _sectionResults.Add(new SectionResultItem(
                Title: $"STEP {section.Step} {section.Name}",
                ScoresText: $"スコアの合計: {section.Scores}",
                ValuesText: $"評価点の合計: {section.Values}",
                Group: section.Group,
                RadarChartItems: section.Factors?.Select(f =>
                    new RadarChartData(f.Scale, f.Value, color)).ToList()
            ));
        }
    }
}
