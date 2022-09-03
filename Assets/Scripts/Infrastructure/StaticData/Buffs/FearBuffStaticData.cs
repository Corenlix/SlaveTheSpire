using Buffs.BuffActions;
using Entities;
using Entities.Buffs;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Buffs
{
    [CreateAssetMenu(menuName = MenuNames.BuffDataMenuName+"FearBuff")]
    public class FearBuffStaticData : BuffStaticData
    {
        [SerializeField] private float _reduceDamageFactor;
        
        public override IBuffAction GetBuffAction(DiContainer diContainer, Entity buffTarget)
        {
            return new FearBuffAction(buffTarget, _reduceDamageFactor);
        }
    }
}
