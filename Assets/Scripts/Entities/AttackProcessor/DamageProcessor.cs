namespace Entities
{
    public interface DamageProcessor
    {
        int DamageProcess(int damage);
        void PostDamageProcess(int damage);
    }
}