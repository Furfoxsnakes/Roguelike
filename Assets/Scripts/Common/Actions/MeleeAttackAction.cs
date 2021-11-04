using System;
using Enums;
using GoRogue.DiceNotation;
using UnityEngine;
using View_Component;

namespace Common.Actions
{
    public class MeleeAttackAction : AttackAction
    {
        public override bool Perform(Actor target)
        {
            var hits = ResolveHits(Owner.Stats);
            if (hits == 0)
            {
                GameController.Instance.DungeonController.DisplayCombatText("MISS", target.Vector3Pos);
                return true;
            }

            hits = ResolveDefense(target.Stats, hits);

            if (hits <= 0)
            {
                GameController.Instance.DungeonController.DisplayCombatText("BLOCKED", target.Vector3Pos);
                return true;
            }

            target.TakeDamage(hits);

            // target.TakeDamage(Owner.Stats[StatTypes.Attack]);
            return true;
            // return base.Perform(target);
        }

        private int ResolveHits(Stats attackerStats)
        {
            // var attackerStats = Owner.GetComponent<Stats>();
            var hits = 0;

            for (var dice = 0; dice < attackerStats[StatTypes.Attack]; dice++)
            {
                var rollOutcome = Dice.Roll("1d100");
                if (rollOutcome <= attackerStats[StatTypes.AttackChance])
                    hits++;
            }

            return hits;
        }

        private int ResolveDefense(Stats defenderStats, int hits)
        {
            if (hits <= 0) return 0;

            for (var dice = 0; dice < defenderStats[StatTypes.Defense]; dice++)
            {
                var rollOutcome = Dice.Roll("1d100");
                if (rollOutcome <= defenderStats[StatTypes.DefenseChance])
                    hits--;
            }

            return Math.Max(0, hits);
        }
    }
}