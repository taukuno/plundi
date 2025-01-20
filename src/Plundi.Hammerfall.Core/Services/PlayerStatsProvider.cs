namespace Plundi.Hammerfall.Core.Services;

public static class PlayerStatsProvider
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

    public static int GetHitPoints(int playerLevel)
    {
        if (playerLevel is < 1 or > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(playerLevel), "Player level must be between 1 and 10.");
        }

        return HitPoints[playerLevel];
    }

    public static int GetAttackPower(int playerLevel)
    {
        if (playerLevel is < 1 or > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(playerLevel), "Player level must be between 1 and 10.");
        }

        return AttackPower[playerLevel];
    }

    public static decimal CalculateMeleeAttack(int playerLevel)
    {
        if (playerLevel is < 1 or > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(playerLevel), "Player level must be between 1 and 10.");
        }

        return Math.Round(GetAttackPower(playerLevel) * 0.226m, 1);
    }

    public static decimal CalculatePowerLevel(int playerLevel)
    {
        return CalculatePowerDifference(playerLevel, 1);
    }

    public static decimal CalculatePowerDifference(int playerLevel, int targetLevel)
    {
        var playerHitPoints = Convert.ToDecimal(HitPoints[playerLevel]);
        var playerAttackPower = Convert.ToDecimal(AttackPower[playerLevel]);

        var targetHitPoints = Convert.ToDecimal(HitPoints[targetLevel]);
        var targetAttackPower = Convert.ToDecimal(AttackPower[targetLevel]);

        var hitPointDifference = playerHitPoints / targetHitPoints;
        var attackPowerDifference = playerAttackPower / targetAttackPower;

        var powerLevel = hitPointDifference * attackPowerDifference;
        return powerLevel;
    }
}
