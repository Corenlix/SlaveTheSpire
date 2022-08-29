namespace Infrastructure
{
    public class LocationHolder
    {
        public Location Location { get; private set; }

        public void SetLocation(Location location)
        {
            Location = location;
        }
    }
}