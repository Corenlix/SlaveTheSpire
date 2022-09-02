using Entities.Buffs;
using Infrastructure.StaticData.Buffs;

namespace Entities
{
    public class BuffsHolderFacade
    {
        private readonly BuffsHolder _buffsHolder;

        public BuffsHolderFacade(BuffsHolder buffsHolder)
        {
            _buffsHolder = buffsHolder;
        }

        public void AddBuff(BuffId id, int steps) => _buffsHolder.Add(id, steps);
    }
}