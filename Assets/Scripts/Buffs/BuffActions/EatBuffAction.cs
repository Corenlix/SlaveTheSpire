namespace Entities.Buffs
{
    public class EatBuffAction : IBuffAction
    {
        private readonly Entity _buffTarget;
        private readonly int _heal;

        public EatBuffAction(Entity buffTarget, int heal)
        {
            _buffTarget = buffTarget;
            _heal = heal;
        }
        
        public void Step()
        {
            _buffTarget.ApplyHeal(_heal);
        }

        public void End()
        {
            
        }
    }
}