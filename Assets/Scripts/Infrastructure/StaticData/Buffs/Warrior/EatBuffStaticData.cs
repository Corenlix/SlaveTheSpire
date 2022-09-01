using Entities;
using Entities.Buffs;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Buffs.Warrior
{
    [CreateAssetMenu(menuName = MenuNames.BuffDataMenuName+"Warrior/Eat")]
    public class EatBuffStaticData : BuffStaticData
    {
        [SerializeField] private int _heal;

        public override IBuffAction GetBuffAction(DiContainer diContainer, Entity buffTarget)
        {
            return new EatBuffAction(buffTarget, _heal);
        }
    }
}