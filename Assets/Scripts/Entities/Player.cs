namespace Entities
{
    public class Player : Entity
    {
        public void Init(BoundedValue health)
        {
            InitView(health, "Player");
        }

        protected override void OnStep()
        {
            
        }
    }
}