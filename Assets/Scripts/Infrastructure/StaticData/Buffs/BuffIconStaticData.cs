using Entities.Buffs;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Buffs
{
    public abstract class BuffStaticData : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private BuffId _id;
        
        public Sprite Icon => _icon;
        public BuffId Id => _id;
        public abstract Buff GetBuff(BuffId id, int steps, DiContainer diContainer);
    }
}