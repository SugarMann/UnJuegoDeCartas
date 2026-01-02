using System.Collections.Generic;
using UnityEngine;

public class Perk
{
    private readonly PerkCondition condition;
    private readonly PerkData data;

    private readonly AutoTargetEffect effect;

    public Perk(PerkData perkData)
    {
        data = perkData;
        condition = data.PerkCondition;
        effect = data.AutoTargetEffect;
    }

    public Sprite Image => data.Image;

    public void OnRemove()
    {
        condition.UnsubscribeCondition(Reaction);
    }

    public void OnAdd()
    {
        condition.SubscribeCondition(Reaction);
    }

    private void Reaction(GameAction gameAction)
    {
        if (condition.SubConditionIsMet(gameAction))
        {
            List<CombatantView> targets = new();
            if (data.UseActionCasterAsTarget && gameAction is IHaveCaster haveCaster)
                targets.Add(haveCaster.Caster);
            if (data.UseAutoTarget)
                targets.AddRange(effect.TargetMode.GetTargets());
            var perkEffectAction = effect.Effect.GetGameAction(targets, HeroSystem.Instance.HeroView);
            ActionSystem.Instance.AddReaction(perkEffectAction);
        }
    }
}