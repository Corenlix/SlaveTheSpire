using Entities.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.StaticData.Enemies
{
    public abstract class EnemyStaticData : ScriptableObject
    {
        [SerializeField] private EnemyId _id;
        [SerializeField] private int _maxHealth;
        [SerializeField] private string _name;
        [SerializeField] private int _armor;

        public EnemyId Id => _id;
        public abstract Enemy EnemyPrefab { get; }
        public int MaxHealth => _maxHealth;
        public string Name => _name;
        public int Armor => _armor;
    }
}