namespace Entities.Buffs
{
    public interface IBuffAction
    {
        void Step();
        void End();
    }
}