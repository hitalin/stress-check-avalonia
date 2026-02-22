using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media;

namespace StressCheckAvalonia.Models;

public enum State
{
    Input,
    SectionActive,
    Aggregated
}

public enum FactorType
{
    Subtraction,
    Addition,
    Complex
}

public enum StressLevel
{
    High,
    Low
}

public record LevelResult(bool Method1, bool Method2, IReadOnlyList<int> Totals);

public record RadarChartData(string? Label, double Value, Color Color);

public class Employee
{
    public string Gender { get; set; } = string.Empty;
    public StressLevel Level { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Furigana { get; set; } = string.Empty;
    public DateTimeOffset Birthday { get; set; }
    public string ID { get; set; } = string.Empty;
    public string Workplace { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Phone1 { get; set; } = string.Empty;
    public string Phone2 { get; set; } = string.Empty;
    public string Phone3 { get; set; } = string.Empty;
    public string? Extension { get; set; }
}

public class Section(IEnumerable<Question>? questions, IEnumerable<string>? choices, IEnumerable<Factor>? factors)
{
    public int Step { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ReadOnlyCollection<Question>? Questions { get; } = questions?.ToList().AsReadOnly();
    public int Scores { get; set; }
    public ReadOnlyCollection<string>? Choices { get; } = choices?.ToList().AsReadOnly();
    public string? Group { get; set; }
    public ReadOnlyCollection<Factor>? Factors { get; } = factors?.ToList().AsReadOnly();
    public int Values { get; set; }
}

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int Score { get; set; }
    public bool Reverse { get; set; }
}

public class Factor(IEnumerable<Rate>? rates, IEnumerable<int>? items)
{
    public int Point { get; set; }
    public string Scale { get; set; } = string.Empty;
    public int Value { get; set; }
    public FactorType Type { get; set; }
    public ReadOnlyCollection<Rate>? Rates { get; } = rates?.ToList().AsReadOnly();
    public ReadOnlyCollection<int>? Items { get; } = items?.ToList().AsReadOnly();
}

public class Rate
{
    public int Min { get; set; }
    public int Max { get; set; }
    public int Value { get; set; }
}