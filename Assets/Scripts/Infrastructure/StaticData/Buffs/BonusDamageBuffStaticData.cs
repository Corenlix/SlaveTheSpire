using Entities;
using Entities.Buffs;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Buffs
{
    public class BonusDamageBuffStaticData : BuffStaticData
    {
        [SerializeField] private int _damage;
        
        public override IBuffAction GetBuffAction(DiContainer diContainer, Entity buffTarget)
        {
            return new BonusDamageBuffAction(buffTarget, _damage);
        }
    }
}