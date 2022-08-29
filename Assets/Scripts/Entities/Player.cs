namespace Entities
{
    public class Player : Entity
    {
        public BoundedValue Energy { get; private set; }
        
        public void Init(int energy, int health, string name)
        {
            Energy = new BoundedValue(energy);
            base.Init(health, health, name);
        }

        protected override void OnStep()
        {
            
        }
    }
}