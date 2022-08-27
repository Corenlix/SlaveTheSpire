namespace Entities
{
    public class Player : Entity
    {
        public BoundedValue Energy => _energy;
        private BoundedValue _energy;

        private void Start()
        {
            InitHealth(3);
            _energy = new BoundedValue(3, 3);
        }
    }
}