using Entities;
using UnityEngine;

namespace Infrastructure.StaticData
{
    public abstract class EnemyStaticData : ScriptableObject
    {
        [SerializeField] private EnemyId _id;

        public EnemyId Id => _id;
        public abstract Enemy EnemyPrefab { get; }
    }
}