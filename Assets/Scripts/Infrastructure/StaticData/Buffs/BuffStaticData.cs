using Entities;
using Entities.Buffs;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Buffs
{
    public abstract class BuffStaticData : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private BuffId _id;
        [SerializeField] public BuffStackStrategy _buffStackStrategy;

        public Sprite Icon => _icon;
        public BuffId Id => _id;
        public BuffStackStrategy BuffStackStrategy => _buffStackStrategy;
        public abstract IBuffAction GetBuffAction(DiContainer diContainer, Entity buffTarget);
    }

    public enum BuffStackStrategy
    {
        None,
        Steps,
        Multiple,
    }
}