using System.Drawing;

namespace Fituska.Client;

public class ColorTints
{
    public struct ColorTint
    {
        public Color Color;
        public int VotesCount;
    }

    public static ColorTint[] NegativeTints = {
        new ColorTint{
            Color = Color.FromArgb(246, 209, 215),
            VotesCount = -1,
        },
        new ColorTint
        {
            Color = Color.FromArgb(242, 187, 197),
            VotesCount = -2,
        },
        new ColorTint
        {
            Color = Color.FromArgb(238, 165, 178),
            VotesCount = -5,
        },
        new ColorTint
        {
            Color = Color.FromArgb(234, 144, 159),
            VotesCount = -10,
        },
        new ColorTint
        {
            Color = Color.FromArgb(230, 122, 141),
            VotesCount = -15,
        },
        new ColorTint
        {
            Color = Color.FromArgb(226, 100, 122),
            VotesCount = -30,
        },
        new ColorTint
        {
            Color = Color.FromArgb(221, 78, 103),
            VotesCount = -60,
        },
        new ColorTint
        {
            Color = Color.FromArgb(217, 57, 84),
            VotesCount = -90,
        },
        new ColorTint
        {
            Color = Color.FromArgb(213, 35, 66),
            VotesCount = -120,
        },
        new ColorTint
        {
            Color = Color.FromArgb(209, 13, 47),
            VotesCount = -150,
        },
        new ColorTint
        {
            Color = Color.FromArgb(255, 0, 0),
            VotesCount = -200,
        },
    };

    public static ColorTint[] PositiveTints = {
        new ColorTint{
            Color = Color.FromArgb(232, 255, 230),
            VotesCount = 1,
        },
        new ColorTint{
            Color = Color.FromArgb(232, 250, 220),
            VotesCount = 2,
        },
        new ColorTint{
            Color = Color.FromArgb(216, 246, 197),
            VotesCount = 5,
        },
        new ColorTint{
            Color = Color.FromArgb(185, 239, 150),
            VotesCount = 10,
        },
        new ColorTint{
            Color = Color.FromArgb(154, 233, 104),
            VotesCount = 15,
        },
        new ColorTint{
            Color = Color.FromArgb(123, 226, 58),
            VotesCount = 30,
        },
        new ColorTint{
            Color = Color.FromArgb(108, 223, 35),
            VotesCount = 60,
        },
        new ColorTint{
            Color = Color.FromArgb(108, 223, 35),
            VotesCount = 90,
        },
        new ColorTint{
            Color = Color.FromArgb(100, 221, 23),
            VotesCount = 120,
        },
    };

    public static Color GetColorFromVotes(int votes)
    {
        if (votes == 0) return Color.WhiteSmoke;
        if (votes > 0)
        {
            return GetColorFromPositiveVotes(votes);
        }
        return GetColorFromNegativeVotes(votes);
    }

    private static Color GetColorFromNegativeVotes(int votes)
    {
        for(int i = NegativeTints.Length-1; i >= 0; i--)
        {
            if (votes <= NegativeTints[i].VotesCount)
            {
                return NegativeTints[i].Color;
            }
        }
        return Color.WhiteSmoke;
    }

    private static Color GetColorFromPositiveVotes(int votes)
    {
        for (int i = PositiveTints.Length - 1; i >= 0; i--)
        {
            if (votes >= PositiveTints[i].VotesCount)
            {
                return PositiveTints[i].Color;
            }
        }
        return Color.WhiteSmoke;
    }
}
