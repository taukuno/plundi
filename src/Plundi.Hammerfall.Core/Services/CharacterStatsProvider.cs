namespace Plundi.Hammerfall.Core.Services;

public static class CharacterStatsProvider
{
    private static readonly Dictionary<int, int> HitPoints = new()
    {
        { 1, 100 },
        { 2, 120 },
        { 3, 136 },
        { 4, 152 },
        { 5, 164 },
        { 6, 176 },
        { 7, 192 },
        { 8, 196 },
        { 9, 200 },
        { 10, 200 }
    };

    private static readonly Dictionary<int, int> AttackPower = new()
    {
        { 1, 31 },
        { 2, 41 },
        { 3, 49 },
        { 4, 56 },
        { 5, 61 },
        { 6, 65 },
        { 7, 69 },
        { 8, 72 },
        { 9, 74 },
        { 10, 76 }
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

        return Math.Round(GetAttackPower(characterLevel) * 0.226, 1);
    }
}