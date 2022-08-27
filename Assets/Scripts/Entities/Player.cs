namespace Entities
{
    public class Player : Entity
    {
        public void Init(BoundedValue health)
        {
            InitHealth(health);
        }
    }
}