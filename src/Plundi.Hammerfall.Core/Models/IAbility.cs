namespace Plundi.Hammerfall.Core.Models;

public interface IAbility
{
    public string Name { get; }
    public double CastDuration { get; }
    public double ChannelDuration { get; }
    public string ImagePath { get; }
    public AbilityType Type { get; }
    public List<AbilityEffect> GetEffects(int characterLevel, AbilityRarity abilityRarity);
    public double GetCooldown(int characterLevel, AbilityRarity abilityRarity);
    public DamageProfile GetDamageProfile(int characterLevel, AbilityRarity abilityRarity);
}