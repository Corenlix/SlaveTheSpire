namespace Entities.Buffs
{
    public class BonusDamageBuffAction : IBuffAction
    {
        private readonly Entity _buffTarget;
        private readonly int _bonusDamage;

        public BonusDamageBuffAction(Entity buffTarget, int bonusDamage)
        {
            _buffTarget = buffTarget;
            _bonusDamage = bonusDamage;
            Init();
        }

        private void Init()
        {
            _buffTarget.BonusDamage += _bonusDamage;
        }
        
        public void Step()
        {
            
        }

        public void End()
        {
            _buffTarget.BonusDamage -= _bonusDamage;
        }
    }
}