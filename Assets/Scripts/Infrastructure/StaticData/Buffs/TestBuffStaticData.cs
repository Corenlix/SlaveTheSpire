using Entities;
using Entities.Buffs;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Buffs
{
    [CreateAssetMenu(menuName = MenuNames.BuffDataMenuName+"TestBuff")]
    public class TestBuffStaticData : BuffStaticData
    {
        public override IBuffAction GetBuffAction(DiContainer diContainer, Entity buffTarget)
        {
            return new TestBuffAction();
        }
    }
}