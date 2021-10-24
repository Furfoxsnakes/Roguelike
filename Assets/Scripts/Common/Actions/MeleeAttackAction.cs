using Enums;

namespace Common.Actions
{
    public class MeleeAttackAction : AttackAction
    {
        public override bool Perform(Actor target)
        {
            target.TakeDamage(Owner.Stats[StatTypes.Attack]);

            return base.Perform(target);
        }
    }
}