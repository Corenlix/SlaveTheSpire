using Entities.Buffs;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Buffs
{
    [CreateAssetMenu(menuName = "Buffs/TestBuff")]
    public class TestBuffStaticData : BuffStaticData
    {
        public override IBuffAction GetBuffAction(DiContainer diContainer)
        {
            return new TestBuffAction();
        }
    }
}