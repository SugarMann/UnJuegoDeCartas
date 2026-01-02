using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : Effect
{
    [SerializeField] private int damageAmount;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        var dealDamageGA = new DealDamageGA(damageAmount, targets, caster);
        return dealDamageGA;
    }
}