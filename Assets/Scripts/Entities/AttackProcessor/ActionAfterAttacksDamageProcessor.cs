using System;
using Entities;

namespace Cards.CardActions
{
    public class ActionAfterAttacksDamageProcessor : DamageProcessor
    {
        private int _attacksCount;
        private Action _action;

        public ActionAfterAttacksDamageProcessor(int attacksCount)
        {
            _attacksCount = attacksCount;
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

        public int DamageProcess(int damage)
        {
            return damage;
        }

        public void PostDamageProcess(int damage)
        {
            _attacksCount -= 1;
            if(_attacksCount == 0)
                _action?.Invoke();
        }
    }
}