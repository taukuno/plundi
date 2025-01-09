namespace Plundi.Hammerfall.Core.Services;

public static class CharacterStatsProvider
{
    private static readonly Dictionary<int, int> HitPoints = new()
    {
        { 1, 100 },
        { 2, 116 },
        { 3, 132 },
        { 4, 148 },
        { 5, 164 },
        { 6, 180 },
        { 7, 196 },
        { 8, 212 },
        { 9, 228 },
        { 10, 244 }
    };

    private static readonly Dictionary<int, int> AttackPower = new()
    {
        { 1, 34 },
        { 2, 42 },
        { 3, 50 },
        { 4, 58 },
        { 5, 66 },
        { 6, 74 },
        { 7, 82 },
        { 8, 90 },
        { 9, 98 },
        { 10, 103 }
    };

    public static int GetHitPoints(int characterLevel)
    {
        if (characterLevel is < 1 or > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(characterLevel), "Character level must be between 1 and 10.");
        }

        return HitPoints[characterLevel];
    }

    public static int GetAttackPower(int characterLevel)
    {
        if (characterLevel is < 1 or > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(characterLevel), "Character level must be between 1 and 10.");
        }

        return AttackPower[characterLevel];
    }

    public static double CalculateMeleeAttack(int characterLevel)
    {
        if (characterLevel is < 1 or > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(characterLevel), "Character level must be between 1 and 10.");
        }

        return Math.Round(GetAttackPower(characterLevel) * 0.16, 1);
    }
}
