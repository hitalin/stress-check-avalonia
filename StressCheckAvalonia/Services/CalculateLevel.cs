using StressCheckAvalonia.Models;

namespace StressCheckAvalonia.Services;

public static class LevelCalculator
{
    public static LevelResult CalculateLevel(this IReadOnlyList<int> scores, IReadOnlyList<int> values)
    {
        ArgumentNullException.ThrowIfNull(scores);

        ArgumentNullException.ThrowIfNull(values);

        bool method1 = scores[1] >= 77 || (scores[0] + scores[2] >= 76 && scores[1] >= 63);
        bool method2 = values[1] <= 12 || (values[0] + values[2] <= 26 && values[1] <= 17);

        return new LevelResult(method1, method2, values);
    }
}
