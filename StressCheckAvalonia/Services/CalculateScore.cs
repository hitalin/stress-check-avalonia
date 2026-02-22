using System.Collections.Generic;
using System.Linq;
using StressCheckAvalonia.Models;

namespace StressCheckAvalonia.Services;

public static class ScoreCalculator
{
    public static int CalculateScore(this List<Question> questions)
    {
        return questions.Sum(question => question.Reverse ? 5 - question.Score : question.Score);
    }
}