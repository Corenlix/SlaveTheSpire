namespace Entities
{
    public class Player : Entity
    {
        public BoundedValue Energy { get; private set; }
        
        public void Init(int energy, int health, string name, int shield)
        {
            Energy = new BoundedValue(energy);
            base.Init(health, health, name, shield);
        }

        protected override void OnStep()
        {
            
        }
    }
}