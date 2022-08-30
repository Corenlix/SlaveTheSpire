using Entities.Buffs;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "Buffs/TestBuff")]
    public class TestBuffStaticData : BuffStaticData
    {
        public override Buff GetBuff(BuffId id, int steps, DiContainer diContainer)
        {
            return new TestBuff(id, steps);
        }
    }
}