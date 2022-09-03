using Entities;
using Entities.Buffs;

namespace Buffs.BuffActions
{
    public class FearBuffAction : IBuffAction
    {
        private readonly Entity _buffTarget;
        private readonly float _reduceDamageFactor;

        public FearBuffAction(Entity buffTarget, float reduceDamageFactor)
        {
            _buffTarget = buffTarget;
            _reduceDamageFactor = reduceDamageFactor;
            Init();
        }

        private void Init()
        {
            _buffTarget.AttackProcessor.DamageMultiplier -= _reduceDamageFactor;
        }
        
        public void Step() 
        {
            
        }

        public void End()
        {
            _buffTarget.AttackProcessor.DamageMultiplier += _reduceDamageFactor;
        }
    }
}
