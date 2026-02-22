using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using StressCheckAvalonia.Models;

namespace StressCheckAvalonia.Services;

public static class LoadSections
{
    private static readonly Lazy<ReadOnlyCollection<Section>> _sections = new(LoadFromJson);

    public static ReadOnlyCollection<Section> Sections => _sections.Value;

    private static ReadOnlyCollection<Section> LoadFromJson()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("StressCheckAvalonia.Assets.Data.sections.json")
            ?? throw new InvalidOperationException("sections.json embedded resource not found");

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        var dtos = JsonSerializer.Deserialize<List<SectionDto>>(stream, options)
            ?? throw new InvalidOperationException("Failed to deserialize sections.json");

        return dtos.Select(ToSection).ToList().AsReadOnly();
    }

    private static Section ToSection(SectionDto dto)
    {
        var questions = dto.Questions?.Select(q => new Question
        {
            Id = q.Id,
            Text = q.Text,
            Score = 0,
            Reverse = q.Reverse
        }).ToList();

        var factors = dto.Factors?.Select(f => new Factor(
            f.Rates?.Select(r => new Rate { Min = r.Min, Max = r.Max, Value = r.Value }).ToList(),
            f.Items
        )
        {
            Point = f.Point,
            Scale = f.Scale,
            Value = 0,
            Type = f.Type
        }).ToList();

        return new Section(questions, dto.Choices, factors)
        {
            Step = dto.Step,
            Name = dto.Name,
            Description = dto.Description,
            Group = dto.Group
        };
    }

    private sealed class SectionDto
    {
        public int Step { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Group { get; set; }
        public List<string>? Choices { get; set; }
        public List<QuestionDto>? Questions { get; set; }
        public List<FactorDto>? Factors { get; set; }
    }

    private sealed class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool Reverse { get; set; }
    }

    private sealed class FactorDto
    {
        public int Point { get; set; }
        public string Scale { get; set; } = string.Empty;
        public FactorType Type { get; set; }
        public List<int>? Items { get; set; }
        public List<RateDto>? Rates { get; set; }
    }

    private sealed class RateDto
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Value { get; set; }
    }
}
